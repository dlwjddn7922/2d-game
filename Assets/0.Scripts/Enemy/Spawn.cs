using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    [SerializeField] Transform parent;
    //public Transform rangeObject;
    private BoxCollider2D rangeCollider;

    private float spawnTimer = 0;
    private float nextSpawnTimer = 0;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rangeCollider = GetComponent<BoxCollider2D>();
        nextSpawnTimer = Random.Range(1, 3);
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Define.state != Define.GameState.Play)
            return;

        if (nextSpawnTimer == 0)
            return;


        if (rangeCollider == null)
        {
            rangeCollider = GetComponent<BoxCollider2D>();
            return;
        }

        if (target == null)
        {
            target = FindObjectOfType<Player>().transform;
            return;
        }
        spawnTimer += Time.deltaTime;
        if(spawnTimer > nextSpawnTimer)
        {
            spawnTimer = 0;
            nextSpawnTimer = Random.Range(1, 3);

            int rand = Random.Range(0, enemies.Length);
            Enemy e = Instantiate(enemies[rand],Return_RandomPosition(), Quaternion.identity);
            e.transform.SetParent(parent);
            
        }
    }
    // 랜덤 스폰 
    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeCollider.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Y = rangeCollider.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector3 RandomPostion = new Vector3(range_X, range_Y, 0f);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
}
