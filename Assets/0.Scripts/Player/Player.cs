using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteAnimation sa;
    private SpriteRenderer sr;

    [SerializeField] private List<Sprite> standSprite;
    [SerializeField] private List<Sprite> moveSprite;

    Define.PlayerData data = new Define.PlayerData();
    Define.PlayerState state = Define.PlayerState.Stand;

    //���� �۾�
    [SerializeField] private Transform shield;
    private List<Transform> shields = new List<Transform>();

    private int shieldCnt = 0;
    private float shieldSpeed = 100f;
    public int Shield
    {
        set
        {
            if(value >= 0)
            {
                for (int i = transform.childCount - 1; i >= 0; i--)
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
        state = Define.PlayerState.Stand;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 playervec = new Vector3(x, y, 0);
        transform.Translate(playervec * Time.deltaTime * data.Speed);

        //������ȯ
        if(x != 0)
        {
            sr.flipX = x < 0 ? true : false;
        }
        //stand, move ��������Ʈ ��ȯ
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
    }
}