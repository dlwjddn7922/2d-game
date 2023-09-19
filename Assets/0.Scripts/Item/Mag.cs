using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Exp[] exps = FindObjectsOfType<Exp>();

            foreach (var item in exps)
            {
                item.SetTarget(collision.GetComponent<Player>());
                item.isAutoMove = true;
                Destroy(gameObject);
            }
        }
    }
}
