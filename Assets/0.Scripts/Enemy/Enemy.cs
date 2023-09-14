using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Sprite> moveSprites;
    [SerializeField] private List<Sprite> hitSprites;
    [SerializeField] private List<Sprite> deadSprites;
    public Transform target;
    Define.EnemyData data = new Define.EnemyData();
    Define.EnemyState state = Define.EnemyState.Move;
    private SpriteAnimation sa;
    private SpriteRenderer sr;

    private Coroutine hitCor;

    private float hitTimer = 0;

    // Start is called before the first frame update
    void Start()
    {       
        sa = GetComponent<SpriteAnimation>();
        sr = GetComponent<SpriteRenderer>();
        data.HP = 10;
        data.Speed = 1.5f;
        data.AttackRange = 0.5f;
        data.HitDelayTime = 0.5f;
        sa.SetSprite(moveSprites, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
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
    public void Hit(int damage)
    {
        data.HP -= damage;
        state = Define.EnemyState.Hit;
        sa.SetSprite(hitSprites, 0.1f);
        // Ÿ���� �޾����� ����¡ �Ǳ� 
        hitTimer = data.HitDelayTime;
        //hp�� �ٴ��� ��� �״´�
        if(data.HP <= 0)
        {
            Dead();   
        }
        void Dead()
        {
            target = null;
            GetComponent<CapsuleCollider2D>().enabled = false;
            sa.SetSprite(deadSprites, 1f, () => Destroy(gameObject));
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerBullet>())
        {

            Hit(10);
            Destroy(collision.gameObject);
        }
        if(collision.GetComponent<Shield>())
        {
            Hit(10);
        }
    }
}
