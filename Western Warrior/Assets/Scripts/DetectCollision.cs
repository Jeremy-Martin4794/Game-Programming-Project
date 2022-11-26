using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectCollision : MonoBehaviour
{
    [SerializeField] GameObject winTextObject;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] GameObject loseTextObject;
    [SerializeField] TextMeshProUGUI loseText;

    // Start is called before the first frame update
    void Start()
    {
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            if (!collision.gameObject.GetComponent<EnemyMovement>().isDead)
            {
                --HealthManager.health;
                if (HealthManager.health <= 0)
                {
                    gameObject.GetComponentInParent<PlayerMovement>().gameOver = true;
                    loseTextObject.SetActive(true);
                    //play death animation
                    gameObject.GetComponent<Animator>().SetBool("isDead", true);
                    //Destroy(gameObject, 4);
                    //scene transition
                }
                else
                {
                    //play hurt animation
                    gameObject.GetComponent<Animator>().SetBool("isHurt", true);
                    gameObject.GetComponentInParent<PlayerMovement>().isHurt = true;
                    gameObject.GetComponentInParent<PlayerMovement>().hurtCount = .3f;
                }
            }
        }
        else if (collision.transform.tag == "Trophy")
        {
            //game won
            gameObject.GetComponentInParent<PlayerMovement>().gameWon = true;
            winTextObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ammo")
        {
            Destroy(collision.gameObject);
            gameObject.GetComponentInParent<PlayerMovement>().ammo += 6;
        }
        else if(collision.transform.tag == "HealthPowerup")
        {
            Destroy(collision.gameObject);
            if(HealthManager.health < HealthManager.maxHealth)
            {
                ++HealthManager.health;
            }
        }
        else if(collision.transform.tag == "SpeedPowerup")
        {
            Destroy(collision.gameObject);
            gameObject.GetComponentInParent<PlayerMovement>().speedBoost = true;
            gameObject.GetComponentInParent<PlayerMovement>().speedBoostTime = 8;
        }
    }
}
