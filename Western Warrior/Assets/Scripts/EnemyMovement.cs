using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform playerObjectTransform;
    private float speed = 2f;
    private int maxFollowDistance = 10;
    public bool isHurt;
    public bool isDead;
    public int hurtTime;
    public int hurtCount;

    void Start()
    {
        gameObject.GetComponent<Animator>().SetBool("isActive", true);
        playerObjectTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isHurt = false;
        isDead = false;
        hurtTime = 0;
        hurtCount = 0;
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        bool isGameOver = player.GetComponentInParent<PlayerMovement>().gameOver;
        bool isGameWon = player.GetComponentInParent<PlayerMovement>().gameWon;
        if (!isGameOver && !isGameWon)
        {
            if (!isDead)
            {
                float offset = -90f;
                Vector2 direction = playerObjectTransform.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));

                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(playerObjectTransform.position.x, playerObjectTransform.position.y)) < maxFollowDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, playerObjectTransform.position, speed * Time.deltaTime);
                }

                if (isHurt)
                {
                    if (hurtTime <= 0)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isHurt", false);
                        isHurt = false;
                        hurtTime = 0;
                    }
                    else
                        --hurtTime;
                }
            }
        }
    }
}
