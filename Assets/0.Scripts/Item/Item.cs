using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Exp { get; set; }
    [HideInInspector] public bool isAutoMove = false;
    Vector3 velocity;
    Player p;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isAutoMove)
        {
            transform.position = Vector3.SmoothDamp(transform.position, p.transform.position, ref velocity, 0.2f);
        }
    }
    public void SetTarget(Player p)
    {
        this.p = p; 
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            
        }
    }
}
