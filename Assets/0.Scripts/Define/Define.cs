using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
public static class Define
{
    public enum PlayerState
    {
        Stand,
        Move,
        Hit,
        Dead
    }
    public class PlayerData
    {
        private int level;
        private int killCount;
        private float curExp;
        public float maxExp;
        public int HP { get; set; }
        public float FireDelayTime { get; set; }
        public float Speed { get; set; }
        public int Level
        {
            get { return level; }
            set
            {
                level = value;               
                UI.Instance.SetLevel(level);
            }
        }
        public float Exp
        {
            get { return curExp; }
            set
            {
                curExp = value;
                UI.Instance.SetExp(curExp / maxExp);               
            }
        }
        public int KillCount
        {
            get { return killCount; }
            set
            {
                killCount = value;              
                UI.Instance.SetKillCount(killCount);
            }
        }
    }
    public enum EnemyState
    {
        Move,
        Hit,
        Dead
    }
    public class EnemyData
    {
        public int Level { get; set; }
        public int HP { get; set; }
        public float Speed { get; set; }
        public float AttackRange { get; set; }
        public float HitDelayTime { get; set; }
    }
}
