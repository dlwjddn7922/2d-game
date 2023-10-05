using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : Singleton<Spawn>
{
    [SerializeField] public PoolManager pool;
    //[SerializeField] private Enemy[] enemies;
    //public SpawnData[] spawnDatas;

    //[SerializeField] private GameObject[] gameObjects;
    //public Transform rangeObject;
    private BoxCollider2D rangeCollider;

    private float spawnTimer = 0;
    private float nextSpawnTimer = 0;
    int level;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rangeCollider = GetComponent<BoxCollider2D>();
        nextSpawnTimer = Random.Range(1, 3);
        target = FindObjectOfType<Player>()?.transform;
        //GameManager.instance.pool.Get(1);
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
            target = FindObjectOfType<Player>()?.transform;
            return;
        }
        spawnTimer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.Instance.gameTime / 10f);
        if(spawnTimer > nextSpawnTimer)
        {
            spawnTimer = 0;
            
            //int rand = Random.Range(0, enemies.Length);
            //Enemy e = Instantiate(enemies[rand], Return_RandomPosition(), Quaternion.identity);
            if(UI.Instance.timer > 0 && UI.Instance.timer < 30)
            {
                nextSpawnTimer = Random.Range(1, 3);
                GameObject enemy = pool.Get(0);
                enemy.transform.position = Return_RandomPosition();
                enemy.transform.localRotation = Quaternion.identity;
                spawnTimer = 0;
            }
            if (UI.Instance.timer > 30 && UI.Instance.timer < 60f)
            {
                nextSpawnTimer = Random.Range(1,2);
                int rand = Random.Range(0, 2);
                GameObject enemy = pool.Get(rand);
                enemy.transform.position = Return_RandomPosition();
                enemy.transform.localRotation = Quaternion.identity;
                spawnTimer = 0;
            }
            if (UI.Instance.timer > 60 && UI.Instance.timer < 120)
            {
                nextSpawnTimer = Random.Range(1, 2);
                int rand = Random.Range(1, 3);
                GameObject enemy = pool.Get(rand);
                enemy.transform.position = Return_RandomPosition();
                enemy.transform.localRotation = Quaternion.identity;
                spawnTimer = 0;
            }
            if (UI.Instance.timer > 120 && UI.Instance.timer < 240)
            {
                nextSpawnTimer = Random.Range(0.5f,1.5f);
                int rand = Random.Range(2, 4);
                GameObject enemy = pool.Get(rand);
                enemy.transform.position = Return_RandomPosition();
                enemy.transform.localRotation = Quaternion.identity;
                spawnTimer = 0;
            }
            if (UI.Instance.timer > 240 && UI.Instance.timer < 360)
            {
                nextSpawnTimer = Random.Range(0, 1.5f);
                int rand = Random.Range(3,5);
                GameObject enemy = pool.Get(rand);
                enemy.transform.position = Return_RandomPosition();
                enemy.transform.localRotation = Quaternion.identity;
                spawnTimer = 0;
            }
            //GameObject enemy = pool.Get(0);
            //enemy.transform.position = Return_RandomPosition();
            //enemy.transform.localRotation = Quaternion.identity;   
        }
    }
    // 랜덤 스폰 
    public Vector3 Return_RandomPosition()
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

/*[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}*/
