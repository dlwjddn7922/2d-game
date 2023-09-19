using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp10 : Exp
{
    // Start is called before the first frame update
    void Start()
    {
        ExpValue = Random.Range(1, 10);
    }
}
