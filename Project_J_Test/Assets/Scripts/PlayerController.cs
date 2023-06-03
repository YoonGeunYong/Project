using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 4.0f;      //¿ä±«º¸´Ù Á¶±Ý »¡¶ó¾ßÇÔ(¿ä±« speed = 3)
	public float jumpPower = 12.0f;	
	public float rayLength = 1.5f;

	private bool isJumping = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(new Vector3(speed * Time.deltaTime * -1, 0, 0));
			//rb.velocity = new Vector2(speed * -1, rb.velocity.y);
		}

		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
			//rb.velocity = new Vector2(speed, rb.velocity.y);
		}

		if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
			rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

		//Vector2 downV = new Vector2(transform.position.x + moveDir * rayXPos, rb.position.y);
		Debug.DrawRay(transform.position, Vector3.down * rayLength, new Color(1, 0, 0));
		RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector3.down, rayLength, LayerMask.GetMask("Platform"));

		if (rayHit.collider != null)
		{
			isJumping = false;
			this.GetComponent<Renderer>().material.color = Color.white;
		}
		else
		{
			isJumping = true;
			this.GetComponent<Renderer>().material.color = Color.blue;
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Monster")
		{
			Debug.Log("Damaged!");

			//spawn position
			transform.position = new Vector3(2, 4, 0);
		}
	}
}
