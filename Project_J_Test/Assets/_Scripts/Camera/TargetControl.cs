using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControl : MonoBehaviour
{
    //Rigidbody2D rb;
    Transform player;
    Transform defaultPosition;
    public BoxCollider2D[] block = new BoxCollider2D[2];

    [Header("추격 속도")]
    [SerializeField][Range(1f, 4f)] float moveSpeed = 3f;

    [Header("인식 범위")]
    [SerializeField][Range(0f, 5f)] float contactDistance = 1f;

    [SerializeField]bool follow = false;
    [SerializeField]bool contactDefaultLocation = true;
    [SerializeField]short enemyTurn = 1;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        defaultPosition = GameObject.Find("DefaultPosition").GetComponent<Transform>();
    }

    
    void Update()
    {
        FollowTarget();
        //if(!follow && contactDefaultLocation)
    }

    void FollowTarget()
    {
        if (Vector2.Distance(transform.position, player.position) < contactDistance && follow)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                player.position, moveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < contactDistance / 2 && follow)
			transform.position = Vector2.MoveTowards(transform.position,
				player.position, moveSpeed * Time.deltaTime);
		else if (!contactDefaultLocation)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                defaultPosition.position, moveSpeed / 2 * Time.deltaTime);
        }
        else 
            transform.position += Vector3.left * moveSpeed / 2 * Time.deltaTime * enemyTurn;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    follow = true;
        //}
        //else if (other.CompareTag("DefaultPosition"))
        //    contactDefaultLocation = true;
        //else if (other.CompareTag("block"))
        //    enemyTurn *= -1;
        switch (other.tag)
        {
            case "Player":
				follow = true;
                break;

			case "DefaultPosition":
				contactDefaultLocation = true;
                break;

            case "block":
				enemyTurn *= -1;
                break;

			default:
                return;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            follow = false;
            contactDefaultLocation = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (follow && collision.gameObject.tag == "block")
        {
            collision.enabled = false;
        }
        else
            for (int i = 0; i < 2; i++)
            {
                block[i].enabled = true;
            }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "block")
    //        enemyTurn *= -1;
    //}

}
