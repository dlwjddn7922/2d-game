using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : Singleton<EnemyPool>
{

    //[SerializeField] private Transform bulletParent;
    [SerializeField] private Enemy[] enemy;

    private Queue<Enemy> pool = new Queue<Enemy>();
    // Start is called before the first frame update
    public Enemy EnableBullet(Enemy rand)
    {
        if (pool.Count == 0)
        {
            //int rand0 = Random.Range(0, enemy.Length);
            Enemy e = Instantiate(rand);
            //b.gameObject.SetActive(true);
            pool.Enqueue(e);
        }
        else
        {
            Enemy e = pool.Peek();
            //e.transform.SetParent(parent);
            //myBullet.transform.localPosition = Vector3.zero;
            e.transform.localRotation = Quaternion.identity;
            e.gameObject.SetActive(true);
        }
        return pool.Dequeue();
    }

    public void DisableBullet(Enemy e)
    {
        e.gameObject.SetActive(false);
        //b.transform.localPosition = Vector3.zero;
        pool.Enqueue(e);
    }
}
