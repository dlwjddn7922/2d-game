using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private SpriteAnimation sa;
    private SpriteRenderer sr;

    [SerializeField] public PoolManager pool;
    [SerializeField] public List<Sprite> standSprite;
    [SerializeField] public List<Sprite> moveSprite;
    [SerializeField] public List<Sprite> deadSprite;
    //[SerializeField] public List<Sprite> attackSprite;
    [SerializeField] private Transform fireTrans;
    [HideInInspector]
    public Define.PlayerData data = new Define.PlayerData();
    Define.PlayerState state = Define.PlayerState.Stand;

    //쉴드 작업
    [SerializeField] private Transform shield;
    private List<Transform> shields = new List<Transform>();

    public int shieldCnt = 0;
    private float shieldSpeed = 100f;
    private float fireTimer = float.MaxValue;
    Rigidbody2D rigid;
    [SerializeField] private bl_Joystick js;
    //private float radius = 3f;
    public int Shield
    {
        set
        {
            if (value > 0)
            {
                for (int i = shields.Count - 1; i >= 0; i--)
                {
                    Destroy(shields[i].gameObject);
                }

                shields.Clear();
                int angle = 360 / value;
                int startAngle = 0;

                for (int i = 0; i < value; i++)
                {
                    Transform t = Instantiate(shield, transform);
                    t.rotation = Quaternion.Euler(new Vector3(0f, 0f, startAngle));
                    shields.Add(t);
                    startAngle += angle;
                }
            }
        }
    }
    void Start()
    {        
        sa = GetComponent<SpriteAnimation>();
        sr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        //sa.SetSprite(standSprite, 0.2f);
        //Shield = 0;
        data.Speed = 3f;
        data.FireDelayTime = 0.2f;
        state = Define.PlayerState.Stand;

        data.Power = 10;
        data.Level = 1;
        data.KillCount = 0;
        data.maxExp = 100;
        data.Exp = 0;
        data.maxHp = 100;
        data.HP = 100;
        data.Radius = 3f;

        switch(GameManager.Instance.characterIndex)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    void Update()
    {
        if (state == Define.PlayerState.Dead)
            return;
        if (Define.state != Define.GameState.Play)
            return;

        /*float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        Vector3 playervec = new Vector3(x, y, 0);
        transform.Translate(playervec * Time.deltaTime * data.Speed);*/
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        // 스틱이 향해있는 방향을 저장해준다.
        Vector3 dir = new Vector3(js.Horizontal, js.Vertical, 0);

        // Vector의 방향은 유지하지만 크기를 1로 줄인다. 길이가 정규화 되지 않을시 0으로 설정.
        dir.Normalize();

        // 오브젝트의 위치를 dir 방향으로 이동시킨다.
        transform.position += dir * data.Speed * Time.deltaTime;

        //방향전환
        if (js.Horizontal != 0)
        {
            sr.flipX = js.Horizontal < 0 ? true : false;
        }
        //stand, move 스프라이트 전환
        if ((js.Vertical != 0 || js.Horizontal != 0) && state != Define.PlayerState.Move)
        {
            sa.SetSprite(moveSprite, 0.1f);
            state = Define.PlayerState.Move;

        }
        else if (js.Vertical == 0 && js.Horizontal == 0 && state != Define.PlayerState.Stand)
        {
            sa.SetSprite(standSprite, 0.2f);
            state = Define.PlayerState.Stand;
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            Shield = ++shieldCnt;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Shield = --shieldCnt;
        }
        if (shields.Count != 0)
        {
            foreach (var s in shields)
            {
                s.Rotate(new Vector3(0f, 0f, Time.deltaTime * -shieldSpeed));
            }
        }
        // 가장 가까운 적을 찾아 발사
        FireBullet();


    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(fireTrans.transform.position, data.Radius);
    }
    public void FireBullet()
    {
        Collider2D[] findMonsters = Physics2D.OverlapCircleAll(fireTrans.position, data.Radius, LayerMask.GetMask("Monster"));
        float distance = float.MaxValue;
        Transform target = null;
        if (findMonsters.Length != 0)
        {
            foreach (var mon in findMonsters)
            {
                float dis = Vector2.Distance(transform.position, mon.transform.position);

                if (dis < distance)
                {
                    distance = dis;
                    target = mon.transform;
                }
            }
        }
        fireTimer += Time.deltaTime;
        if (target != null && fireTimer > data.FireDelayTime)
        {
            fireTimer = 0f;

            //타겟을 찾아 방향전환
            Vector2 vec = fireTrans.position - target.transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            fireTrans.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            //총알 pool
            PlayerBullet b = BulletPool.Instance.EnableBullet(fireTrans);
            b.transform.localPosition = Vector3.zero;
            b.transform.SetParent(BulletPool.Instance.transform);

        }
    }
    public void SetExp(int value)
    {
        data.Exp += value;
    }
    public void Dead()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0;
        UI.Instance.gameoverPopup.gameObject.SetActive(true);
    }

    public void Hit()
    {
        data.HP -= 20;
        if (data.HP <= 0)
        {
            state = Define.PlayerState.Dead;
            sa.SetSprite(deadSprite, 0.1f,Dead);          
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(state != Define.PlayerState.Dead)
                Hit();
        }
    }
}
