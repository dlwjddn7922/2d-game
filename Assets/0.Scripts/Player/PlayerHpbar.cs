using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpbar : Singleton<PlayerHpbar>
{
    [SerializeField] private Transform player;
    [SerializeField] private Image hpBar;
    [HideInInspector]
    public float maxHp;
    [HideInInspector]
    public float currentHp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }

    public void SetHp(int val)
    {
        hpBar.fillAmount = val;
    }
}
