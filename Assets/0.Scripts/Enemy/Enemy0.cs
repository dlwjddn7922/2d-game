using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        data.Level = 1;
        data.HP = 10;
        data.Speed = 1.5f;
        data.AttackRange = 0.5f;
        data.HitDelayTime = 0.5f;

        Init();
    }
}
