using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TangkaUI
{
    public enum Job
    {
        WORRIOR = 0,
        THIEF = 1,
        MAGICIAN = 2,
        BOWMAN = 3,
    }

    public class PlayerData
    {
        public readonly string nickName;
        public readonly int level;
        public readonly Job job;

        public readonly int STR;
        public readonly int DEX;
        public readonly int INT;
        public readonly int LUK;

        public PlayerData(int index)
        {
            nickName = $"Player {index}";
            level = Random.Range(1, 100);
            job = (Job)(Random.Range(0, 4));

            int defaultStats = 20;
            int allStats = 5 * level;

            int defaultStat = Random.Range(1, 7);
            int stat = Random.Range(0, allStats + 1);
            STR = defaultStat + stat;
            defaultStats -= defaultStat;
            allStats -= stat;

            defaultStat = Random.Range(1, 7);
            stat = Random.Range(0, allStats + 1);
            DEX = defaultStat + stat;
            defaultStats -= defaultStat;
            allStats -= stat;

            defaultStat = Random.Range(1, 7);
            stat = Random.Range(0, allStats + 1);
            INT = defaultStat + stat;
            defaultStats -= defaultStat;
            allStats -= stat;

            LUK = defaultStats + allStats;
        }

        public void DebugLog()
        {
            Debug.Log($"Nick Name :\t{nickName}\n" +
                      $"Level :\t\t{level}\n" +
                      $"Job :\t\t{job}\n" +
                      $"----------------------\n" +
                      $"STR :\t{STR}\n" +
                      $"DEX :\t{DEX}\n" +
                      $"INT :\t{INT}\n" +
                      $"LUK :\t{LUK}");
        }
    } 
}
