using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform playerObjectTransform;
    private float speed = 2f;
    private int maxFollowDistance = 6;

    void Start()
    {
        
    }

    private void Update()
    {
        float offset = -90f;
        Vector2 direction = playerObjectTransform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));

        if (Mathf.Abs(transform.position.x - playerObjectTransform.position.x) < maxFollowDistance && Mathf.Abs(transform.position.y - playerObjectTransform.position.y) < maxFollowDistance) {
            transform.position = Vector2.MoveTowards(transform.position, playerObjectTransform.position, speed * Time.deltaTime);
        }
    }
}
