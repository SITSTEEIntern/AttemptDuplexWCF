namespace GPH_QuickMessageServiceServer
{
    partial class MessageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnBroadcast = new System.Windows.Forms.Button();
            this.dgvStatus = new System.Windows.Forms.DataGridView();
            this.txtMessageOutbound = new System.Windows.Forms.TextBox();
            this.btnTarget = new System.Windows.Forms.Button();
            this.cbConnectList = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblServerMsgNo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Send Message:";
            // 
            // btnBroadcast
            // 
            this.btnBroadcast.Location = new System.Drawing.Point(673, 46);
            this.btnBroadcast.Name = "btnBroadcast";
            this.btnBroadcast.Size = new System.Drawing.Size(114, 23);
            this.btnBroadcast.TabIndex = 2;
            this.btnBroadcast.Text = "Broadcast Send";
            this.btnBroadcast.UseVisualStyleBackColor = true;
            this.btnBroadcast.Click += new System.EventHandler(this.btnBroadcast_Click);
            // 
            // dgvStatus
            // 
            this.dgvStatus.Location = new System.Drawing.Point(16, 117);
            this.dgvStatus.Name = "dgvStatus";
            this.dgvStatus.Size = new System.Drawing.Size(642, 186);
            this.dgvStatus.TabIndex = 3;
            this.dgvStatus.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStatus_CellContentClick);
            // 
            // txtMessageOutbound
            // 
            this.txtMessageOutbound.Location = new System.Drawing.Point(16, 44);
            this.txtMessageOutbound.Multiline = true;
            this.txtMessageOutbound.Name = "txtMessageOutbound";
            this.txtMessageOutbound.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageOutbound.Size = new System.Drawing.Size(642, 58);
            this.txtMessageOutbound.TabIndex = 4;
            // 
            // btnTarget
            // 
            this.btnTarget.Location = new System.Drawing.Point(673, 79);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.Size = new System.Drawing.Size(114, 23);
            this.btnTarget.TabIndex = 5;
            this.btnTarget.Text = "Target Send";
            this.btnTarget.UseVisualStyleBackColor = true;
            this.btnTarget.Click += new System.EventHandler(this.btnTarget_Click);
            // 
            // cbConnectList
            // 
            this.cbConnectList.FormattingEnabled = true;
            this.cbConnectList.Location = new System.Drawing.Point(664, 117);
            this.cbConnectList.Name = "cbConnectList";
            this.cbConnectList.Size = new System.Drawing.Size(131, 184);
            this.cbConnectList.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(663, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Msg No:";
            // 
            // lblServerMsgNo
            // 
            this.lblServerMsgNo.AutoSize = true;
            this.lblServerMsgNo.Location = new System.Drawing.Point(723, 19);
            this.lblServerMsgNo.Name = "lblServerMsgNo";
            this.lblServerMsgNo.Size = new System.Drawing.Size(0, 13);
            this.lblServerMsgNo.TabIndex = 8;
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 315);
            this.Controls.Add(this.lblServerMsgNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbConnectList);
            this.Controls.Add(this.btnTarget);
            this.Controls.Add(this.txtMessageOutbound);
            this.Controls.Add(this.dgvStatus);
            this.Controls.Add(this.btnBroadcast);
            this.Controls.Add(this.label1);
            this.Name = "MessageForm";
            this.Text = "Call-Out Server Interface";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessageForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBroadcast;
        private System.Windows.Forms.DataGridView dgvStatus;
        private System.Windows.Forms.TextBox txtMessageOutbound;
        private System.Windows.Forms.Button btnTarget;
        private System.Windows.Forms.CheckedListBox cbConnectList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblServerMsgNo;
    }
}

