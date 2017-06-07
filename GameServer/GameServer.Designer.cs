namespace GameServer
{
    partial class GameServer
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbSysMsg = new System.Windows.Forms.RichTextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.lbPort = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbRoom1 = new System.Windows.Forms.ListBox();
            this.gbRoom1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.gbRoom1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbSysMsg
            // 
            this.rtbSysMsg.Location = new System.Drawing.Point(12, 115);
            this.rtbSysMsg.Name = "rtbSysMsg";
            this.rtbSysMsg.Size = new System.Drawing.Size(554, 178);
            this.rtbSysMsg.TabIndex = 0;
            this.rtbSysMsg.Text = "\n";
            this.rtbSysMsg.TextChanged += new System.EventHandler(this.rtbSysMsg_TextChanged);
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(605, 128);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(75, 22);
            this.tbPort.TabIndex = 1;
            this.tbPort.Text = "14242";
            // 
            // lbPort
            // 
            this.lbPort.AutoSize = true;
            this.lbPort.Location = new System.Drawing.Point(572, 131);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(27, 12);
            this.lbPort.TabIndex = 2;
            this.lbPort.Text = "Port:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(605, 156);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "連線";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lbUsers
            // 
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.ItemHeight = 12;
            this.lbUsers.Location = new System.Drawing.Point(12, 12);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(554, 88);
            this.lbUsers.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(12, 299);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "清除資料";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbRoom1
            // 
            this.lbRoom1.FormattingEnabled = true;
            this.lbRoom1.ItemHeight = 12;
            this.lbRoom1.Location = new System.Drawing.Point(16, 21);
            this.lbRoom1.Name = "lbRoom1";
            this.lbRoom1.Size = new System.Drawing.Size(179, 112);
            this.lbRoom1.TabIndex = 6;
            // 
            // gbRoom1
            // 
            this.gbRoom1.Controls.Add(this.lbRoom1);
            this.gbRoom1.Location = new System.Drawing.Point(12, 340);
            this.gbRoom1.Name = "gbRoom1";
            this.gbRoom1.Size = new System.Drawing.Size(209, 147);
            this.gbRoom1.TabIndex = 7;
            this.gbRoom1.TabStop = false;
            this.gbRoom1.Text = "Room1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(605, 186);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // GameServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 494);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gbRoom1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lbUsers);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lbPort);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.rtbSysMsg);
            this.Name = "GameServer";
            this.Text = "GameServer";
            this.Load += new System.EventHandler(this.GameServer_Load);
            this.Shown += new System.EventHandler(this.GameServer_Shown);
            this.gbRoom1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbSysMsg;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListBox lbRoom1;
        private System.Windows.Forms.GroupBox gbRoom1;
        private System.Windows.Forms.Button button1;
    }
}

