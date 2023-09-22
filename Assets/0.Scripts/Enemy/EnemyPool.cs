using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : Singleton<EnemyPool>
{

    [SerializeField] private List<Enemy> enemies;
    private Dictionary<string, Queue<Enemy>> queueEnemy = new Dictionary<string, Queue<Enemy>>();

    public void EnableEnemy()
    {

    }

    public void DisableEnemy()
    {

    }
}
