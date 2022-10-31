﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private float movementSpeed = 5;
    [SerializeField] Animator animatorPlayer;
    [SerializeField] Animator animatorGun;
    static int maxBullets = 6;
    public int ammo;
    public int currentBullets;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;

    private void Start()
    {
        ammo = 12;
        currentBullets = 6;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        ammoText.text = "Ammo: " + ammo.ToString();
    }

    private void Move()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            --HealthManager.health;
            if(HealthManager.health <= 0)
            {
                //game over is true (switch scenes?)
                //gameObject.SetActive(false);              
            }
            else
            {
                //play hurt animation?
            }
        }
    }
}
