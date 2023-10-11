using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
        
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(Itemdatas data)
    {
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int index = 0; index < GameManager.Instance.epool.prefabs.Length; index++)
        {
            if(data.projectile == GameManager.Instance.epool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }
        
    }
}
