using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public int HP { get; set; }
        public float Speed { get; set; }
        public int Level { get; set; }
        public int KillCount { get; set; }
    }
    public enum EnemyState
    {
        Move,
        Hit,
        Dead
    }
    public class EnemyData
    {
        public int HP { get; set; }
        public float Speed { get; set; }
        public float AttackRange { get; set; }
        public float HitDelayTime { get; set; }
    }
}