using System;
using System.Collections.Generic;
using System.ComponentModel; //for bindinglist
using System.Data;
using System.Drawing;
using System.Threading;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics; //for debug
using System.Timers;
// Location of the proxy.
using GPH_QuickMessageServiceServer.ServiceReference1;

namespace GPH_QuickMessageServiceServer
{
    // Specify for the callback to NOT use the current synchronization context
    [CallbackBehavior(
        ConcurrencyMode = ConcurrencyMode.Single,
        UseSynchronizationContext = false)]
    public partial class MessageForm : Form, ServiceReference1.GPH_QuickMessageServiceCallback
    {
        private SynchronizationContext _uiSyncContext = null;
        private ServiceReference1.GPH_QuickMessageServiceClient _GPH_QuickMessageService = null;

        //For binding of list to datagirdview
        BindingList<ConnStatus> connstatuslist = new BindingList<ConnStatus>();

        private static List<System.Drawing.Image> imageList = new List<System.Drawing.Image>();

        //Message Number
        private static string _MessageNo = "";

        public MessageForm()
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

            //Connect server to service
            _GPH_QuickMessageService.ServerJoin();

            // Initial eventhandlers
            this.FormClosing += new FormClosingEventHandler(MessageForm_FormClosing);

            //Add in image for icon
            imageList.Add(Properties.Resources.Greenicon);
            imageList.Add(Properties.Resources.Redicon);
            imageList.Add(Properties.Resources.Yellowicon);
            
            //Message Number
            _MessageNo = _GPH_QuickMessageService.GetMessageNo();
            this.lblServerMsgNo.Text = _MessageNo;

            //Load the connection status and bind to datagridview to show realtime changes
            InitDataGrid();

        }

        private void MessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Terminate the connection to the service.
            _GPH_QuickMessageService.ServerLeave();
            

        }

        private void btnBroadcast_Click(object sender, EventArgs e)
        {
            //Trigger service method to pass msg from server to connected client
            _GPH_QuickMessageService.BroadcastClientMsg(_MessageNo, this.txtMessageOutbound.Text);


            //To toggle the message status 
            foreach (ConnStatus station in connstatuslist)
            {
                //Debug.WriteLine(station.Name);
                if (station.Status.Equals("Connected"))
                {
                    station.MessageNo = _MessageNo;
                    station.Progress = "Pending";
                }
            }

            string sentMsgNo = _MessageNo; // save the number before edited
            //Set Timer to count timeout for reply from client (10 sec)
            System.Timers.Timer ReplyTimer = new System.Timers.Timer();
            ReplyTimer.Interval = 10000; //10 sec
            ReplyTimer.Elapsed += delegate { ReplyTimeout(sentMsgNo); };
            ReplyTimer.AutoReset = false;
            ReplyTimer.Start();

            //_MessageNo++;
            _GPH_QuickMessageService.IncreaseMessageNo();
            _MessageNo = _GPH_QuickMessageService.GetMessageNo();
            lblServerMsgNo.Text = _MessageNo;

        }

        private void btnTarget_Click(object sender, EventArgs e)
        {
            // Forward the message to the service
            //_GPH_QuickMessageService.ReceiveMessage(this.txtName.Text, null, this.txtMessageOutbound.Text);

            string[] addressList = new string[cbConnectList.CheckedItems.Count];
            int i = 0;
            for (int j = 0; j < cbConnectList.CheckedItems.Count; j++)
            {
                addressList[i++] = (string)cbConnectList.CheckedItems[j];
            }
            _GPH_QuickMessageService.TargetClientMsg(addressList,_MessageNo, this.txtMessageOutbound.Text);

            foreach (ConnStatus station in connstatuslist)
            {
                foreach (string stationname in addressList)
                {
                    if (station.Name.Equals(stationname))
                    {
                        station.MessageNo = _MessageNo;
                        station.Progress = "Pending";
                    }
                }
            }

            string sentMsgNo = _MessageNo; // save the number before edited
            //Set Timer to count timeout for reply from client (10 sec)
            System.Timers.Timer ReplyTimer = new System.Timers.Timer();
            ReplyTimer.Interval = 10000; //10 sec
            ReplyTimer.Elapsed += delegate { ReplyTimeout(sentMsgNo); };
            ReplyTimer.AutoReset = false;
            ReplyTimer.Start();

            //_MessageNo++;
            _GPH_QuickMessageService.IncreaseMessageNo();
            _MessageNo = _GPH_QuickMessageService.GetMessageNo();
            lblServerMsgNo.Text = _MessageNo;
        }

        public void ReplyTimeout(string MessageNo)
        {
            foreach (ConnStatus station in connstatuslist)
            {
                //Debug.WriteLine(station.Name);
                if (station.Progress.Equals("Pending") && station.MessageNo.Equals(MessageNo))
                {
                    station.Progress = "Send Fail";
                    //Cancel Message at client side when timeout
                    _GPH_QuickMessageService.ConfirmMsg(station.Name, MessageNo, "Cancel");
                }
            }
        }

        public void InitDataGrid()
        {
            //Initialize the datagridview
            this.dgvStatus.AutoGenerateColumns = false;
            this.dgvStatus.CellBorderStyle = DataGridViewCellBorderStyle.None;

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.DataPropertyName = "Icon";
            imageColumn.HeaderText = "Icon";
            imageColumn.Name = "Icon";
            imageColumn.ReadOnly = true;
            imageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "Name";
            nameColumn.HeaderText = "Name";
            nameColumn.ReadOnly = true;
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.DataPropertyName = "Status";
            statusColumn.HeaderText = "Status";
            statusColumn.ReadOnly = true;
            statusColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn messagenoColumn = new DataGridViewTextBoxColumn();
            messagenoColumn.DataPropertyName = "MessageNo";
            messagenoColumn.HeaderText = "MessageNo";
            messagenoColumn.ReadOnly = true;
            messagenoColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn progressColumn = new DataGridViewTextBoxColumn();
            progressColumn.DataPropertyName = "Progress";
            progressColumn.HeaderText = "Progress";
            progressColumn.ReadOnly = true;
            progressColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //Confirm and Cancel button will only be out when client accept the request
            DataGridViewTextBoxColumn confirmColumn = new DataGridViewTextBoxColumn();
            confirmColumn.DataPropertyName = "Confirm";
            confirmColumn.Name = "Confirm";
            confirmColumn.HeaderText = "Confirm";
            confirmColumn.ReadOnly = true;
            confirmColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn cancelColumn = new DataGridViewTextBoxColumn();
            cancelColumn.DataPropertyName = "Cancel";
            cancelColumn.Name = "Cancel";
            cancelColumn.HeaderText = "Cancel";
            cancelColumn.ReadOnly = true;
            cancelColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            this.dgvStatus.Columns.Add(imageColumn);
            this.dgvStatus.Columns.Add(nameColumn);
            this.dgvStatus.Columns.Add(statusColumn);
            this.dgvStatus.Columns.Add(messagenoColumn);
            this.dgvStatus.Columns.Add(progressColumn);
            this.dgvStatus.Columns.Add(confirmColumn);
            this.dgvStatus.Columns.Add(cancelColumn);

            connstatuslist.Clear();
            foreach (KeyValuePair<string, string> station in _GPH_QuickMessageService.GetStationList())
            {
                //Debug.WriteLine(name);
                if (station.Value.Equals("Connected"))
                {
                    connstatuslist.Add(new ConnStatus(imageList[0], station.Key, station.Value,"", ""));
                }
                else
                {
                    connstatuslist.Add(new ConnStatus(imageList[1], station.Key, station.Value, "", ""));
                }
            }

            this.dgvStatus.DataSource = connstatuslist;
        }

        //Displaying of content on checkboxlist
        private void ShowUserList(string[] _SubscriberList)
        {
            //Check whether the method is running on the current thread, 
            //if method is on the other thread, invoke is required to trigger the method

            if (cbConnectList.InvokeRequired)
            {
                cbConnectList.BeginInvoke(new MethodInvoker(delegate { cbConnectList.Items.Clear(); }));

                foreach (string _subscriber in _SubscriberList)
                {
                    cbConnectList.BeginInvoke(new MethodInvoker(delegate { cbConnectList.Items.Add(_subscriber); }));
                }
            }
            else
            {
                cbConnectList.Items.Clear();
                foreach (string _subscriber in _SubscriberList)
                {
                    cbConnectList.Items.Add(_subscriber);
                }
            }
        }

        //To trigger event whe confirm and cancel button click
        private void dgvStatus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //When confirm button press
            if (this.dgvStatus.Columns[e.ColumnIndex].Name.Equals("Confirm"))
            {
                //parameter taken: ConfirmMsg(Name, MessageNumber, Server confirm or cancel)
                _GPH_QuickMessageService.ConfirmMsg(this.dgvStatus.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    this.dgvStatus.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    "Confirm");

                //Remove the confirm and cancel button

                DataGridViewTextBoxCell txtCell1 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell txtCell2 = new DataGridViewTextBoxCell();
                this.dgvStatus.Rows[e.RowIndex].Cells[e.ColumnIndex] = txtCell1;
                this.dgvStatus.Rows[e.RowIndex].Cells[e.ColumnIndex + 1] = txtCell2;
                txtCell1.ReadOnly = true;
                txtCell2.ReadOnly = true;
            }

            //When cancel button press
            if (this.dgvStatus.Columns[e.ColumnIndex].Name.Equals("Cancel"))
            {
                _GPH_QuickMessageService.ConfirmMsg(this.dgvStatus.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    this.dgvStatus.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    "Cancel");

                //Remove the confirm and cancel button
                DataGridViewTextBoxCell txtCell1 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell txtCell2 = new DataGridViewTextBoxCell();
                this.dgvStatus.Rows[e.RowIndex].Cells[e.ColumnIndex] = txtCell1;
                this.dgvStatus.Rows[e.RowIndex].Cells[e.ColumnIndex - 1] = txtCell2;
                txtCell1.ReadOnly = true;
                txtCell2.ReadOnly = true;
            }
        }


        #region GPH_QuickMessageServiceCallback Methods

        public void NotifyCurrentConnList(string arg_Name, string[] ConnectedClientList)
        {
            if (ConnectedClientList.Count() >= 0)
                ShowUserList(ConnectedClientList);
        }
        public void NotifyUserOfMessage(string arg_Name,string arg_MessageNo, string arg_Message)
        {}

        public void EditConnStatus(string Name, string Status)
        {
            //To toggle the status 
            foreach(ConnStatus station in connstatuslist){
                //Debug.WriteLine(station.Name);
                if (station.Name.Equals(Name))
                {
                    station.Status = Status;
                    if (Status.Equals("Connected"))
                    {
                        station.Icon = imageList[0]; //Green icon
                        station.MessageNo = "";
                        station.Progress = "";
                    }
                    else if (Status.Equals("Disconnected"))
                    {
                        station.Icon = imageList[1]; //Red Icon
                        station.MessageNo = "";
                        station.Progress = "";
                    }
                    else
                    {
                        station.Icon = imageList[2]; //Yellow Icon
                    }
                }
            }
        }

        public void EditMessageStatus(string Name, string MessageNo, string Progress) 
        {
            //To trigger message status 
            foreach (ConnStatus station in connstatuslist)
            {
                //Debug.WriteLine(station.Name);
                if (station.Name.Equals(Name))
                {
                    station.MessageNo = MessageNo;
                    station.Progress = Progress;
                }
            }

            //Attempt to create button
            for (int i = 0; i < this.dgvStatus.RowCount - 1; i++)
            {
                if (this.dgvStatus.Rows[i].Cells[4].Value.ToString().Equals("Accept") && this.dgvStatus.Rows[i].Cells[2].Value.ToString().Equals("Connected"))
                {
                    var btnConfirm = new DataGridViewButtonCell();
                    btnConfirm.Value = "Confirm";
                    var btnCancel = new DataGridViewButtonCell();
                    btnCancel.Value = "Cancel";
                    //Adding button to the textbox field
                    this.dgvStatus.Rows[i].Cells[5] = btnConfirm;
                    this.dgvStatus.Rows[i].Cells[6] = btnCancel;
                }
            }
        }

        public void NotifyUserOfConfirmMessage(string userName, string MessageNo, string userMessage, string serverConfirm)
        { }

        #endregion

    }

    public class ConnStatus : INotifyPropertyChanged
    {
        private Image _img;
        private string _name;
        private string _connstatus;
        private string _messageno;
        private string _sendstatus;
        public event PropertyChangedEventHandler PropertyChanged;

        public ConnStatus(Image img, string name, string connstatus,string messageno, string sendstatus)
        {
            _img = img;
            _name = name;
            _connstatus = connstatus;
            _messageno = messageno;
            _sendstatus = sendstatus;
        }

        public Image Icon
        {
            get { return _img; }
            set
            {
                _img = value;
                this.NotifyPropertyChanged("Icon");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        public string Status
        {
            get { return _connstatus; }
            set
            {
                _connstatus = value;
                this.NotifyPropertyChanged("Status");
            }
        }

        public string MessageNo
        {
            get { return _messageno; }
            set
            {
                _messageno = value;
                this.NotifyPropertyChanged("MessageNo");
            }
        }

        public string Progress
        {
            get { return _sendstatus; }
            set
            {
                _sendstatus = value;
                this.NotifyPropertyChanged("Progress");
            }
        }

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

}
