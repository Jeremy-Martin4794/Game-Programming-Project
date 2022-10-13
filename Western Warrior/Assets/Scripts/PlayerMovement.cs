using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementSpeed = 5;
    private Vector2 rotate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey("d"))
        {
            Debug.Log("test");
            transform.Translate(new Vector2(1, 0) * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey("a"))
        {
            Debug.Log("test");
            transform.Translate(new Vector2(-1, 0) * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey("w"))
        {
            Debug.Log("test");
            transform.Translate(new Vector2(0, 1) * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            Debug.Log("test");
            transform.Translate(new Vector2(0, -1) * movementSpeed * Time.deltaTime);
        }
    }
}
