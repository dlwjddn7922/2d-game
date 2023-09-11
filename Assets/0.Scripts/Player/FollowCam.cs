using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Player p;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = p.transform.position;
        targetPos.z = -10;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.1f);
        transform.position = targetPos;
    }
}
