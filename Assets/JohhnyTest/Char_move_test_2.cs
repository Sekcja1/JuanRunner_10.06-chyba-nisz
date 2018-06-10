using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Char_move_test_2 : MonoBehaviour
{


    Animator anim;

    private new GameObject gameObject;
    private Vector3 position;
    private Vector3 startPos;

    bool isgrounded = true;
    public float speed = 5;
    public int UseWeapon { get; set; }

    public bool move = false;
    private Quaternion rotation;
    bool DirRight = true;
    bool DirLeft = false;
    bool Dir_triggerL = true;
    bool Dir_triggerR = true;
    bool Dir_IDLE = true;
    bool Normal_shoot = false;
    bool crouch = false;
    private GameObject cross;
    private List<IWeapon> weapons = new List<IWeapon>();

    public bool grounded = true;

    public GameObject pistolBullet;
    public GameObject granade;
    public GameObject mele;
    private GameObject camera;

    private bool canJump = true;

    private AudioSource shoot;
    System.Timers.Timer jumpTimer = new System.Timers.Timer();

    private Vector3 prev, curr;
    public float hp = 1000;
    bool alive = true;

    public GameObject feet { get; set; }
    public GameObject Gun { get; set; }
    public GameObject Gun2 { get; set; }

    public static float healtAmount;


    // Use this for initialization
    void Start()
    {
        gameObject = GameObject.Find("GreenBeret");
        feet = GameObject.Find("GreenBeretFeet");
        //camera = GameObject.Find("Camera");
        anim = GetComponent<Animator>();
        startPos = gameObject.transform.position;

        shoot = this.gameObject.GetComponent<AudioSource>();

        UseWeapon = 0;
        cross = GameObject.Find("Aim");
        Gun = GameObject.Find("Gun1");
        Gun2 = GameObject.Find("Gun12");

        rotation = gameObject.transform.rotation;

        weapons.Add(new Pistol() { AmmoCount = 50000, AmmoPrefab = pistolBullet });
        weapons.Add(new Granade() { AmmoCount = 20000, AmmoPrefab = granade });
        weapons.Add(new Mele() { AmmoPrefab = mele });

        jumpTimer.Interval = 1000;
        jumpTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        jumpTimer.Enabled = true;

        healtAmount = hp / 1000;
    }
    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            float char_move = Input.GetAxis("Vertical");
            //anim.SetFloat("char_speed", );
            position = gameObject.transform.position;

            Dir_IDLE = true;

            //camera.transform.position = new Vector3(position.x, position.y, camera.transform.position.z);


            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("char_normal_shoot", true);
                Normal_shoot = true;

                if (UseWeapon == 0)
                    shoot.Play();

                weapons[UseWeapon].Shoot(Gun.transform.position, Gun2.transform.position, rotation);
            }
            else
            {
                anim.SetBool("char_normal_shoot", false);
                Normal_shoot = false;
            }

            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && Normal_shoot == false)
            {
                position.x += speed * Time.deltaTime;
                DirRight = true;
                Dir_triggerR = false;
                Dir_triggerL = true;
                DirLeft = false;
                Dir_IDLE = false;
                anim.SetBool("char_moving", true);
            }
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && Normal_shoot == false)
            {
                position.x -= speed * Time.deltaTime;
                DirRight = false;
                Dir_triggerR = true;
                Dir_triggerL = false;
                DirLeft = true;
                Dir_IDLE = false;
                anim.SetBool("char_moving", true);
            }

            var cols = Physics2D.OverlapCircleAll(transform.position, 1F);

            if (cols.Any(a => feet.GetComponent<BoxCollider2D>().IsTouching(a)))
            {
                grounded = true;
            }

            if (Input.GetKey(KeyCode.W) && canJump && grounded)
            {
                canJump = false;
                grounded = false;
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1700), ForceMode2D.Impulse);
            }



            if (Input.GetKey(KeyCode.S) && Normal_shoot == false)
            {
                // position.y -= speed * Time.deltaTime;
                Dir_IDLE = false;
                crouch = true;
                anim.SetBool("char_crouch", true);


            }
            if (Input.GetKey(KeyCode.Alpha1))
            {
                UseWeapon = 0;
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                UseWeapon = 1;
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                UseWeapon = 2;
            }

            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.C))
            {
                anim.SetBool("char_crouch_shoot", true);
            }

            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.C))
            {
                anim.SetBool("char_crouch_shoot", false);
            }

            if (!Input.GetKey(KeyCode.S))
            {
                anim.SetBool("char_crouch", false);
            }

            if (Dir_IDLE == true)
            {
                anim.SetBool("char_moving", false);
            }


            gameObject.transform.SetPositionAndRotation(position, rotation);


            #region Click
            var cameraloc = new Camera();
            var mousePos = new Vector3();
            var worldPos = new Vector3();
            cameraloc = GameObject.Find("Camera").GetComponent<Camera>();
            mousePos = Input.mousePosition;
            worldPos = cameraloc.ScreenToWorldPoint(mousePos);
            #endregion

            cross.transform.position = worldPos + new Vector3(0, 0, 10);

            Vector3 thescale = transform.localScale;
            if (DirRight == true)
            {
                thescale.x = 1;
            }

            else if (DirLeft == true)
            {
                thescale.x = -1;
            }
            transform.localScale = thescale;

            if (gameObject.transform.position.y <= -30.0f)
            {
                gameObject.transform.position = startPos;
            }
        }
        else
        {
            anim.SetBool("char_is_dead", true);
        }
    }
    void waiter()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void AddDamage(float damage)
    {
        hp -= (damage);

        if (alive && healtAmount>=0)
            healtAmount -= (damage/100);
        
        if (hp <= 0 && healtAmount >= 0)
        {
            alive = false;
            healtAmount = 0;
            anim.SetBool("char_crouch_shoot", false);
            anim.SetBool("char_crouch", false);
            anim.SetBool("char_moving", false);
            anim.SetBool("char_normal_shoot", false);

            anim.SetBool("char_is_dead", true);
            Invoke("waiter", 6);
        }
    }

}
