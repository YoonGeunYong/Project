using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//기본 콜라이더와 인식 콜라이더 2개를 쓰는데, 2개의 콜라이더에 한꺼번에 닿을 시
//인식과 공격 중 어느쪽으로 인식될지 알기 어려움

public class TargetControl : MonoBehaviour
{
    Transform player;
    Transform defaultPosition;

    [Header("추격 속도")]
    [SerializeField][Range(1f, 4f)] float moveSpeed = 3f;

    [Header("인식 범위")]
    [SerializeField][Range(0f, 5f)] float contactDistance = 1f;

    [SerializeField]bool follow = false;    //인식 여부
    [SerializeField]bool contactDefaultLocation = true; //대기 상태인지 여부
    [SerializeField]short enemyTurn = 1;    //방향 값

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        defaultPosition = GameObject.Find("DefaultPosition").GetComponent<Transform>();
    }
    
    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        //notice
        if (Vector2.Distance(transform.position, player.position) < contactDistance && follow)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                player.position, moveSpeed * Time.deltaTime);
        }
        //attack
        else if (Vector2.Distance(transform.position, player.position) < contactDistance / 2 && follow)
			transform.position = Vector2.MoveTowards(transform.position,
				player.position, moveSpeed * Time.deltaTime);
        //idk & to patrol
		else if (!contactDefaultLocation) 
        {
            transform.position = Vector2.MoveTowards(transform.position,
                defaultPosition.position, moveSpeed / 2 * Time.deltaTime);
        }
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
				follow = true;  //인식 했음을 의미
                break;

			case "DefaultPosition":
				contactDefaultLocation = true;  //원래 위치로 돌아왔음
                break;
			default:
                return;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //플레이어와 멀어졌을때(인식 범위 바깥으로)
        {
            follow = false;
            contactDefaultLocation = false;
        }
    }
}
