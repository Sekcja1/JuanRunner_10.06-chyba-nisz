using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private GameObject gameObject;
    private Vector3 position;
    private Quaternion rotation;

    public int UseWeapon { get; set; }

    public float speed = 2;
    private GameObject cross;

    private List<IWeapon> weapons = new List<IWeapon>();

    public GameObject pistolBullet;
    public GameObject granade;
    public GameObject mele;

    private int showedInfo = 0;
    // Use this for initialization
    void Start()
    {
        UseWeapon = 0;
        gameObject = GameObject.Find("Player");
        cross = GameObject.Find("Aim");
        rotation = gameObject.transform.rotation;

        weapons.Add(new Pistol() { AmmoCount = 50, AmmoPrefab = pistolBullet });
        weapons.Add(new Granade() { AmmoCount = 20, AmmoPrefab = granade });
        weapons.Add(new Mele() { AmmoPrefab = mele });
    }

    // Update is called once per frame
    void Update()
    {
        position = gameObject.transform.position;

        #region Sterowanie
        if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("olsdkfjgol;kjsdfgl;kjdflgjdfkljglkdfjgklrd");
            this.gameObject.GetComponent<Rigidbody2D>().
                AddForce(transform.up * 2);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            position.y -= speed * Time.deltaTime;
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
        #endregion

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

        if (Input.GetMouseButtonDown(0))
        {
            weapons[UseWeapon].Shoot(position, worldPos, rotation);
        }
    }

}
