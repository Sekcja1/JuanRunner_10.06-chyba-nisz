using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : IWeapon
{
	public Pistol()
	{
		base.Name = "Pistolet";
		base.Damage = 10f;
        base.FireRate = 1;
		base.Splash = false;
		base.SplashRange = 0;
		base.ProjectileSpeed = 10;
		base.SplashDamage = 0;
		base.ProjectileRange = 50.0f;
		base.AmmoCount = 0;
	}
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Destroy(gameObject);
		coll.gameObject.SendMessage("AddDamage", Damage);
	}
}
