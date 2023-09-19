using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Singleton<PlayerBullet>
{
    [HideInInspector] public float speed;
    [HideInInspector] public int power;

    void Start()
    {
        power = 2;
        speed = 5;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
