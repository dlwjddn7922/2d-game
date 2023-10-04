using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Enemy : Singleton<Enemy>
{
    public float speed;
    public float health;
    public float maxHealth;
    [SerializeField] public List<Sprite> moveSprites;
    [SerializeField] private List<Sprite> hitSprites;
    [SerializeField] private List<Sprite> deadSprites;
    [SerializeField] private Transform itemParent;

    [HideInInspector] public int spawnIndex = -1;

    [SerializeField] private List<Exp> exps;
    public Transform target;
    protected Define.EnemyState state = Define.EnemyState.Move;
    private SpriteAnimation sa;
    private SpriteRenderer sr;
    private Coroutine hitCor;
    protected Define.EnemyData data = new Define.EnemyData();

    private float hitTimer = 0;

    // Start is called before the first frame update

    public void Init()
    {
        sa = GetComponent<SpriteAnimation>();
        sr = GetComponent<SpriteRenderer>();
        sa.SetSprite(moveSprites, 0.2f);

        target = FindObjectOfType<Player>().transform;

        /*exps = Resources.LoadAll<Exp>("Prefabs/Exp").ToList();

        Resources.Load<Exp>("Prefabs/Exp/Exp0}");*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Define.state != Define.GameState.Play)
            return;

        if (target == null)
            return;

        if (hitTimer > 0 && state == Define.EnemyState.Hit)
        {
            hitTimer -= Time.deltaTime;
            return;
        }

        float distance = Vector3.Distance(target.position, transform.position);
        if(data.AttackRange < distance && state != Define.EnemyState.Hit)
        {
            Vector2 dis = target.transform.position - transform.position;
            Vector2 dir = dis.normalized * data.Speed * Time.deltaTime;

            transform.Translate(dir);

            if(dir.normalized.x > 0)
            {
                sr.flipX = false;
            }
            else if(dir.normalized.x < 0)
            {
                sr.flipX = true;
            }
        }
        else if(state != Define.EnemyState.Move)
        {
            state = Define.EnemyState.Move;
            sa.SetSprite(moveSprites, 0.2f);
        }
    }
    IEnumerator SetNormalState()
    {
        yield return new WaitForSeconds(1f);
        state = Define.EnemyState.Move;
        sa.SetSprite(moveSprites, 0.1f);
        hitCor = null;
    }
    public Enemy SetTarget(Transform target)
    {
        this.target = target;
        return this;
    }
    public void Hit()
    {
        data.HP -= Player.Instance.data.Power;
        state = Define.EnemyState.Hit;
        sa.SetSprite(hitSprites, 0.1f);
        // 타격을 받았을때 프리징 되기 
        hitTimer = data.HitDelayTime;
        //hp가 바닥일 경우 죽는다
        if(data.HP <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            sa.SetSprite(deadSprites, 0.5f,DropItem);
            if (target.GetComponent<Player>())
            {
                target.GetComponent<Player>().data.KillCount++;
            }
            target = null;

        }
        void DropItem()
        {
            Exp i = Instantiate(exps[Random.Range(0, data.Level)], transform.position, Quaternion.identity);
            i.transform.SetParent(ItemSpawn.Instance.item);
            gameObject.SetActive(false);
            //Destroy(gameObject);
            //EnemyPool.Instance.DisableEnemy(spawnIndex, this);
        }
    }
    string[] hitTags = { "bullet", "shield" };
    public void OnTriggerEnter2D(Collider2D collision)
    {
        /*if(collision.GetComponent<PlayerBullet>())
        {

            Hit(10);
            Destroy(collision.gameObject);
        }
        if(collision.GetComponent<Shield>())
        {
            Hit(10);
        }*/
        foreach (var tag in hitTags)
        {
            if(collision.CompareTag(tag))
            {
                Hit();
                if(tag.Equals("bullet"))
                {
                    BulletPool.Instance.DisableBullet(collision.GetComponent<PlayerBullet>());
                }
                else
                {

                }               
            }
        }
    }
    private void OnEnable()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
        target = FindObjectOfType<Player>().transform;

    }
    private void OnDisable()
    {
        sa.SetSprite(moveSprites, 0.2f);
    }
}
