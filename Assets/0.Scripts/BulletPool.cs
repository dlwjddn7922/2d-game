using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Singleton<BulletPool>
{
    //[SerializeField] private Transform bulletParent;
    [SerializeField] private PlayerBullet bullet;

    private Queue<PlayerBullet> pool = new Queue<PlayerBullet>();
    // Start is called before the first frame update
    public PlayerBullet EnableBullet(Transform parent)
    {
        if(pool.Count == 0)
        {
            PlayerBullet b = Instantiate(bullet);
            //b.gameObject.SetActive(true);
            pool.Enqueue(b);
        }
        else
        {
            PlayerBullet myBullet = pool.Peek();
            myBullet.transform.SetParent(parent);
            myBullet.transform.localPosition = Vector3.zero;
            myBullet.transform.localRotation = Quaternion.identity;
            myBullet.gameObject.SetActive(true);
        }
        return pool.Dequeue();
    }

    public void DisableBullet(PlayerBullet b)
    {
        b.gameObject.SetActive(false);
        //b.transform.localPosition = Vector3.zero;
        pool.Enqueue(b);
    }
}
