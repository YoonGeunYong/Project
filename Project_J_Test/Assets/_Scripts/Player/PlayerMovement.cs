using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 15;
    public float speed = 20;
	public float maxSpeed;
    float input;

    Vector3 moveDirX;
	int defaultLayer;
	int hideLayer;

	public bool asd;

    Rigidbody2D playerRigidbody;

	void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
		defaultLayer = LayerMask.NameToLayer("Player");
		hideLayer = LayerMask.NameToLayer("HidePlayer");
		transform.position = DataController.Instance.nowPlayerData.playerPositionTutorial;
		GameManager.GM.hpGauge = DataController.Instance.nowPlayerData.playerHP;
	}

	void Update()
    {

	}

	void FixedUpdate()
	{
		input = Input.GetAxis("Horizontal");
				
		playerRigidbody.velocity = new Vector2(maxSpeed * input, playerRigidbody.velocity.y);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			playerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.tag)
		{
			case "Monster":
				GameManager.GM.hpGauge -= 0.34f;
				DataController.Instance.nowPlayerData.playerHP = GameManager.GM.hpGauge;
				break;
			case "HelpObject":
				transform.GetChild(0).gameObject.SetActive(true);
				break;
			case "Item":
				ItemManager.IM.chechItem = true;
				other.gameObject.SetActive(false);
				break;

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
