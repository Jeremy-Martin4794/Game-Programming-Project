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
    [SerializeField] Animator animatorEnemy;

    // Start is called before the first frame update
    void Start()
    {
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        animatorEnemy.SetBool("isActive", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("test");
        if (collision.transform.tag == "Enemy")
        {
            --HealthManager.health;
            if (HealthManager.health <= 0)
            {
                //game over is true (switch scenes?)
                //gameObject.SetActive(false);
                loseTextObject.SetActive(true);
                animatorEnemy.SetBool("isActive", false);
            }
            else
            {
                //play hurt animation?
            }
        }
        else if (collision.transform.tag == "Trophy")
        {
            //game won
            winTextObject.SetActive(true);
            animatorEnemy.SetBool("isActive", false);
        }
    }
}
