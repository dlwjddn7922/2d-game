using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : Singleton<EnemyPool>
{
    [SerializeField] private List<Enemy> enemies;

    private Dictionary<int, Queue<Enemy>> queueEnemy = new Dictionary<int, Queue<Enemy>>();
    // Start is called before the first frame update
    void Start()
    {
        queueEnemy.Add(0, new Queue<Enemy>());
        queueEnemy.Add(1, new Queue<Enemy>());
        queueEnemy.Add(2, new Queue<Enemy>());
        queueEnemy.Add(3, new Queue<Enemy>());
        queueEnemy.Add(4, new Queue<Enemy>());

    }

    public Enemy EnableEnemy(int index, Vector3 randPos)
    {
        if(queueEnemy[index].Count == 0)
        {
            Enemy e = Instantiate(enemies[index], randPos, Quaternion.identity);
            e.spawnIndex = index;
            e.transform.SetParent(transform);
            queueEnemy[index].Enqueue(e);
        }

        Enemy enemy = queueEnemy[index].Peek();
        //enemy.Init();
        enemy.transform.position = randPos;
        enemy.gameObject.SetActive(true);

        return queueEnemy[index].Dequeue();
    }

    public void DisableEnemy(int index, Enemy e)
    {
        e.gameObject.SetActive(false);
        queueEnemy[index].Enqueue(e);
    }
}
