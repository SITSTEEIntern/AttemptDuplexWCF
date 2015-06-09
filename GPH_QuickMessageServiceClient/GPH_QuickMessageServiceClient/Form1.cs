using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics; //for debug
// Location of the proxy.
using GPH_QuickMessageServiceClient.ServiceReference1;

namespace GPH_QuickMessageServiceClient
{
    // Specify for the callback to NOT use the current synchronization context
    [CallbackBehavior(
        ConcurrencyMode = ConcurrencyMode.Single,
        UseSynchronizationContext = false)]
    public partial class Form1 : Form, ServiceReference1.GPH_QuickMessageServiceCallback
    {
        //Declaration
        private SynchronizationContext _uiSyncContext = null;
        private ServiceReference1.GPH_QuickMessageServiceClient _GPH_QuickMessageService = null;

        bool isConnected = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Capture the UI synchronization context
            _uiSyncContext = SynchronizationContext.Current;

            // The client callback interface must be hosted for the server to invoke the callback
            // Open a connection to the message service via the proxy (qualifier ServiceReference1 needed due to name clash)
            _GPH_QuickMessageService = new ServiceReference1.GPH_QuickMessageServiceClient(new InstanceContext(this), "WSDualHttpBinding_GPH_QuickMessageService");
            _GPH_QuickMessageService.Open();

            // Initial eventhandlers
            this.FormClosing += new FormClosingEventHandler(MessageForm_FormClosing);

            // Load combobox
            cbID.Text = "-Select your ID-";
            foreach (KeyValuePair<string,string> name in _GPH_QuickMessageService.GetStationList())
            {
                cbID.Items.Add(name.Key);
            }

            //hide accept and reject button
            btnAccept.Visible = false;
            btnReject.Visible = false;
            btnDone.Visible = false;
        }

        private void MessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Terminate the connection to the service.
            if (isConnected)
            {
                _GPH_QuickMessageService.Close();
            }

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                // Let the service know that this user is leaving
                _GPH_QuickMessageService.LeaveTheConversation(this.cbID.Text);

                //Toggle button display
                isConnected = false;
                btnConnect.Text = "Connect";
                cbID.Enabled = true;
            }
            else
            {
                //Sub from service
                if (_GPH_QuickMessageService == null)
                {
                    _GPH_QuickMessageService = new ServiceReference1.GPH_QuickMessageServiceClient(new InstanceContext(this), "WSDualHttpBinding_GPH_QuickMessageService");
                    _GPH_QuickMessageService.Open();
                }

                //contact the service.
                _GPH_QuickMessageService.JoinTheConversation(this.cbID.Text);

                //Toggle button display
                isConnected = true;
                btnConnect.Text = "Disconnect";
                cbID.Enabled = false;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            _GPH_QuickMessageService.ReplyServerMsg(this.cbID.Text, this.lblClientMsgNo.Text, "Accept");
            btnAccept.Visible = false;
            btnReject.Visible = false;
            
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            _GPH_QuickMessageService.ReplyServerMsg(this.cbID.Text, this.lblClientMsgNo.Text, "Reject");
            lblClientMsgNo.Text = "";
            btnAccept.Visible = false;
            btnReject.Visible = false;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            _GPH_QuickMessageService.TaskCompleted(this.cbID.Text);
            lblClientMsgNo.Text = "";
            btnDone.Visible = false;
        }


        private void WriteMessage(string message)
        {
            //Formating of message
            string format = this.txtMessageLog.Text.Length > 0 ? "{0}\r\n{1} {2}" : "{0}{1} {2}";
            this.txtMessageLog.Text = String.Format(format, this.txtMessageLog.Text, DateTime.Now.ToShortTimeString(), message);
            this.txtMessageLog.SelectionStart = this.txtMessageLog.Text.Length - 1;
            this.txtMessageLog.ScrollToCaret();
        }


        //As it is interface had to implement all even thou not used
        #region GPH_QuickMessageServiceCallback Methods

        public void NotifyCurrentConnList(string arg_Name, string[] ConnectedClientList)
        {}

        public void NotifyUserOfMessage(string arg_Name, string arg_MessageNo, string arg_Message)
        {
            // The UI thread won't be handling the callback, but it is the only one allowed to update the controls.  
            // So, we will dispatch the UI update back to the UI sync context.
            
            //Update Message Number (cross thread therefore need invoke)
            if (this.lblClientMsgNo.InvokeRequired)
            {
                this.lblClientMsgNo.BeginInvoke((MethodInvoker)delegate() 
                {
                    this.lblClientMsgNo.Text = arg_MessageNo;
                });
            }
            else
            {
                this.lblClientMsgNo.Text = arg_MessageNo; 
            }

            //show accept and reject button
            if (this.btnAccept.InvokeRequired)
            {
                this.btnAccept.BeginInvoke((MethodInvoker)delegate()
                {
                    btnAccept.Visible = true;
            
                });
            }
            else
            {
                btnAccept.Visible = true;
            }

            if (this.btnReject.InvokeRequired)
            {
                this.btnReject.BeginInvoke((MethodInvoker)delegate()
                {
                    btnReject.Visible = true;

                });
            }
            else
            {
                btnReject.Visible = true;
            }


            SendOrPostCallback callback =
                delegate(object state)
                {
                    this.WriteMessage(String.Format("[{0}]: {1}", arg_Name.ToUpper(), arg_Message));
                };

            _uiSyncContext.Post(callback, arg_Name);
        }

        public void EditConnStatus(string Name, string Status)
        {}

        public void EditMessageStatus(string Name, string MessageNo, string Progress) 
        {}

        public void NotifyUserOfConfirmMessage(string arg_Name, string arg_MessageNo, string arg_Message, string serverConfirm)
        {
            //Update Message Number (cross thread therefore need invoke)
            if (this.lblClientMsgNo.InvokeRequired)
            {
                this.lblClientMsgNo.BeginInvoke((MethodInvoker)delegate()
                {
                    this.lblClientMsgNo.Text = arg_MessageNo;
                });
            }
            else
            {
                this.lblClientMsgNo.Text = arg_MessageNo;
            }

            //toggle accept button
            if (this.btnAccept.InvokeRequired)
            {
                this.btnAccept.BeginInvoke((MethodInvoker)delegate()
                {
                    btnAccept.Visible = false;

                });
            }
            else
            {
                btnAccept.Visible = false;
            }
            //toggle reject button
            if (this.btnReject.InvokeRequired)
            {
                this.btnReject.BeginInvoke((MethodInvoker)delegate()
                {
                    btnReject.Visible = false;

                });
            }
            else
            {
                btnReject.Visible = false;
            }

            //toggle done button
            if(serverConfirm.Equals("Confirm"))
            {
                if (this.btnDone.InvokeRequired)
                {
                    this.btnDone.BeginInvoke((MethodInvoker)delegate()
                    {
                        btnDone.Visible = true;

                    });
                }
                else
                {
                    btnDone.Visible = true;
                }
            }

            SendOrPostCallback callback =
                delegate(object state)
                {
                    this.WriteMessage(String.Format("[{0}]: {1}", arg_Name.ToUpper(), arg_Message));
                };

            _uiSyncContext.Post(callback, arg_Name);
        }

        #endregion  

        
    }
}
