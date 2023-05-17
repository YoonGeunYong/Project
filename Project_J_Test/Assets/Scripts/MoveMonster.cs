using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveMonster : MonoBehaviour
{
	private Rigidbody2D rb;
	public int moveDir;
	public float speed = 3f;
	public float cliffRayLen = 2.0f;
	public float noticeRayLen = 6.0f;
	public float attackRayLen = 2.0f;
	public float rayXPos = 2.0f;

	private bool isAttack = false;

	enum MonsterStates { Patrol, Notice, Attack, Delay }

	private static MonsterStates currentState = MonsterStates.Patrol;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		//first move direction
		moveDir = Random.Range(0, 2);
		if(moveDir == 0) moveDir = -1;
	}

	void Update()
	{
		//search cliff
		Vector2 cliffV = new Vector2(transform.position.x + moveDir * rayXPos, transform.position.y);
		Debug.DrawRay(cliffV, Vector3.down * cliffRayLen, new Color(0, 0, 1));
		RaycastHit2D cliffRayHit = Physics2D.Raycast(cliffV, Vector3.down, cliffRayLen, LayerMask.GetMask("Platform"));

		//search player
		Debug.DrawRay(transform.position, Vector3.left * (noticeRayLen + 0.75f), new Color(0, 1, 0));
		RaycastHit2D noticeLRayHit = Physics2D.BoxCast(transform.position, new Vector2(1.5f, 2.5f), 0, Vector3.left, noticeRayLen, LayerMask.GetMask("Player"));
		Debug.DrawRay(transform.position, Vector3.right * (noticeRayLen + 0.75f), new Color(0, 1, 0));
		RaycastHit2D noticeRRayHit = Physics2D.BoxCast(transform.position, new Vector2(1.5f, 2.5f), 0, Vector3.right, noticeRayLen, LayerMask.GetMask("Player"));

		//closer player
		Debug.DrawRay(transform.position, Vector3.left * (attackRayLen + 0.75f), new Color(1, 0, 0));
		RaycastHit2D attackLRayHit = Physics2D.BoxCast(transform.position, new Vector2(1.5f, 2.5f), 0, Vector3.left, attackRayLen, LayerMask.GetMask("Player"));
		Debug.DrawRay(transform.position, Vector3.right * (attackRayLen + 0.75f), new Color(1, 0, 0));
		RaycastHit2D attackRRayHit = Physics2D.BoxCast(transform.position, new Vector2(1.5f, 2.5f), 0, Vector3.right, attackRayLen, LayerMask.GetMask("Player"));

		//change state
		if (attackLRayHit.collider != null || attackRRayHit.collider != null)
			currentState = MonsterStates.Attack;
		else if (noticeLRayHit.collider != null || noticeRRayHit.collider != null)
			currentState = MonsterStates.Notice;
		else
			currentState = MonsterStates.Patrol;

		switch (currentState)
		{
			case MonsterStates.Patrol:
				this.GetComponent<Renderer>().material.color = Color.yellow;

				//move forward
				//rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
				speed = 3f;
				transform.Translate(new Vector3(moveDir * speed * Time.deltaTime, 0, 0));

				//switch direction
				if (cliffRayHit.collider == null)
					moveDir *= -1;
				break;

			case MonsterStates.Notice:
				this.GetComponent<Renderer>().material.color = new Color(1, 0.66f, 0);

				//calc position with player
				GameObject gameObject = GameObject.FindWithTag("Player");
				if (this.transform.position.x < gameObject.transform.position.x)
					moveDir = 1;
				else
                    moveDir = -1;

				//chase speedly
				speed = 5f;
				transform.Translate(new Vector3(moveDir * speed * Time.deltaTime, 0, 0));
				break;

			case MonsterStates.Attack:
				this.GetComponent<Renderer>().material.color = Color.red;

				if (!isAttack)
                    StartCoroutine("attackMotion");


                break;
		}

	}

	IEnumerator attackMotion()
	{
		isAttack = true;
		this.transform.localScale = new Vector3(transform.localScale.x + 2.0f, transform.localScale.y, transform.localScale.z);
		yield return new WaitForSeconds(0.2f);
        this.transform.localScale = new Vector3(transform.localScale.x - 2.0f, transform.localScale.y, transform.localScale.z);
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
		yield break;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(collision.gameObject.name);
	}
}
