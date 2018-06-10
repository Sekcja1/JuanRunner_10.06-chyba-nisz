using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : IBlock
{

    // Use this for initialization
    void Start()
    {
        Durability = 100;
        Destroyable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Destroyed)
        {
            Destroy(gameObject);
        }
    }
}
