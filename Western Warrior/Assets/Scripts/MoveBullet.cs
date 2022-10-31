using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private float speed = 20f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject playerObject;
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void Update()
    {
        Destroy(gameObject, .6f);
    }
}
