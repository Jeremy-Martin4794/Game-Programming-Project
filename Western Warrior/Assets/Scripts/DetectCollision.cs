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

    [SerializeField] AudioSource hurt;
    [SerializeField] AudioSource die;
    private bool deadSoundPlayed;

    // Start is called before the first frame update
    void Start()
    {
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        deadSoundPlayed = false;
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
                --gameObject.GetComponentInParent<HealthManager>().health;
                if (gameObject.GetComponentInParent<HealthManager>().health <= 0)
                {
                    if (!deadSoundPlayed)
                    {
                        die.Play();
                        deadSoundPlayed = true;
                    }
                    gameObject.GetComponentInParent<PlayerMovement>().gameOver = true;
                    loseTextObject.SetActive(true);
                    //play death animation
                    gameObject.GetComponent<Animator>().SetBool("isDead", true);
                    //Destroy(gameObject, 4);
                }
                else
                {
                    hurt.Play();
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
            if(gameObject.GetComponentInParent<HealthManager>().health < HealthManager.maxHealth)
            {
                ++gameObject.GetComponentInParent<HealthManager>().health;
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
