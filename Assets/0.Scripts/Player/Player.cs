using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteAnimation sa;

    [SerializeField] private List<Sprite> standSprite;
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        sa.SetSprite(standSprite, 0.2f);
    }

    void Update()
    {
        
    }
}
