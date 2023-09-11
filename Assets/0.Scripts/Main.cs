using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Main : MonoBehaviour
{
    [SerializeField] private Transform title;
    [SerializeField] private Transform char1;
    [SerializeField] private Transform char2;
    [SerializeField] private Transform char3;
    [SerializeField] private Transform char4;

    bool isMainAnimation = true;
    // Start is called before the first frame update
    void Start()
    {
        title.DOMoveY(1100, 1f)
            .SetDelay(1f)
            .SetEase(Ease.OutBounce);

        char1.DOMoveX(240, 1f)
            .SetDelay(1.5f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => isMainAnimation = false);
        char3.DOMoveX(240, 1f)
            .SetDelay(1.5f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => isMainAnimation = false);
        char2.DOMoveX(490, 1f)
            .SetDelay(1.5f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => isMainAnimation = false);
        char4.DOMoveX(490, 1f)
            .SetDelay(1.5f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => isMainAnimation = false);

    }

    // Update is called once per frame
    public void OnCharacterChoice(int index)
    {
        if (isMainAnimation)
            return;

        GameManager.Instance.characterIndex = index;
        SceneManager.LoadScene("Game");
        SceneManager.LoadScene("IngameUI",LoadSceneMode.Additive);
    }
}
