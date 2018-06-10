using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : IWeapon
{
    public GameObject Particles;
    public GameObject ExplosionEffect;

    public Granade()
    {
        base.Name = "Granat odłamkowy";
        base.Damage = 50f;
        base.FireRate = 1;
        base.Splash = true;
        base.SplashRange = 1.8f;
        base.ProjectileSpeed = 5;
        base.SplashDamage = 5.0f;
        base.ProjectileRange = 20.0f;
        base.AmmoCount = 0;
    }


    // Use this for initialization
    void Start ()
    {
        ExplosionEffect.gameObject.GetComponent<ParticleSystem>().Play();
        Particles.gameObject.GetComponent<ParticleSystem>().Play();
        //this.gameObject.
        
        
        this.gameObject.GetComponent<AudioSource>().PlayDelayed(1f);
        Destroy(gameObject, 1.2f);
    }
    
    // Update is called once per frame
    void Update () {
        
    }



}
