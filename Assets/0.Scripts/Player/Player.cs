using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteAnimation sa;
    private SpriteRenderer sr;

    [SerializeField] private List<Sprite> standSprite;
    [SerializeField] private List<Sprite> moveSprite;
    [SerializeField] private Transform fireTrans;

    Define.PlayerData data = new Define.PlayerData();
    Define.PlayerState state = Define.PlayerState.Stand;

    //쉴드 작업
    [SerializeField] private Transform shield;
    private List<Transform> shields = new List<Transform>();

    private int shieldCnt = 0;
    private float shieldSpeed = 100f;
    private float fireTimer = float.MaxValue;
    private float radius = 3f;
    public int Shield
    {
        set
        {
            if(value > 0)
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
        sa.SetSprite(standSprite, 0.2f);
        //Shield = 0;
        data.Speed = 3f;
        data.FireDelayTime = 0.5f;
        state = Define.PlayerState.Stand;

        data.Level = 1;
        data.KillCount = 0;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 playervec = new Vector3(x, y, 0);
        transform.Translate(playervec * Time.deltaTime * data.Speed);

        //방향전환
        if(x != 0)
        {
            sr.flipX = x < 0 ? true : false;
        }
        //stand, move 스프라이트 전환
        if ((y != 0 || x != 0) && state != Define.PlayerState.Move)
        {
            sa.SetSprite(moveSprite, 0.1f);
            state = Define.PlayerState.Move;

        }
        else if (y == 0 && x == 0 && state != Define.PlayerState.Stand)
        {
            sa.SetSprite(standSprite, 0.2f);
            state = Define.PlayerState.Stand;
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            Shield = ++shieldCnt;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Shield = --shieldCnt;
        }
        if(shields.Count != 0)
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
        Gizmos.DrawWireSphere(fireTrans.transform.position, 3f);
    }
    public void FireBullet()
    {
        Collider2D[] findMonsters = Physics2D.OverlapCircleAll(fireTrans.position, radius, LayerMask.GetMask("Monster"));
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
            PlayerBullet b =  BulletPool.Instance.EnableBullet(fireTrans);
            //b.transform.localPosition = Vector3.zero;
            //b.gameObject.SetActive(true);
            b.transform.SetParent(BulletPool.Instance.transform);

        }
    }
}
