using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Itemdatas data;
    public int level;
    public Weapons weapons;
    

    Image icon;
    TMP_Text textLevel;

    void Start()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        textLevel = texts[0];
    }
    void LateUpdate()
    {
        textLevel.text = "Lv. " + (level + 1); 
    }
    
    public void OnClick()
    {
        switch (data.itemType)
        {
            case Itemdatas.ItemType.Melee:
            case Itemdatas.ItemType.Range:
                if( level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapons = newWeapon.AddComponent<Weapons>();
                    weapons.Init(data);
                }
                break;

            case Itemdatas.ItemType.Glove:
                break;
            case Itemdatas.ItemType.Shoe:
                break;
            case Itemdatas.ItemType.Heal:
                break;
        }
        level++;

        if(level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
