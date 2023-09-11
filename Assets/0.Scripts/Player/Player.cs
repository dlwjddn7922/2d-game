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
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        sr = GetComponent<SpriteRenderer>();
        sa.SetSprite(standSprite, 0.2f);

        data.Speed = 3f;
        state = Define.PlayerState.Stand;
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
    }
}
