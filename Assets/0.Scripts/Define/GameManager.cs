using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player p; 
    [HideInInspector]
    public int characterIndex = -1;

    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public PoolManager epool;
    public PoolManager wpool;
    public Player player;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Define.state = Define.GameState.Play;
        
    }

    public void CreatePlayer()
    {
        string curSceneName = SceneManager.GetActiveScene().name;
        if (curSceneName.Equals("Game") && FindObjectOfType<Player>() == null)
        {
            Player player  = Instantiate(p);
            player.standSprite = ResManager.Instance.playerRes[characterIndex].standSprite;
            player.moveSprite = ResManager.Instance.playerRes[characterIndex].moveSprite;
            player.deadSprite = ResManager.Instance.playerRes[characterIndex].deadSprite;
            DontDestroyOnLoad(p.gameObject);
        }
    }
     void Update()
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
