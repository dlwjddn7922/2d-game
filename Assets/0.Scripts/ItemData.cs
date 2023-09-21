using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData/Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string weapon;
    [TextArea(1,10)]
    [SerializeField] private string desc;
    [SerializeField] private float value;
    public Sprite Icon
    {
        get { return icon; }
        set { icon = value; }
    }
    public string Weapon
    {
        get { return weapon; }
        set { weapon = value; }
    }
    public string Desc
    {
        get { return desc; }
        set { desc = value; }
    }
    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
