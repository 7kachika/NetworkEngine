// Copyright 2017 7kachika
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using GameServerLibrary;
using Lidgren.Network;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Windows.Forms;

namespace GameClientDemo
{
    public partial class Client : Form
    {


        private static NetClient client;

        public Client()
        {
            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("game_server");
            config.AutoFlushSendQueue = false;
            client = new NetClient(config);
            client.RegisterReceivedCallback(new SendOrPostCallback(HandleMessage));
        }

        public void HandleMessage(object peer)
        {
            NetIncomingMessage im;
            string message = string.Empty;
            while ((im = client.ReadMessage()) != null)
            {
                // handle incoming message
                switch (im.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.VerboseDebugMessage:
                        string text = im.ReadString();
                        SystemInfo(text);
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)im.ReadByte();
                        if (status == NetConnectionStatus.Connected)
                        {
                            //tbMsg.Enabled = true;
                            btnConnect.Text = "Disconnect";
                        }
                        else
                        {
                            //tbMsg.Enabled = false;
                        }

                        if (status == NetConnectionStatus.Disconnected)
                        {
                            btnConnect.Text = "Connect";
                        }

                        //tbUID
                        string reason = im.ReadString();
                        SystemInfo(status.ToString() + ": " + reason);

                        break;
                    case NetIncomingMessageType.Data:
                        message = im.ReadString();
                        PackageData package = JsonConvert.DeserializeObject<PackageData>(message);
                        SystemInfo("[C<--S] 0x" + package.protocol.ToString("X8") + "[" + ((PROTOCOL)package.protocol).ToString() + "]");
                        switch ((PROTOCOL)package.protocol)
                        {
                            case PROTOCOL.LSV_LOGIN_SUCCESS:
                                clsLoginSuccess login_success = JsonConvert.DeserializeObject<clsLoginSuccess>(package.data.ToString());
                                tbUID.Text = login_success.uid.ToString();
                                break;
                            case PROTOCOL.GS_INIT_ALL_PLAYER:
                                SystemInfo(package.data.ToString());
                                break;
                            case PROTOCOL.GS_PLAYER_JOIN_ROOM:      //有人加入房間
                                SystemInfo(package.data.ToString());
                                break;
                            case PROTOCOL.GS_PLAYER_EXIT_ROOM:       //有人離開房間
                                SystemInfo(package.data.ToString());
                                break;
                            case PROTOCOL.GS_REFRESH_PLAYER_POSITION: //有人移動
                                SystemInfo(package.data.ToString());
                                break;
                        }
                        break;
                    default:
                        SystemInfo("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes");
                        break;
                }
                client.Recycle(im);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (client.ConnectionStatus == NetConnectionStatus.Connected)
            {
                client.Disconnect("User disconnect ~");
            }
            else
            {
                int port;
                Int32.TryParse(tbPort.Text, out port);
                NetOutgoingMessage hail = client.CreateMessage("Hi ~ i m in !!");
                client.Start();
                client.Connect(tbHost.Text.Trim(), port, hail);
            }
        }


        public void SystemInfo(string message)
        {
            if (rtbSysMsg == null) { return; }
            rtbSysMsg.AppendText(message + "\n");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsLogin data = new clsLogin();
            data.username = "admin";
            data.password = "password";
            PackageData package = new PackageData(PROTOCOL.LSV_LOGIN, data);
            SendToServer(package);
        }

        public void SendToServer(PackageData data)
        {
            //啟動json.net 轉換資料
            string json = JsonConvert.SerializeObject(data);
            //送資料出去
            NetOutgoingMessage om = client.CreateMessage(json);
            client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
            SystemInfo("[C-->S] 0x" + data.protocol.ToString("X8") + "\n\t" + json);
            client.FlushSendQueue();
        }

        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            clsJoinRoom data = new clsJoinRoom();
            uint uid = 0;
            uint.TryParse(tbUID.Text, out uid);

            data.uid = uid;
            data.room_id = 1;
            PackageData package = new PackageData(PROTOCOL.GS_JOIN_ROOM, data);
            SendToServer(package);
        }

        private void btnExitRoom_Click(object sender, EventArgs e)
        {
            clsJoinRoom data = new clsJoinRoom();
            uint uid = 0;
            uint.TryParse(tbUID.Text, out uid);

            data.uid = uid;
            data.room_id = 1;
            PackageData package = new PackageData(PROTOCOL.GS_EXIT_ROOM, data);
            SendToServer(package);
        }

        private void btnUpdatePosition_Click(object sender, EventArgs e)
        {
            clsUpdatePosition data = new clsUpdatePosition();
            uint uid = 0;
            uint.TryParse(tbUID.Text, out uid);

            data.uid = uid;
            data.position = new Vec2(0, 0); //模擬移動
            PackageData package = new PackageData(PROTOCOL.GS_UPDATE_POSITION, data);
            SendToServer(package);
        }

        private void rtbSysMsg_TextChanged(object sender, EventArgs e)
        {
            //自動拖曳 到最下面, 參考自
            //http://stackoverflow.com/questions/9416608/rich-text-box-scroll-to-the-bottom-when-new-data-is-written-to-it
            // set the current caret position to the end
            rtbSysMsg.SelectionStart = rtbSysMsg.Text.Length;
            // scroll it automatically
            rtbSysMsg.ScrollToCaret();
        }
    }
}
