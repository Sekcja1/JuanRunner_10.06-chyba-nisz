using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : IBlock
{
    // Use this for initialization
    public void Start()
    {
        Durability = 100;
        Destroyable = true;

    }

    // Update is called once per frame
    public void Update()
    {
        if (Destroyed)
        {
            Destroy(gameObject);
        }
    }

}
