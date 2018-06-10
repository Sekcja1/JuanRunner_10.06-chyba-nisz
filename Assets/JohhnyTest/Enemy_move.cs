using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Enemy_move : MonoBehaviour
{

    public float speed = 5;
    public int UseWeapon { get; set; }

    public bool move = false;
    private List<IWeapon> weapons = new List<IWeapon>();

    public bool grounded = true;

    public GameObject pistolBullet;
    public GameObject granade;
    public GameObject mele;

    public GameObject feet { get; set; }


    public LayerMask enemyMask;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;
    float hp = 100;
    bool alive = true;
    public double rangeOfDetectionX = 1000;
    public double rangeOfDetectionY = 10;
    public int firerate = 1000;

    GameObject Player;
    GameObject Player2;

    Animator anim;
    bool Normal_shoot = false;
    private AudioSource shoot;
    private Quaternion rotation;
    public GameObject Gun;
    public GameObject Gun2;
    int counter = 0;
    bool playerInRange = false;
    public float RotationSpeed = 1;

    // Use this for initialization
    void Start()
    {
        UseWeapon = UnityEngine.Random.Range(0, 2);
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        if (UseWeapon == 1)
        {
            firerate *= 10;
        }

        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;

        Player = GameObject.Find("RedBeret");
        Player2 = GameObject.Find("GreenBeret");

        anim = this.GetComponent<Animator>();
        rotation = gameObject.transform.rotation;
        shoot = this.gameObject.GetComponent<AudioSource>();


        weapons.Add(new Pistol() { AmmoCount = 50000, AmmoPrefab = pistolBullet });
        weapons.Add(new Granade() { AmmoCount = 20000, AmmoPrefab = granade });
        weapons.Add(new Mele() { AmmoPrefab = mele });
        
    }

    void AddDamage(float damage)
    {
 
        hp -= damage;
        if (hp <= 0)
        {
           
            alive = false;
            anim.SetBool("char_moving", false);
            anim.SetBool("char_normal_shoot", false);

            anim.SetBool("char_is_dead", true);

            Destroy(gameObject,10);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currRot;
        if (alive)
        {
            var distanceX = Math.Abs(Player.transform.position.x - this.gameObject.transform.position.x);
            var distanceY = Math.Abs(Player.transform.position.y - this.gameObject.transform.position.y);

            var distanceX2 = Math.Abs(Player2.transform.position.x - this.gameObject.transform.position.x);
            var distanceY2 = Math.Abs(Player2.transform.position.y - this.gameObject.transform.position.y);

            if (distanceX < rangeOfDetectionX)// && distanceY < rangeOfDetectionY) //jeśli gracz w zasięgu
            {
                playerInRange = true;
                anim.SetBool("char_normal_shoot", true);
                Normal_shoot = true;

                var position = gameObject.transform.position;

                counter++;
                if (counter > firerate)
                {
                    if (UseWeapon == 0)
                    {
                        shoot.Play();
                    }
                    weapons[UseWeapon].Shoot(Gun.transform.position, Gun2.transform.position, rotation);
                    counter = 0;
                }

                var _direction = (Player.transform.position - transform.position).normalized;              

                if (_direction.x > 0)
                {
                    currRot = myTrans.eulerAngles;
                    currRot.y = 180;
                    myTrans.eulerAngles = currRot;
                }
                else
                {
                    currRot = myTrans.eulerAngles;
                    currRot.y = 0;
                    myTrans.eulerAngles = currRot;
                }

                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 0.1F);
            }
            if (distanceX2 < rangeOfDetectionX) //jeśli gracz2 w zasięgu
            {
                playerInRange = true;
                anim.SetBool("char_normal_shoot", true);
                Normal_shoot = true;

                var position = gameObject.transform.position;

                counter++;
                if (counter > firerate)
                {
                    if (UseWeapon == 0)
                    {
                        shoot.Play();
                    }
                    weapons[UseWeapon].Shoot(Gun.transform.position, Gun2.transform.position, rotation);
                    counter = 0;
                }

                var _direction = (Player2.transform.position - transform.position).normalized;


                if (_direction.x > 0)
                {
                    currRot = myTrans.eulerAngles;
                    currRot.y = 180;
                    myTrans.eulerAngles = currRot;
                }
                else
                {
                    currRot = myTrans.eulerAngles;
                    currRot.y = 0;
                    myTrans.eulerAngles = currRot;
                }


                transform.position = Vector3.MoveTowards(transform.position, Player2.transform.position, 0.1F);
            }

            if (!playerInRange)
            {
                anim.SetBool("char_moving", true);
                //chect to see if theres ground in front of us before moving forward
                Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
                Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
                bool isgrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
                Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.5f);
                bool isblocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.5f, enemyMask);

                //if there is no ground, turn around
                if (!isgrounded || isblocked)
                {
                     currRot = myTrans.eulerAngles;
                    currRot.y += 180;
                    myTrans.eulerAngles = currRot;
                }
                if (myTrans.eulerAngles.z != 0)
                {
                     currRot = myTrans.eulerAngles;
                    currRot.z = 0;
                    myTrans.eulerAngles = currRot;
                }
                Vector2 myVel = myBody.velocity;
                myVel.x = -myTrans.right.x * speed;
                myBody.velocity = myVel;
            }
        }
        else
        {
            anim.SetBool("char_moving", false);
            anim.SetBool("char_normal_shoot", false);
            anim.SetBool("char_is_dead", true);
       
        }


    }

}
