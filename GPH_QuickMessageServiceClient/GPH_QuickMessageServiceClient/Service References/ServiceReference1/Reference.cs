﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GPH_QuickMessageServiceClient.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="GPH_QuickMessageServiceLib", ConfigurationName="ServiceReference1.GPH_QuickMessageService", CallbackContract=typeof(ServiceReference1.GPH_QuickMessageServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface GPH_QuickMessageService {
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/JoinTheConversation", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/JoinTheConversationResponse")]
        void JoinTheConversation(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/JoinTheConversation", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/JoinTheConversationResponse")]
        System.Threading.Tasks.Task JoinTheConversationAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/LeaveTheConversation", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/LeaveTheConversationResponse")]
        void LeaveTheConversation(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/LeaveTheConversation", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/LeaveTheConversationResponse")]
        System.Threading.Tasks.Task LeaveTheConversationAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/BroadcastClientMsg")]
        void BroadcastClientMsg(string MessageNo, string userMessage);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/BroadcastClientMsg")]
        System.Threading.Tasks.Task BroadcastClientMsgAsync(string MessageNo, string userMessage);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/TargetClientMsg")]
        void TargetClientMsg(string[] addressList, string MessageNo, string userMessage);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/TargetClientMsg")]
        System.Threading.Tasks.Task TargetClientMsgAsync(string[] addressList, string MessageNo, string userMessage);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/ReplyServerMsg")]
        void ReplyServerMsg(string userName, string MessageNo, string userReply);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/ReplyServerMsg")]
        System.Threading.Tasks.Task ReplyServerMsgAsync(string userName, string MessageNo, string userReply);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/ConfirmMsg")]
        void ConfirmMsg(string userName, string MessageNo, string serverConfirm);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/ConfirmMsg")]
        System.Threading.Tasks.Task ConfirmMsgAsync(string userName, string MessageNo, string serverConfirm);
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/TaskCompleted", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/TaskCompletedResponse")]
        void TaskCompleted(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/TaskCompleted", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/TaskCompletedResponse")]
        System.Threading.Tasks.Task TaskCompletedAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/GetStationList", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/GetStationListResponse")]
        System.Collections.Generic.Dictionary<string, string> GetStationList();
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/GetStationList", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/GetStationListResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> GetStationListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/IncreaseMessageNo", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/IncreaseMessageNoResponse")]
        void IncreaseMessageNo();
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/IncreaseMessageNo", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/IncreaseMessageNoResponse")]
        System.Threading.Tasks.Task IncreaseMessageNoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/GetMessageNo", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/GetMessageNoResponse")]
        string GetMessageNo();
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/GetMessageNo", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/GetMessageNoResponse")]
        System.Threading.Tasks.Task<string> GetMessageNoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/ServerJoin", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/ServerJoinResponse")]
        void ServerJoin();
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/ServerJoin", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/ServerJoinResponse")]
        System.Threading.Tasks.Task ServerJoinAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/ServerLeave", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/ServerLeaveResponse")]
        void ServerLeave();
        
        [System.ServiceModel.OperationContractAttribute(Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/ServerLeave", ReplyAction="GPH_QuickMessageServiceLib/GPH_QuickMessageService/ServerLeaveResponse")]
        System.Threading.Tasks.Task ServerLeaveAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface GPH_QuickMessageServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/NotifyCurrentConnList")]
        void NotifyCurrentConnList(string userName, string[] SubscriberList);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/NotifyUserOfMessage")]
        void NotifyUserOfMessage(string userName, string MessageNo, string userMessage);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/EditConnStatus")]
        void EditConnStatus(string Name, string Status);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/EditMessageStatus")]
        void EditMessageStatus(string Name, string MessageNo, string Progress);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="GPH_QuickMessageServiceLib/GPH_QuickMessageService/NotifyUserOfConfirmMessage")]
        void NotifyUserOfConfirmMessage(string userName, string MessageNo, string userMessage, string serverConfirm);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface GPH_QuickMessageServiceChannel : ServiceReference1.GPH_QuickMessageService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GPH_QuickMessageServiceClient : System.ServiceModel.DuplexClientBase<ServiceReference1.GPH_QuickMessageService>, ServiceReference1.GPH_QuickMessageService {
        
        public GPH_QuickMessageServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public GPH_QuickMessageServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public GPH_QuickMessageServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GPH_QuickMessageServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GPH_QuickMessageServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void JoinTheConversation(string userName) {
            base.Channel.JoinTheConversation(userName);
        }
        
        public System.Threading.Tasks.Task JoinTheConversationAsync(string userName) {
            return base.Channel.JoinTheConversationAsync(userName);
        }
        
        public void LeaveTheConversation(string userName) {
            base.Channel.LeaveTheConversation(userName);
        }
        
        public System.Threading.Tasks.Task LeaveTheConversationAsync(string userName) {
            return base.Channel.LeaveTheConversationAsync(userName);
        }
        
        public void BroadcastClientMsg(string MessageNo, string userMessage) {
            base.Channel.BroadcastClientMsg(MessageNo, userMessage);
        }
        
        public System.Threading.Tasks.Task BroadcastClientMsgAsync(string MessageNo, string userMessage) {
            return base.Channel.BroadcastClientMsgAsync(MessageNo, userMessage);
        }
        
        public void TargetClientMsg(string[] addressList, string MessageNo, string userMessage) {
            base.Channel.TargetClientMsg(addressList, MessageNo, userMessage);
        }
        
        public System.Threading.Tasks.Task TargetClientMsgAsync(string[] addressList, string MessageNo, string userMessage) {
            return base.Channel.TargetClientMsgAsync(addressList, MessageNo, userMessage);
        }
        
        public void ReplyServerMsg(string userName, string MessageNo, string userReply) {
            base.Channel.ReplyServerMsg(userName, MessageNo, userReply);
        }
        
        public System.Threading.Tasks.Task ReplyServerMsgAsync(string userName, string MessageNo, string userReply) {
            return base.Channel.ReplyServerMsgAsync(userName, MessageNo, userReply);
        }
        
        public void ConfirmMsg(string userName, string MessageNo, string serverConfirm) {
            base.Channel.ConfirmMsg(userName, MessageNo, serverConfirm);
        }
        
        public System.Threading.Tasks.Task ConfirmMsgAsync(string userName, string MessageNo, string serverConfirm) {
            return base.Channel.ConfirmMsgAsync(userName, MessageNo, serverConfirm);
        }
        
        public void TaskCompleted(string userName) {
            base.Channel.TaskCompleted(userName);
        }
        
        public System.Threading.Tasks.Task TaskCompletedAsync(string userName) {
            return base.Channel.TaskCompletedAsync(userName);
        }
        
        public System.Collections.Generic.Dictionary<string, string> GetStationList() {
            return base.Channel.GetStationList();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> GetStationListAsync() {
            return base.Channel.GetStationListAsync();
        }
        
        public void IncreaseMessageNo() {
            base.Channel.IncreaseMessageNo();
        }
        
        public System.Threading.Tasks.Task IncreaseMessageNoAsync() {
            return base.Channel.IncreaseMessageNoAsync();
        }
        
        public string GetMessageNo() {
            return base.Channel.GetMessageNo();
        }
        
        public System.Threading.Tasks.Task<string> GetMessageNoAsync() {
            return base.Channel.GetMessageNoAsync();
        }
        
        public void ServerJoin() {
            base.Channel.ServerJoin();
        }
        
        public System.Threading.Tasks.Task ServerJoinAsync() {
            return base.Channel.ServerJoinAsync();
        }
        
        public void ServerLeave() {
            base.Channel.ServerLeave();
        }
        
        public System.Threading.Tasks.Task ServerLeaveAsync() {
            return base.Channel.ServerLeaveAsync();
        }
    }
}
