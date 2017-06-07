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
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace GameServer
{
    public partial class GameServer : Form
    {
        private NetServer server;
        NetPeerConfiguration config;
        private bool startGame = false;

        public static GameServer instance;
        public GameServer()
        {
            if (instance == null) { instance = this; }
            InitializeComponent();
        }
        private void GameServer_Load(object sender, EventArgs e)
        {


        }

        private void GameServer_Shown(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbSysMsg.Clear();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                //轉換port
                int port;
                Int32.TryParse(tbPort.Text, out port);
                config = new NetPeerConfiguration("game_server");
                config.MaximumConnections = 100;
                config.Port = port;
                server = new NetServer(config);
                server.RegisterReceivedCallback(new SendOrPostCallback(HandleMessage));

                server.Start();
                btnConnect.Text = "斷線";

            }
            else
            {
                server.UnregisterReceivedCallback(HandleMessage);
                server.Shutdown("Server shut down ");
                server = null;

                lbUsers.Items.Clear();
                SystemInfo("Shut Down Server ...");
                btnConnect.Text = "連線";
            }
        }


        public void HandleMessage(object peer)
        {
            NetIncomingMessage im;
            string message = string.Empty;
            ServerHandler handler;
            while ((im = server.ReadMessage()) != null)
            {
                handler = new ServerHandler(im.SenderConnection);
                // handle incoming message
                switch (im.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.VerboseDebugMessage:
                        message = im.ReadString();
                        SystemInfo(message);
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)im.ReadByte();

                        message = im.ReadString();
                        SystemInfo(NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " " + status + ": " + message);

                        if (status == NetConnectionStatus.Connected)
                            SystemInfo("Remote hail: " + im.SenderConnection.RemoteHailMessage.ReadString() + " from " + im.SenderEndPoint.ToString());

                        //UpdateConnectionsList();
                        break;
                    case NetIncomingMessageType.Data:
                        message = im.ReadString();
                        PackageData package = JsonConvert.DeserializeObject<PackageData>(message);
                        SystemInfo("[C-->S] 0x" + package.protocol.ToString("X8"));
                        switch ((PROTOCOL)package.protocol)
                        {
                            case PROTOCOL.LSV_LOGIN:
                                handler.LoginHandler(package.data);
                                break;

                            case PROTOCOL.GS_JOIN_ROOM:
                                handler.JoinRoomHandler(package.data);
                                UpdateRoomList(1);
                                break;

                            case PROTOCOL.GS_EXIT_ROOM:
                                handler.ExitRoomHandler(package.data);
                                UpdateRoomList(1);
                                break;

                            case PROTOCOL.GS_READY_ROOM:
                                handler.ReadyRoomHandler(package.data);
                                break;

                            case PROTOCOL.GS_UPDATE_POSITION:
                                handler.UpdatePositionHandler(package.data);
                                break;

                            case PROTOCOL.GS_UPDATE_STATUS:
                                handler.UpdateStatusHandler(package.data);
                                break;

                            case PROTOCOL.GS_UPDATE_BULLET:
                                handler.UpdateBulletHandler(package.data);
                                break;
                        }
                        break;
                    default:
                        SystemInfo("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes " + im.DeliveryMethod + "|" + im.SequenceChannel);
                        break;
                }
                server.Recycle(im);
                if(startGame) { handler.StartGameHandler(); }
                Thread.Sleep(1);
            }
        }




        public void SendToClient(NetConnection client, PackageData data)
        {
            string json = JsonConvert.SerializeObject(data);
            NetOutgoingMessage om = server.CreateMessage();
            om.Write(json);
            SystemInfo("[C<--S] 0x" + data.protocol.ToString("X8") + " to " + client.RemoteEndPoint);
            server.SendMessage(om, client, NetDeliveryMethod.ReliableOrdered, 0);
        }

        public void SendToAllClient(List<NetConnection> clients, PackageData data)
        {
            string json = JsonConvert.SerializeObject(data);
            NetOutgoingMessage om = server.CreateMessage();
            om.Write(json);
            SystemInfo("Broadcasting '" + json + "'");
            server.SendMessage(om, clients, NetDeliveryMethod.ReliableOrdered, 0);
        }


        public void SendToAllClient(List<Player> player_list, PackageData data)
        {

            List<NetConnection> clients = new List<NetConnection>();
            foreach (Player player in player_list)
            {
                clients.Add(player.connection);
            }

            string json = JsonConvert.SerializeObject(data);
            NetOutgoingMessage om = server.CreateMessage();
            om.Write(json);
            SystemInfo("Broadcasting '" + json + "'");
            server.SendMessage(om, clients, NetDeliveryMethod.ReliableOrdered, 0);
        }

        public void UpdateConnectionsList()
        {
            lbUsers.Items.Clear();

            foreach (NetConnection conn in server.Connections)
            {
                string str = NetUtility.ToHexString(conn.RemoteUniqueIdentifier) + " from " + conn.RemoteEndPoint.ToString() + " [" + conn.Status + "]";
                lbUsers.Items.Add(str);
            }
        }

        public void UpdateRoomList(uint room_id)
        {
            lbRoom1.Items.Clear();
            clsOpenRoom room_data = DataPool.rooms[room_id];
            foreach (Player play_info in room_data.player)
            {
                lbRoom1.Items.Add(play_info.token);
            }
        }

        public void SystemInfo(string message)
        {
            if (rtbSysMsg == null) { return; }
            rtbSysMsg.AppendText(message + "\n");

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
