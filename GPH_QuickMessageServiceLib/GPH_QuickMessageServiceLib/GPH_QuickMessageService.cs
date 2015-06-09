using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; //for read from file
using System.Runtime.Serialization;
using System.Diagnostics; //for debug
using System.ServiceModel;// for WCF to happen

namespace GPH_QuickMessageServiceLib
{
    /// <summary>
    /// GPH Quick Message Service Operations
    /// </summary>
    [ServiceContract(
    Name = "GPH_QuickMessageService",
    Namespace = "GPH_QuickMessageServiceLib",
    SessionMode = SessionMode.Required,
    CallbackContract = typeof(IMessageServiceCallback))]

    public interface IMessageServiceInbound
    {
        //Allow client to connect or disconnect from the chat
        [OperationContract]
        void JoinTheConversation(string userName);
        [OperationContract]
        void LeaveTheConversation(string userName);

        //Send message from server to client
        [OperationContract(IsOneWay = true)]
        void BroadcastClientMsg(string MessageNo, string userMessage);
        [OperationContract(IsOneWay = true)]
        void TargetClientMsg(List<string> addressList, string MessageNo, string userMessage);

        //Reply message from client to server
        [OperationContract(IsOneWay = true)]
        void ReplyServerMsg(string userName, string MessageNo, string userReply);

        //Confirm or Cancel from server to client
        [OperationContract(IsOneWay = true)]
        void ConfirmMsg(string userName, string MessageNo, string serverConfirm);
        [OperationContract]
        void TaskCompleted(string userName);


        //Pass ConnectionList to Server
        [OperationContract]
        Dictionary<string, string> GetStationList();

        //Return Message Number
        [OperationContract]
        void IncreaseMessageNo();
        [OperationContract]
        string GetMessageNo();

        //Server join service to establish channel here
        [OperationContract]
        void ServerJoin();
        [OperationContract]
        void ServerLeave();

    }

    public interface IMessageServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void NotifyCurrentConnList(string userName, List<string> SubscriberList);

        [OperationContract(IsOneWay = true)]
        void NotifyUserOfMessage(string userName, string MessageNo, string userMessage);

        //Callback to trigger changes to Connection Status List
        [OperationContract(IsOneWay = true)]
        void EditConnStatus(string Name, string Status);
        [OperationContract(IsOneWay = true)]
        void EditMessageStatus(string Name, string MessageNo, string Progress);

        //for confirmation 
        [OperationContract(IsOneWay = true)]
        void NotifyUserOfConfirmMessage(string userName, string MessageNo, string userMessage, string serverConfirm);

    }

    /// <summary>
    /// GPH Quick Message Service behaviour
    /// </summary>
    /// <remarks>
    /// Single is the default concurrency mode, but it never hurts to be explicit.
    /// Note the single threading model still functions properly with use of one way methods for callbacks 
    /// since they are marked as one way on the contract.
    /// </remarks>
    [ServiceBehavior(
            ConcurrencyMode = ConcurrencyMode.Single,
            InstanceContextMode = InstanceContextMode.PerCall)]

    public class GPH_QuickMessageService : IMessageServiceInbound
    {
        private static List<IMessageServiceCallback> _callbackList = new List<IMessageServiceCallback>();
        //Server callback channel
        private static List<IMessageServiceCallback> _servercallbackList = new List<IMessageServiceCallback>();

        private static List<string> ConnectedClientList = new List<string>();
        private static Dictionary<string, IMessageServiceCallback> NotifyList =
        new Dictionary<string, IMessageServiceCallback>();// Default Constructor

        //List for connection status 
        private static Dictionary<string, string> StationList = new Dictionary<string, string>();

        //Message Number
        private static int _MessageNo = 1;
        
        // Default Constructor
        public GPH_QuickMessageService()
        {
            //read from file to store the station name (only run one time for the first server)
            if (StationList.Count == 0)
            {
                string[] lineOfContents = File.ReadAllLines(@"configFile.txt");
                foreach (var name in lineOfContents)
                {
                    StationList.Add(name, "Disconnected");
                }
            }
        }

        //Sub to server to receive msg from server
        public void JoinTheConversation(string userName)
        {
            // Subscribe the user to the conversation
            IMessageServiceCallback registeredUser = OperationContext.Current.GetCallbackChannel<IMessageServiceCallback>();

            if (!_callbackList.Contains(registeredUser))
            {
                _callbackList.Add(registeredUser);//Note the callback list is just a list of channels.
                ConnectedClientList.Add(userName);
                NotifyList.Add(userName, registeredUser);//Bind the username to the callback channel ID

                //Update the station status
                StationList[userName] = "Connected";

                //Handle case where 2 client log in as same ID
            }

            _servercallbackList.ForEach(
                delegate(IMessageServiceCallback servercallback)
                {
                    servercallback.NotifyCurrentConnList(userName, ConnectedClientList);
                    servercallback.EditConnStatus(userName, "Connected");
                });

        }

        public void LeaveTheConversation(string userName)
        {
            // Unsubscribe the user from the conversation.      
            IMessageServiceCallback registeredUser = OperationContext.Current.GetCallbackChannel<IMessageServiceCallback>();

            if (_callbackList.Contains(registeredUser))
            {
                _callbackList.Remove(registeredUser);
                NotifyList.Remove(userName);
                ConnectedClientList.Remove(userName);

                //Update the station status
                StationList[userName] = "Disconnected";
            }

            // Notify everyone that user has arrived.
            // Use an anonymous delegate and generics to do our dirty work.

            _servercallbackList.ForEach(
                delegate(IMessageServiceCallback servercallback)
                {
                    servercallback.NotifyCurrentConnList(userName, ConnectedClientList);
                    servercallback.EditConnStatus(userName, "Disconnected");
                });

        }

        public void BroadcastClientMsg(string MessageNo, string userMessage)
        {

            // Notify the users of a message.
            // Use an anonymous delegate and generics to do our dirty work.

            foreach (string tmpAddr in ConnectedClientList)
            {
                IMessageServiceCallback tmpCallback = NotifyList[tmpAddr];
                tmpCallback.NotifyUserOfMessage("Server", MessageNo, userMessage);
            }
        }

        public void TargetClientMsg(List<string> addressList,string MessageNo, string userMessage)
        {
            foreach (string tmpAddr in addressList)
            {
                IMessageServiceCallback tmpCallback = NotifyList[tmpAddr];
                tmpCallback.NotifyUserOfMessage("Server", MessageNo, userMessage);
            }
        }

        public void ReplyServerMsg(string userName, string MessageNo, string userReply)
        {
            //Loop server callback channel to send back reply
            foreach (IMessageServiceCallback tmpCallback in _servercallbackList)
            {
                tmpCallback.EditMessageStatus(userName, MessageNo, userReply);
            }
        }

        public void ConfirmMsg(string userName, string MessageNo, string serverConfirm)
        {
            IMessageServiceCallback tmpCallback = NotifyList[userName];
            
            //If server confirm the task to client
            if (serverConfirm.Equals("Confirm"))
            {
                //update the connection status
                StationList[userName] = "Busy";
                //Remove from list in order not to receive new message
                ConnectedClientList.Remove(userName);

                tmpCallback.NotifyUserOfConfirmMessage("Server", MessageNo, "This Message had been Confirmed", serverConfirm);

                //Change client connection status in server to be busy, in order not to receive any new message
                _servercallbackList.ForEach(
                    delegate(IMessageServiceCallback servercallback)
                    {
                        servercallback.EditConnStatus(userName, "Busy");
                        servercallback.NotifyCurrentConnList(userName, ConnectedClientList);
                    });
            }
            //server cancel the task
            else
            {
                tmpCallback.NotifyUserOfConfirmMessage("Server", MessageNo, "This Message had been Cancelled", serverConfirm);
                _servercallbackList.ForEach(
                    delegate(IMessageServiceCallback servercallback)
                    {
                        servercallback.EditConnStatus(userName, "Connected");
                    });
            }
            
        }

        public void TaskCompleted(string userName)
        {
            //Add back to available connected client
            ConnectedClientList.Add(userName);

            _servercallbackList.ForEach(
                delegate(IMessageServiceCallback servercallback)
                {
                    servercallback.EditConnStatus(userName, "Connected");
                    servercallback.NotifyCurrentConnList(userName, ConnectedClientList);
                });
        }

        public Dictionary<string, string> GetStationList()
        {
            return StationList;
        }

        public void IncreaseMessageNo()
        {
            _MessageNo++;
        }

        public string GetMessageNo()
        {
            return _MessageNo.ToString();
        }

        //Server join and leave in order to place a channel path here
        public void ServerJoin()
        {
            // Subscribe the user to the conversation
            IMessageServiceCallback registeredServer = OperationContext.Current.GetCallbackChannel<IMessageServiceCallback>();

            if (!_servercallbackList.Contains(registeredServer))
            {                
                _servercallbackList.Add(registeredServer);//Note the callback list is just a list of channels.
                Debug.WriteLine(_servercallbackList.Count());
                // need to remove the servercallback channel from list

            }
        }

        public void ServerLeave()
        {
            //Unsubscribe the user to the conversation
            IMessageServiceCallback registeredServer = OperationContext.Current.GetCallbackChannel<IMessageServiceCallback>();

            if (_servercallbackList.Contains(registeredServer))
            {
                _servercallbackList.Remove(registeredServer);//Note the callback list is just a list of channels.

            }
        }
    }
}