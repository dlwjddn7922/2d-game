using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : Singleton<ResManager>
{
    [System.Serializable]
    public class PlayerRes
    {
        public List<Sprite> standSprite;
        public List<Sprite> moveSprite;
        public List<Sprite> deadSprite;
    }

    public List<PlayerRes> playerRes;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
