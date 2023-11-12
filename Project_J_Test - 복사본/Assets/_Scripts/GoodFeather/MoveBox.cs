using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    public bool isMove;
    public float speed = 10.0f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isMove)
        {
            float input = Input.GetAxis("Horizontal");
            //transform.Translate(0, speed * Time.deltaTime * input, 0);
            rb.velocity = new Vector2(speed * input, rb.velocity.y);
        }
        else
            rb.velocity = Vector3.zero;
    }
}
