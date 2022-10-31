using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    private int currentBullets;
    [SerializeField] Image[] bullets;
    [SerializeField] Sprite fullBullet;
    [SerializeField] Sprite emptyBullet;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentBullets = playerMovement.currentBullets;
    }

    // Update is called once per frame
    void Update()
    {
        currentBullets = playerMovement.currentBullets;
        foreach (Image bullet in bullets)
        {
            bullet.sprite = emptyBullet;
        }
        for (int i = 0; i < currentBullets; ++i)
        {
            bullets[i].sprite = fullBullet;
        }
    }
}
