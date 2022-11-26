using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private const float defaultMovementSpeed = 5;
    private float movementSpeed = defaultMovementSpeed;
    [SerializeField] Animator animatorPlayer;
    [SerializeField] Animator animatorGun;

    static int maxBullets = 6;
    public int ammo;
    public int currentBullets;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    public bool speedBoost;
    public float speedBoostTime;

    public bool isHurt;
    public float hurtCount;
    public bool gameOver;
    public bool gameWon;

    private void Start()
    {
        ammo = 12;
        currentBullets = 6;
        speedBoost = false;
        speedBoostTime = 0;
        isHurt = false;
        hurtCount = 0;
        gameOver = false;
        gameWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && !gameWon)
        {
            Move();
            Shoot();
        }
        ammoText.text = "Ammo: " + ammo.ToString();
        if (isHurt)
        {
            if(hurtCount <= 0)
            {
                animatorPlayer.SetBool("isHurt", false);
                isHurt = false;
                hurtCount = 0;
            }
            else
            {
                hurtCount -= Time.deltaTime;
            }
        }
    }

    private void Move()
    {
        if (speedBoost)
        {
            if(speedBoostTime <= 0)
            {
                speedBoost = false;
                speedBoostTime = 0;
            }
            else
            {
                speedBoostTime -= Time.deltaTime;
            }
            movementSpeed = defaultMovementSpeed + (defaultMovementSpeed / 2);
        }
        else
        {
            movementSpeed = defaultMovementSpeed;
        }

        Vector2 movementVector = new Vector2(0, 0);
        if (Input.GetKey("d"))
        {
            movementVector = new Vector2(1, 0);
            transform.Translate(movementVector * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey("a"))
        {
            movementVector = new Vector2(-1, 0);
            transform.Translate(movementVector * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey("w"))
        {
            movementVector = new Vector2(0, 1);
            transform.Translate(movementVector * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            movementVector = new Vector2(0, -1);
            transform.Translate(movementVector * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown("r"))
        {
            if(ammo > 0)
                reload();
        }


            //checking if the player is walking (for walking animation)
            if ((Mathf.Abs(movementVector.x) + Mathf.Abs(movementVector.y)) > 0)
        {
            animatorPlayer.SetBool("isMoving", true);
        }
        else
        {
            animatorPlayer.SetBool("isMoving", false);
        }
    }

    private void Shoot()
    {
        
        //checking if the player is shooting (for shooting animation)
        if (Input.GetMouseButtonDown(0))
        {
            if (currentBullets > 0)
            {
                animatorGun.SetBool("isShooting", true);
                --currentBullets;               
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            }
        }
        else
        {
            animatorGun.SetBool("isShooting", false);
        }
    }

    private void reload()
    {
        int bulletsToAdd = maxBullets - currentBullets;
        if(ammo >= bulletsToAdd)
        {
            currentBullets += bulletsToAdd;
            ammo -= bulletsToAdd;
        }
        else
        {
            currentBullets += ammo;
            ammo = 0;
        }                
    }
}
