using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
public static class Define
{
    public static GameState state = GameState.Stop;
    public enum GameState
    {
        Play,
        Pause,
        Stop
    }
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
        private int hp;
        private float radius;
        public float maxExp;
        public int maxHp;
        public float Radius
        {
            get { return radius; }
            set
            {
                radius = value;
            }
        }
        public int HP
        {
            get { return hp; }
            set
            {
                hp = value;
                //PlayerHpbar.Instance.SetHp(hp / maxHp);

            }
        }
        public float FireDelayTime { get; set; }
        public float Speed { get; set; }
        public int Power { get; set; }
        public int Level
        {
            get { return level; }
            set
            {
                level = value;               
                UI.Instance.SetLevel(level);
                Define.state = Define.GameState.Stop;
            }
        }
        public float Exp
        {
            get { return curExp; }
            set
            {
                curExp = value;
                UI.Instance.SetExp(curExp / maxExp);
                if (curExp >= maxExp)
                {
                    Level++;
                    curExp = 0;
                    maxExp += 30;
                    UI.Instance.lvPopup.SetActive(true);
                    //Time.timeScale = 0;
                }
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
