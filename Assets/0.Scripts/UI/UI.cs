using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : Singleton<UI>
{
    [SerializeField] private TMP_Text killText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text lvText;
    [SerializeField] private Image expImage;

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

    public void SetKillCount(int val) => killText.text = val.ToString();
    public void SetLevel(int val) => lvText.text = $"lv.{val}";
    public void SetExp(float val) => expImage.fillAmount = val;


}
