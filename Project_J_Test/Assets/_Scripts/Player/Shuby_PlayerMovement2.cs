using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shuby_PlayerMovement2 : MonoBehaviour
{
    public float speed = 10.0f;      //요괴보다 조금 빠르게 설정함(요괴 speed = 3)
    public float jumpForce = 14.0f;
    public float rayLength = 3f;

    Rigidbody2D rb;
    FixedJoint2D fixJoint;
    GameObject box;
    float localY;

    public bool isJumping;    
    public bool isRope;
    int isLaddering;    //타지않음 0, 닿았음 1, 탔음 2 
    bool isPush;

    void Start()
    {
        fixJoint = GetComponent<FixedJoint2D>();
        rb = GetComponent<Rigidbody2D>();

        transform.position = DataController.Instance.nowPlayerData.playerPositionTutorial;
    }

    void Update()
    {
        if(isPush && Input.GetKey(KeyCode.F))
        {
            Debug.Log("asdf");
            box.gameObject.GetComponent<MoveBox>().isMove = true;
            //box.transform.position = box.transform.position + Vector3.right;
        }
        
        if(isPush && Input.GetKeyUp(KeyCode.F))
        {
            Debug.Log("QQQq");
            box.gameObject.GetComponent<MoveBox>().isMove = false;
            isPush = false;
        }

        //move
        if (isLaddering < 2)    //땅에 있을 때
        {
            float input = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(speed * input, rb.velocity.y);
        }
        else if (isLaddering == 2)  //사다리에 있을 때
        {
            if (Input.GetKey(KeyCode.DownArrow))
                transform.Translate(new Vector3(0, speed * Time.deltaTime * -1, 0));

            if (Input.GetKey(KeyCode.UpArrow))
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }

        //gravity on the ladder
        if (isLaddering == 1 && Input.GetKeyDown(KeyCode.UpArrow)) //사다리 타기
        {
            rb.velocity = Vector3.zero;
            isLaddering = 2;
            rb.gravityScale = 0;
            //rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else if (isLaddering == 2 && Input.GetKeyDown(KeyCode.Space))   //탄 상태에서 점프
        {
            isLaddering = 1;
            rb.gravityScale = 1;
            //rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //Debug.Log($"ladder number : {isLaddering}");


        //jump
        if (!isPush && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if(isRope && fixJoint.connectedBody != null)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            fixJoint.connectedBody = null;
            fixJoint.enabled = false;
        }
        
        //jump raycast
        Debug.DrawRay(transform.position, Vector3.down * rayLength, new Color(1, 0, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector3.down, rayLength, LayerMask.GetMask("Platform"));

        //jump check with eyes
        if (rayHit.collider != null)
        {
            isJumping = false;
            isRope = false;
        }
        else
        {
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Damaged!");
            GameManager.GM.hpGauge -= 0.34f;
        }

        if(!isPush && other.gameObject.CompareTag("MoveObj"))
        {            
            isPush = true;
            box = other.gameObject;

            //localY = Mathf.Abs(box.transform.position.y - transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            Debug.Log(other.gameObject.name);
            if (other.GetComponent<ClimbLadder>().isOpen)
            {
                isLaddering = 1;
            }
        }

        if (other.CompareTag("Rope") && !isRope)
        {
            Rigidbody2D rig = other.gameObject.GetComponent<Rigidbody2D>();
            fixJoint.enabled = true;
            fixJoint.connectedBody = rig;
            isRope = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isLaddering = 0;
            rb.gravityScale = 1;
        }
    }
}
