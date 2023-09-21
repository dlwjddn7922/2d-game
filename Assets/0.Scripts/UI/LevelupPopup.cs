using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Linq;

public class LevelupPopup : MonoBehaviour
{
    [SerializeField] private Transform bg;

    [SerializeField] private List<ItemData> itemDatas;

    [System.Serializable]
    public class LevelUPUI
    {
        public Image icon;
        public TMP_Text lvText;
        public TMP_Text weaponText;
        public TMP_Text descText;
    }

    public List<LevelUPUI> levelUPUIs = new List<LevelUPUI>();

    private List<ItemData> popUpDatas = new List<ItemData>();
    private void OnEnable()
    {
        bg.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        bg.DOScale(1.2f, 0.5f)
            .SetEase(Ease.InBounce)
            .OnComplete(() => bg.DOScale(1f, 0.2f));


    //데이터 3개만 추출하기
    popUpDatas.Clear();
        popUpDatas = itemDatas.ToList();
        while (popUpDatas.Count > 3)
        {
            int rand = Random.Range(0, popUpDatas.Count);
            popUpDatas.RemoveAt(rand);
        }

        for (int i = 0; i < levelUPUIs.Count; i++)
        {
            levelUPUIs[i].icon.sprite = popUpDatas[i].Icon;
            levelUPUIs[i].weaponText.text = popUpDatas[i].Weapon;
            levelUPUIs[i].descText.text = popUpDatas[i].Desc;
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSelect(int index)
    {
        Define.state = Define.GameState.Play;
    }
}

