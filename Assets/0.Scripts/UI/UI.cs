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
    [SerializeField] private Image expImage;

    private float loopTimeValue = 1f;
    private float targetValue = 0f;
    private float currentValue = 0f;
    public AnimationCurve loopAnimCurve;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = string.Format("{0:00:00}", timer);

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
