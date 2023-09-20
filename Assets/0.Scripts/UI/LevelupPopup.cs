using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelupPopup : MonoBehaviour
{
    [SerializeField] private Transform bg;
    private void OnEnable()
    {
        bg.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        bg.DOScale(1.2f, 0.5f)
            .SetEase(Ease.InBounce)
            .OnComplete(() => bg.DOScale(1f, 0.2f));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
