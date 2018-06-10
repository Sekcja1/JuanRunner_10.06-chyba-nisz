using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : IBlock
{
    public ParticleSystem PS;

    // Use this for initialization
    void Start()
    {
        Fire = PS;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
