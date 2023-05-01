using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float speed = 0.2f;
	public float jumpForce = 5f;
	Rigidbody2D rB;

    float horizontal;

    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector3(horizontal * speed, 0, 0));

		if (Input.GetKeyDown(KeyCode.Space))
			rB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        
	}
}
