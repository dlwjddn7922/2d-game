using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    private SpriteRenderer sr;

    private List<Sprite> sprites = new List<Sprite>();
    private float delay;
    private bool isLoop;

    private int animationIndex = 0;
    private float animationDelayTimer = 0;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(sprites.Count == 0)
            return;

        animationDelayTimer += Time.deltaTime;
        if(animationDelayTimer > delay)
        {
            animationDelayTimer = 0;

            sr.sprite = sprites[animationIndex];
            animationIndex++;

            // 이미지의 갯수가 넘어가면 처음부터 다시
            if(animationIndex >= sprites.Count)
            {
                animationIndex = 0;
            }
        }

    }

    public void SetSprite(List<Sprite> sprite, float delay, bool isLoop = true)
    {
        sprites = sprite.ToList();
        this.delay = delay;
        this.isLoop = isLoop;
    }

}
