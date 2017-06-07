using GameServerLibrary;
using Lidgren.Network;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameServer
{
    public class ServerHandler
    {
        NetConnection source = null;

        public ServerHandler(NetConnection source)
        {
            this.source = source;
        }

        public void LoginHandler(Object data)
        {
            // 判斷玩家數量
            if (DataPool.users.Count >= 4)
            {
                // TODO: 玩家已滿
                return;
            }

            // 新增玩家
            Player newPlayer = new Player();
            newPlayer.token = (uint)new Random().Next(100);
            newPlayer.name = newPlayer.token.ToString();

            // 將自己的連線存起來
            newPlayer.connection = source;

            // 加入cache
            DataPool.users[newPlayer.token] = newPlayer;

            clsLoginSuccess info = new clsLoginSuccess(newPlayer.token);
            PackageData package = new PackageData(PROTOCOL.LSV_LOGIN_SUCCESS, info);
            GameServer.instance.SendToClient(source, package);
        }

        /// <summary>
        /// 加入房間
        /// </summary>
        /// <param name="data"></param>
        public void JoinRoomHandler(Object data)
        {
            clsJoinRoom join_info = JsonConvert.DeserializeObject<clsJoinRoom>(data.ToString());

            if (join_info.room_id != 1)
            {
                // 加入失敗
            }

            #region //嘗試取得該房間中的資料
            clsOpenRoom room_info = null;
            if (!DataPool.rooms.TryGetValue(join_info.room_id, out room_info))
            {
                room_info = new clsOpenRoom();
                room_info.map_id = 1;
                //room_info.map_id = new Random().Next(10);  // 隨機分配地圖
                room_info.player = new List<Player>();

            }
            #endregion

            room_info.player.Add(DataPool.users[join_info.uid]); // 新增user 進房間

            #region //回寫資料
            if (DataPool.rooms.ContainsKey(join_info.room_id))
            {
                DataPool.rooms[join_info.room_id] = room_info; // 修改
            }
            else
            {
                DataPool.rooms.Add(join_info.room_id, room_info);  // 新增
            }
            #endregion

            //根據pool 的資料更新輸出的封包

            PackageData package = new PackageData(PROTOCOL.GS_INIT_ALL_PLAYER, room_info);
            GameServer.instance.SendToClient(source, package);

            if (room_info.player.Count > 1)
            {
                //利用linq 語法過濾資料
                List<Player> player_except_myself = room_info.player.Where(x => x.token != join_info.uid).ToList();
                Player myself_info = room_info.player.Where(x => x.token == join_info.uid).First();

                package = new PackageData(PROTOCOL.GS_PLAYER_JOIN_ROOM, myself_info);
                GameServer.instance.SendToAllClient(player_except_myself, package);
            }
        }

        /// <summary>
        /// 離開房間
        /// </summary>
        /// <param name="data"></param>
        public void ExitRoomHandler(Object data)
        {
            clsJoinRoom join_info = JsonConvert.DeserializeObject<clsJoinRoom>(data.ToString());

            //掃描是否存在所指定的房間內
            clsOpenRoom room_info = null;
            if (!DataPool.rooms.TryGetValue(join_info.room_id, out room_info))
            {
                //TODO: 處理錯誤!! 資料不存在
            }

            foreach (Player player in room_info.player)
            {
                if (player.token == join_info.uid)
                {
                    room_info.player.Remove(player);
                    break;
                }
            }

            //廣播某人離開房間 
            PackageData package = new PackageData(PROTOCOL.GS_PLAYER_EXIT_ROOM, join_info);
            GameServer.instance.SendToAllClient(room_info.player, package);
        }

        /// <summary>
        /// 準備房間
        /// </summary>
        /// <param name="data"></param>
        public void ReadyRoomHandler(Object data)
        {
            clsReadyRoom ready_info = JsonConvert.DeserializeObject<clsReadyRoom>(data.ToString());
            clsReadyRoom ready_all = new clsReadyRoom();
            ready_all.ready = true;
            foreach (clsReadyRoom ready in DataPool.readys)
            {
                if (ready.uid == ready_info.uid) { ready.ready = ready_info.ready; }
                if (!ready.ready) { ready_all.ready = false; }
            }
            clsOpenRoom room_info = null;
            if (!DataPool.rooms.TryGetValue(1, out room_info))
            {
                //TODO:資料錯誤
            }
            List<Player> player_except_myself = room_info.player.ToList();
            PackageData package = new PackageData(PROTOCOL.GS_READY_ROOM, DataPool.readys);
            GameServer.instance.SendToAllClient(room_info.player, package);
            if (ready_all.ready)
            {
                LockReadyRoomHandler(data);
            }
        }

        /// <summary>
        /// 鎖定準備房間
        /// </summary>
        /// <param name="data"></param>
        public void LockReadyRoomHandler(Object data)
        {
            DataPool.readyLock.locked = true;
            clsOpenRoom room_info = null;
            if (!DataPool.rooms.TryGetValue(1, out room_info))
            {
                //TODO:資料錯誤
            }
            List<Player> player_except_myself = room_info.player.ToList();
            PackageData package = new PackageData(PROTOCOL.GS_LOCK_READY_ROOM, DataPool.readyLock);
            GameServer.instance.SendToAllClient(room_info.player, package);
            StartGameHandler();
        }

        public void StartGameHandler()
        {
            clsStartGame startGame = new clsStartGame();
            startGame.users = DataPool.users;
            startGame.gamePlay = DataPool.gamePlay;
            DataPool.gameStart = true;

            clsOpenRoom room_info = null;
            if (!DataPool.rooms.TryGetValue(1, out room_info))
            {
                //TODO:資料錯誤
            }
            List<Player> player_except_myself = room_info.player.ToList();
            PackageData package = new PackageData(PROTOCOL.GS_START_GAME, startGame);
            GameServer.instance.SendToAllClient(room_info.player, package);
        }

        public void UpdatePositionHandler(Object data)
        {
            clsUpdatePosition update_data = JsonConvert.DeserializeObject<clsUpdatePosition>(data.ToString());

            Player player_info = DataPool.users[update_data.uid];
            player_info.position = update_data.position;
            player_info.theta = update_data.theta;

            DataPool.users[update_data.uid] = player_info;
            //TODO: 可以優化
            clsOpenRoom room_info = null;
            if (!DataPool.rooms.TryGetValue(1, out room_info))
            {
                //TODO:資料錯誤
            }
            List<Player> player_except_myself = room_info.player.ToList();
            PackageData package = new PackageData(PROTOCOL.GS_REFRESH_POSITION, update_data);
            GameServer.instance.SendToAllClient(player_except_myself, package);
        }

        public void UpdateStatusHandler(Object data)
        {
            clsUpdateStatus point = JsonConvert.DeserializeObject<clsUpdateStatus>(data.ToString());
            DataPool.users[point.uid].attack = point.attack;

            //TODO: 可以優化
            //clsOpenRoom room_info = null;
            //if (!DataPool.rooms.TryGetValue(1, out room_info))
            //{
            //    //TODO:資料錯誤
            //}
            //List<Player> player_except_myself = room_info.player.ToList();
            //PackageData package = new PackageData(PROTOCOL.GS_REFRESH_STATUS, point);
            //GameServer.instance.SendToAllClient(player_except_myself, package);
        }

        public void UpdateBulletHandler(Object data)
        {
            Bullet shot = JsonConvert.DeserializeObject<Bullet>(data.ToString());
            DataPool.shots[shot.token] = shot;

            //TODO: 可以優化
            clsOpenRoom room_info = null;
            if (!DataPool.rooms.TryGetValue(1, out room_info))
            {
                //TODO:資料錯誤
            }
            List<Player> player_except_myself = room_info.player.ToList();
            PackageData package = new PackageData(PROTOCOL.GS_REFRESH_BULLET, shot);
            GameServer.instance.SendToAllClient(player_except_myself, package);
        }

        public void EndGameHandler(Object data)
        {
            clsEndGame endGame = JsonConvert.DeserializeObject<clsEndGame>(data.ToString());
            endGame.endGame = true;

            //TODO: 可以優化
            clsOpenRoom room_info = null;
            if (!DataPool.rooms.TryGetValue(1, out room_info))
            {
                //TODO:資料錯誤
            }
            List<Player> player_except_myself = room_info.player.ToList();
            PackageData package = new PackageData(PROTOCOL.GS_END_GAME, endGame);
            GameServer.instance.SendToAllClient(player_except_myself, package);
        }
    }
}
