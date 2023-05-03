using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 15;
    public float speed = 20;
    float input;

    Vector3 moveDirX;
	int defaultLayer;
	int hideLayer;

    Rigidbody2D playerRigidbody;
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
		defaultLayer = LayerMask.NameToLayer("Player");
		hideLayer = LayerMask.NameToLayer("HidePlayer");
    }

    void Update()
    {
        input = Input.GetAxis("Horizontal");
		if (Input.GetAxis("Horizontal") != 0)
        {
			moveDirX = new Vector3(input, 0, 0).normalized;
			transform.position += moveDirX * speed * Time.deltaTime;
		}
		if (Input.GetKeyDown(KeyCode.Space))
			playerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("Monster"))
        {
                GameManager.GM.hpGauge -= 0.34f;
        }

        if (other.CompareTag("HelpObject"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Dust"))
		{
			gameObject.layer = hideLayer;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("HelpObject"))
			transform.GetChild(0).gameObject.SetActive(false);

		if (other.CompareTag("Dust"))
			gameObject.layer = defaultLayer;
	}
}
