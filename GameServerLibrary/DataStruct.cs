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

using Lidgren.Network;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameServerLibrary
{
    #region -- 資料結構 --
    /// <summary>
    /// 玩家
    /// </summary>
    public class Player
    {
        // 玩家編號
        public uint token { get; set; }

        // 玩家暱稱
        public string name { get; set; }

        // 玩家角色
        public float skin { get; set; }

        // 玩家武器
        public float weapon { get; set; }

        // 玩家子彈
        public float bullet { get; set; }

        // 玩家勝利回合數
        public int win { get; set; }

        // 玩家擊中數量
        public int attack { get; set; }

        // 玩家被擊中數量
        public int hit { get; set; }

        // 玩家位置(世界座標)
        public Vec2 position { get; set; }

        // 玩家朝向，利用角度計算角色動畫(Direction)，武器同樣參考角度
        public float theta { get; set; }

        public Player()
        {
            token = 0;
            name = "npc";
            skin = 0;
            weapon = 0;
            bullet = 0;
            win = 0;
            attack = 0;
            hit = 0;
            position = new Vec2(0, 0);
            theta = 0;
            //player = null;
        }

        public Player(Player copy)
        {
            token = copy.token;
            name = copy.name;
            skin = copy.skin;
            weapon = copy.weapon;
            bullet = copy.bullet;
            win = copy.win;
            attack = copy.attack;
            hit = copy.hit;
            position = copy.position;
            theta = copy.theta;
            //player = copy.player;
        }

        public static Player fake()
        {
            Player fake_Character = new Player();
            return fake_Character;
        }

        [JsonIgnore]
        public NetConnection connection { get; set; }   //不讓json.net 輸出, 僅提供給引擎使用
    }

    /// <summary>
    /// 子彈
    /// </summary>
    public class Bullet
    {
        // 子彈編號
        public uint token { get; set; }

        // 玩家ID
        public uint uid { get; set; }

        // 發射角度
        public float theta { get; set; }

        public Bullet()
        {
            token = 0;
            uid = 0;
            theta = 0;
            //bullet = null;
        }

        public Bullet(Bullet copy)
        {
            token = copy.token;
            uid = copy.uid;
            theta = copy.theta;
            //bullet = copy.bullet;
        }

        public static Bullet fake()
        {
            Bullet fake_Bullet = new Bullet();
            return fake_Bullet;
        }
    }

    /// <summary>
    /// 遊戲參數
    /// </summary>
    public class GamePlay
    {
        // 玩家預載數量
        public int objPoolPlayerCount { get; set; }

        // 子彈預載數量
        public int objPoolBulletCount { get; set; }

        // 單位移動時間
        public float moveTime { get; set; }

        // 遊戲地圖
        public uint map_id { get; set; }

        // 遊戲回合
        public int round { get; set; }

        // 角色移動速度
        public int move { get; set; }

        // 子彈移動速度
        public int speed { get; set; }

        // 子彈射擊間隔
        public float cooldown { get; set; }

        // 遊戲時間
        public int time { get; set; }

        public GamePlay()
        {
            objPoolPlayerCount = 0;
            objPoolBulletCount = 0;
            moveTime = .1f;
            round = 0;
            move = 0;
            speed = 0;
            cooldown = 0;
            time = 0;
        }
    }

    /// <summary>
    /// 地圖
    /// </summary>
    public class Map
    {
        // 編號
        public uint id { get; set; }

        // 地圖
        public List<Board> list { get; set; }

        public Map() { }

        public Map(List<Board> list)
        {
            this.list = list;
        }
    }

    /// <summary>
    /// 地圖物件
    /// </summary>
    public class Board
    {
        // 編號
        public uint token { get; set; }

        // 座標
        public Vec2 position { get; set; }

        // 屬性，道具、障礙物、牆壁
        public int type { get; set; }

        // 圖片材質
        public string sprite { get; set; }

        public Board()
        {
            token = 0;
            position = new Vec2(0, 0);
            type = 0;
            sprite = "";
        }

        public Board(Vec2 position, int type, string sprite)
        {
            this.position = position;
            this.type = type;
            this.sprite = sprite;
        }
    }

    /// <summary>
    /// Vector2
    /// </summary>
    public class Vec2
    {
        // Vector2.x
        public float x { get; set; }

        // Vector2.y
        public float y { get; set; }

        public Vec2()
        {
            x = .0f;
            y = .0f;
        }

        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vec2 operator +(Vec2 r, Vec2 l)
        {
            return new Vec2(r.x + l.x, r.y + l.y);
        }

        public static Vec2 operator *(Vec2 r, float l)
        {
            return new Vec2(r.x * l, r.y * l);
        }
    }

    /// <summary>
    /// LSV_LOGIN
    /// </summary>
    public class clsLogin
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    /// <summary>
    /// LSV_LOGIN_SUCCESS
    /// </summary>
    public class clsLoginSuccess
    {
        public uint uid { get; set; }

        public clsLoginSuccess(uint uid) { this.uid = uid; }
    }

    /// <summary>
    /// GS_JOIN_ROOM
    /// </summary>
    public class clsJoinRoom
    {
        public uint uid { get; set; }
        public uint room_id { get; set; }
    }

    /// <summary>
    /// GS_OPEN_ROOM
    /// </summary>
    public class clsOpenRoom
    {
        public int map_id { get; set; }
        public List<Player> player { get; set; }
    }

    /// <summary>
    /// GS_READY_ROOM
    /// </summary>
    public class clsReadyRoom
    {
        public uint uid { get; set; }
        public bool ready { get; set; }
        public clsReadyRoom() { ready = false; }
    }

    /// <summary>
    /// GS_LOCK_READY_ROOM
    /// </summary>
    public class clsLockReadyRoom
    {
        public bool locked { get; set; }
    }

    /// <summary>
    /// GS_START_GAME
    /// </summary>
    public class clsStartGame
    {
        public Dictionary<uint, Player> users { get; set; }
        public GamePlay gamePlay { get; set; }
    }

    /// <summary>
    /// GS_UPDATE_POSITION
    /// </summary>
    public class clsUpdatePosition
    {
        public uint uid { get; set; }
        public Vec2 position { get; set; }
        public float theta { get; set; }
    }

    /// <summary>
    /// GS_REFRESH_POSITION
    /// </summary>
    public class clsRefreshAllPlayerPosition
    {
        public List<clsUpdatePosition> data { get; set; }
    }

    /// <summary>
    /// GS_UPDATE_STATUS
    /// </summary>
    public class clsUpdateStatus
    {
        public uint uid { get; set; }
        public int attack { get; set; }
        public int hit { get; set; }
    }

    /// <summary>
    /// GS_REFRESH_STATUS
    /// </summary>
    public class clsRefreshAllPlayerStatus
    {
        public List<clsUpdateStatus> data { get; set; }
    }

    /// <summary>
    /// GS_UPDATE_BULLET
    /// </summary>
    public class clsUpdateBullet
    {
        public uint token { get; set; }
        public uint uid { get; set; }
        public float theta { get; set; }
    }

    /// <summary>
    /// GS_REFRESH_BULLET
    /// </summary>
    public class clsRefreshAllBullet
    {
        public List<clsUpdateBullet> data { get; set; }
    }

    /// <summary>
    /// GS_END_GAME
    /// </summary>
    public class clsEndGame
    {
        public bool endGame { get; set; }
    }
    #endregion
}
