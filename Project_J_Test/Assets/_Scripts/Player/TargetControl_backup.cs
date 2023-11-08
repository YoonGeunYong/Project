using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�⺻ �ݶ��̴��� �ν� �ݶ��̴� 2���� ���µ�, 2���� �ݶ��̴��� �Ѳ����� ���� ��
//�νİ� ���� �� ��������� �νĵ��� �˱� �����

public class TargetControl_backup : MonoBehaviour
{
    //Rigidbody2D rb;
    Transform player;
    Transform defaultPosition;
    public BoxCollider2D[] block = new BoxCollider2D[2];    //�̵� ���� ���� �ݶ��̴�

    [Header("�߰� �ӵ�")]
    [SerializeField][Range(1f, 4f)] float moveSpeed = 3f;

    [Header("�ν� ����")]
    [SerializeField][Range(0f, 5f)] float contactDistance = 1f;

    [SerializeField]bool follow = false;    //�ν� ����
    [SerializeField]bool contactDefaultLocation = true; //��� �������� ����
    [SerializeField]short enemyTurn = 1;    //���� ��

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
        if (Vector2.Distance(transform.position, player.position) < contactDistance && follow)  //�ν����� ��
        {
            transform.position = Vector2.MoveTowards(transform.position,
                player.position, moveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < contactDistance / 2 && follow) //���� ���� �� �϶�
			transform.position = Vector2.MoveTowards(transform.position,
				player.position, moveSpeed * Time.deltaTime);
		else if (!contactDefaultLocation)   //����Ʈ ��ġ�� ���ƿ��� 
        {
            transform.position = Vector2.MoveTowards(transform.position,
                defaultPosition.position, moveSpeed / 2 * Time.deltaTime);
        }
        else    //�Դٰ���
            transform.position += Vector3.left * moveSpeed / 2 * (Time.deltaTime * enemyTurn);
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

            case "block":
				enemyTurn *= -1;    //���� ��ȯ
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

    private void OnTriggerStay2D(Collider2D collision)  //�ʿ����
    {
        if (follow && collision.gameObject.CompareTag("block"))
        {
            collision.enabled = false;
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                block[i].enabled = true;
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "block")
    //        enemyTurn *= -1;
    //}

}
