using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mele : IWeapon {

	public Mele()
	{
		base.Name = "Kung-fuuu";
		base.Damage = 0.1f;
        base.FireRate = 1;
		base.Splash = false;
		base.SplashRange = 0;
		base.ProjectileSpeed = 20;
		base.SplashDamage = 0;
		base.ProjectileRange = 5.0f;
		base.AmmoCount = -1;
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
