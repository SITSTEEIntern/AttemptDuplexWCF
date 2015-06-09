namespace GPH_QuickMessageServiceClient
{
    partial class Form1
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtMessageLog = new System.Windows.Forms.TextBox();
            this.cbID = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblClientMsgNo = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(290, 28);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtMessageLog
            // 
            this.txtMessageLog.BackColor = System.Drawing.Color.Azure;
            this.txtMessageLog.Location = new System.Drawing.Point(33, 78);
            this.txtMessageLog.Multiline = true;
            this.txtMessageLog.Name = "txtMessageLog";
            this.txtMessageLog.ReadOnly = true;
            this.txtMessageLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessageLog.Size = new System.Drawing.Size(332, 142);
            this.txtMessageLog.TabIndex = 4;
            // 
            // cbID
            // 
            this.cbID.FormattingEnabled = true;
            this.cbID.Location = new System.Drawing.Point(57, 30);
            this.cbID.Name = "cbID";
            this.cbID.Size = new System.Drawing.Size(186, 21);
            this.cbID.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Msg No:";
            // 
            // lblClientMsgNo
            // 
            this.lblClientMsgNo.AutoSize = true;
            this.lblClientMsgNo.ForeColor = System.Drawing.Color.Black;
            this.lblClientMsgNo.Location = new System.Drawing.Point(96, 241);
            this.lblClientMsgNo.Name = "lblClientMsgNo";
            this.lblClientMsgNo.Size = new System.Drawing.Size(0, 13);
            this.lblClientMsgNo.TabIndex = 7;
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(198, 236);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 8;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(290, 236);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(75, 23);
            this.btnReject.TabIndex = 9;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(246, 236);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 10;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 275);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblClientMsgNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbID);
            this.Controls.Add(this.txtMessageLog);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Call-Out Service Client Interface";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessageForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtMessageLog;
        private System.Windows.Forms.ComboBox cbID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblClientMsgNo;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnDone;
    }
}

