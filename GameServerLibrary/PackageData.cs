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

using System;

namespace GameServerLibrary
{
    public class PackageData
    {
        public uint protocol { get; set; }

        public Object data { get; set; }

        public PackageData() { }

        public PackageData(PROTOCOL protocol)
        {
            this.protocol = (uint)protocol;
        }

        public PackageData(PROTOCOL protocol, Object data)
        {
            this.protocol = (uint)protocol;
            this.data = data;
        }
    }

    public enum PROTOCOL
    {
        //←　→

        #region //大廳協定
        /// <summary>
        /// C→S  Client端登入
        /// </summary>
        LSV_LOGIN = 0x0000000A,

        /// <summary>
        /// C←S Client端登入-成功
        /// </summary>
        LSV_LOGIN_SUCCESS = 0x0000000B,
        #endregion

        #region //遊戲協定

        /// <summary>
        /// C→S 加入房間
        /// </summary>
        GS_JOIN_ROOM = 0x00010000,

        /// <summary>
        /// C→S 離開房間
        /// </summary>
        GS_EXIT_ROOM = 0x00010010,

        /// <summary>
        /// C←S 開房，傳送玩家資料
        /// </summary>
        GS_INIT_ALL_PLAYER = 0x00010001,

        /// <summary>
        /// C←S 有玩家加入房間
        /// </summary>
        GS_PLAYER_JOIN_ROOM = 0x00010002,

        /// <summary>
        /// C←S 有玩家離開房間
        /// </summary>
        GS_PLAYER_EXIT_ROOM = 0x00010003,

        /// <summary>
        /// C→S 玩家準備
        /// </summary>
        GS_READY_ROOM = 0x00010004,

        /// <summary>
        /// C←S 有玩家準備
        /// </summary>
        GS_PLAYER_READY_ROOM = 0x00010005,

        /// <summary>
        /// C←S 所有玩家準備
        /// </summary>
        GS_LOCK_READY_ROOM = 0x00010006,

        /// <summary>
        /// C→S 開局，傳送遊戲參數
        /// </summary>
        GS_START_GAME = 0x00020000,

        /// <summary>
        /// C→S 玩家更新座標
        /// </summary>
        GS_UPDATE_POSITION = 0x00020001,

        /// <summary>
        /// C←S 傳遞房間內所有玩家座標
        /// </summary>
        GS_REFRESH_POSITION = 0x00020002,

        /// <summary>
        /// C→S 玩家更新狀態
        /// </summary>
        GS_UPDATE_STATUS = 0x00020003,

        /// <summary>
        /// C←S 傳遞房間內所有玩家狀態
        /// </summary>
        GS_REFRESH_STATUS = 0x00020004,

        /// <summary>
        /// C→S 玩家更新子彈
        /// </summary>
        GS_UPDATE_BULLET = 0x00020005,

        /// <summary>
        /// C←S 傳遞房間內所有子彈
        /// </summary>
        GS_REFRESH_BULLET = 0x00020006,

        /// <summary>
        /// C←S 結束遊戲
        /// </summary>
        GS_END_GAME = 0x00030000,

        #endregion
    }
}
