namespace GameClientDemo
{
    partial class Client
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbHost = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnJoinRoom = new System.Windows.Forms.Button();
            this.tbUID = new System.Windows.Forms.TextBox();
            this.btnExitRoom = new System.Windows.Forms.Button();
            this.btnUpdatePosition = new System.Windows.Forms.Button();
            this.lbUID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtbSysMsg
            // 
            this.rtbSysMsg.Location = new System.Drawing.Point(12, 52);
            this.rtbSysMsg.Name = "rtbSysMsg";
            this.rtbSysMsg.Size = new System.Drawing.Size(403, 236);
            this.rtbSysMsg.TabIndex = 0;
            this.rtbSysMsg.Text = "";
            this.rtbSysMsg.TextChanged += new System.EventHandler(this.rtbSysMsg_TextChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(267, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "連線";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbHost
            // 
            this.tbHost.Location = new System.Drawing.Point(12, 12);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(100, 22);
            this.tbHost.TabIndex = 2;
            this.tbHost.Text = "127.0.0.1";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(137, 12);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(100, 22);
            this.tbPort.TabIndex = 3;
            this.tbPort.Text = "14242";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(446, 52);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnJoinRoom
            // 
            this.btnJoinRoom.Location = new System.Drawing.Point(446, 96);
            this.btnJoinRoom.Name = "btnJoinRoom";
            this.btnJoinRoom.Size = new System.Drawing.Size(75, 23);
            this.btnJoinRoom.TabIndex = 5;
            this.btnJoinRoom.Text = "JoinRoom";
            this.btnJoinRoom.UseVisualStyleBackColor = true;
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);
            // 
            // tbUID
            // 
            this.tbUID.Location = new System.Drawing.Point(568, 52);
            this.tbUID.Name = "tbUID";
            this.tbUID.Size = new System.Drawing.Size(100, 22);
            this.tbUID.TabIndex = 6;
            // 
            // btnExitRoom
            // 
            this.btnExitRoom.Location = new System.Drawing.Point(446, 137);
            this.btnExitRoom.Name = "btnExitRoom";
            this.btnExitRoom.Size = new System.Drawing.Size(75, 23);
            this.btnExitRoom.TabIndex = 7;
            this.btnExitRoom.Text = "ExitRoom";
            this.btnExitRoom.UseVisualStyleBackColor = true;
            this.btnExitRoom.Click += new System.EventHandler(this.btnExitRoom_Click);
            // 
            // btnUpdatePosition
            // 
            this.btnUpdatePosition.Location = new System.Drawing.Point(446, 181);
            this.btnUpdatePosition.Name = "btnUpdatePosition";
            this.btnUpdatePosition.Size = new System.Drawing.Size(75, 23);
            this.btnUpdatePosition.TabIndex = 8;
            this.btnUpdatePosition.Text = "UpdPosition";
            this.btnUpdatePosition.UseVisualStyleBackColor = true;
            this.btnUpdatePosition.Click += new System.EventHandler(this.btnUpdatePosition_Click);
            // 
            // lbUID
            // 
            this.lbUID.AutoSize = true;
            this.lbUID.Location = new System.Drawing.Point(527, 57);
            this.lbUID.Name = "lbUID";
            this.lbUID.Size = new System.Drawing.Size(35, 12);
            this.lbUID.TabIndex = 9;
            this.lbUID.Text = "UID->";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 311);
            this.Controls.Add(this.lbUID);
            this.Controls.Add(this.btnUpdatePosition);
            this.Controls.Add(this.btnExitRoom);
            this.Controls.Add(this.tbUID);
            this.Controls.Add(this.btnJoinRoom);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbHost);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.rtbSysMsg);
            this.Name = "Client";
            this.Text = "遊戲模擬器";
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbSysMsg;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnJoinRoom;
        private System.Windows.Forms.TextBox tbUID;
        private System.Windows.Forms.Button btnExitRoom;
        private System.Windows.Forms.Button btnUpdatePosition;
        private System.Windows.Forms.Label lbUID;
    }
}

