using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float speed = 0.1f;
	public float jumpForce = 5f;
	Rigidbody2D rB;

    float ho;

    Vector2 playermove;

    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        ho = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector3(ho * speed, 0, 0));

		if (Input.GetKeyDown(KeyCode.Space))
			rB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
	}
}
