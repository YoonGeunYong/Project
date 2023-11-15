using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    public int isMove;
    public float speed = 10.0f;
    private float input;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isMove == 2)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            input = Input.GetAxis("Horizontal");
            //transform.Translate(0, speed * Time.deltaTime * input, 0);
            if(this.name == "MoveBox (2)")
                rb.freezeRotation = false;
            rb.velocity = new Vector2(speed * input, rb.velocity.y);
        }
        else if(isMove == 1)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            if(this.name == "MoveBox (2)")
                rb.freezeRotation = true;
            ZeroVelocity();
        }
        else if (isMove == 0)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            if(this.name == "MoveBox (2)")
                rb.freezeRotation = false;
        }
    }

    public void ZeroVelocity()
    { 
        rb.velocity = Vector2.zero;
    }
}
