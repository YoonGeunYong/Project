using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�⺻ �ݶ��̴��� �ν� �ݶ��̴� 2���� ���µ�, 2���� �ݶ��̴��� �Ѳ����� ���� ��
//�νİ� ���� �� ��������� �νĵ��� �˱� �����

public class TargetControl : MonoBehaviour
{
    Transform player;
    Transform defaultPosition;

    [Header("�߰� �ӵ�")]
    [SerializeField][Range(1f, 4f)] float moveSpeed = 3f;

    [Header("�ν� ����")]
    [SerializeField][Range(0f, 5f)] float contactDistance = 1f;

    [SerializeField]bool follow = false;    //�ν� ����
    [SerializeField]bool contactDefaultLocation = true; //��� �������� ����
    [SerializeField]short enemyTurn = 1;    //���� ��

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
				follow = true;  //�ν� ������ �ǹ�
                break;

			case "DefaultPosition":
				contactDefaultLocation = true;  //���� ��ġ�� ���ƿ���
                break;
			default:
                return;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //�÷��̾�� �־�������(�ν� ���� �ٱ�����)
        {
            follow = false;
            contactDefaultLocation = false;
        }
    }
}
