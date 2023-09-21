using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI : Singleton<UI>
{
    [SerializeField] private TMP_Text killText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text lvText;
    [SerializeField] public Image expImage;
    [SerializeField] public GameObject lvPopup;


    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Define.state != Define.GameState.Play)
            return;

        timer += Time.deltaTime;
        timerText.text = string.Format("{0:00:00}", timer);

        if (expImage.fillAmount >= 1)
            expImage.fillAmount = 0;
    }

    public void SetKillCount(int val)
    {
        killText.text = val.ToString();
        if (val > 0)
        {
            killText.transform.DOScale(1.5f, 0.2f)
            .SetEase(Ease.OutElastic)
            .OnComplete(() => killText.transform.DOScale(1f, 0.2f));
        }
    }
    public void SetLevel(int val)
    {
        lvText.text = $"lv.{val}";
    }
    public void SetExp(float val)
    {
        expImage.DOFillAmount(val, 0.2f);      
    }


}
