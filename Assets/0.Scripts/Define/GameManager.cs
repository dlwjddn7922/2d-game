using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public int characterIndex = -1;
    public static GameManager instance;
    public PoolManager pool;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Define.state = Define.GameState.Play;
    }
}
