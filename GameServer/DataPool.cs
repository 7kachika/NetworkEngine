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
using System;
using System.Collections.Generic;

public class DataPool
{
    // ← →

    #region -- 所有資料 --
    /// <summary>
    /// C←S 所有玩家資料
    /// </summary>
    public static Dictionary<uint, Player> users { get; set; }

    /// <summary>
    /// C←S 所有房間資料
    /// </summary>
    public static Dictionary<uint, clsOpenRoom> rooms { get; set; }

    /// <summary>
    /// C←S 所有地圖資料
    /// </summary>
    public static Dictionary<uint, Map> maps { get; set; }

    /// <summary>
    /// C←S 所有子彈資料
    /// </summary>
    public static Dictionary<uint, Bullet> shots { get; set; }

    /// <summary>
    /// C←S 當前遊戲資料
    /// </summary>
    public static GamePlay gamePlay { get; set; }

    public static List<clsReadyRoom> readys { get; set; }

    public static clsLockReadyRoom readyLock { get; set; }

    public static bool gameStart { get; set; }
    #endregion

    /// <summary>
    /// 靜態建構式
    /// </summary>
    static DataPool()
    {
        rooms = new Dictionary<uint, clsOpenRoom>();

        users = new Dictionary<uint, Player>();
        maps = new Dictionary<uint, Map>();
        shots = new Dictionary<uint, Bullet>();
        gamePlay = new GamePlay();
        gamePlay.objPoolPlayerCount = 4;
        gamePlay.objPoolBulletCount = 200;
        gamePlay.moveTime = .01f;
        gamePlay.map_id = 5;
        gamePlay.move = 5;
        gamePlay.speed = 10;
        gamePlay.cooldown = 2;
        gamePlay.time = 60;
        readys = new List<clsReadyRoom>();
        readyLock = new clsLockReadyRoom();
        readyLock.locked = false;
        gameStart = false;
    }
}
