using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        data.Level = 1;
        data.HP = 100;
        data.Speed = 2f;
        data.AttackRange = 0.5f;
        data.HitDelayTime = 0.5f;
        data.SpawnTime = 1.5f;
        Init();
    }

}
