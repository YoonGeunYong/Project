using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 10.0f;      //요괴보다 조금 빠르게 설정함(요괴 speed = 3)
    public float jumpForce = 14.0f;
    public float rayLength = 3f;

    Rigidbody2D rb;
    FixedJoint2D fixJoint;
    Animator anim;

    public bool isJumping;
    int isLaddering;    //타지않음 0, 닿았음 1, 탔음 2 
    public bool isRope;
    public float input;

    void Start()
    {
        fixJoint = GetComponent<FixedJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //transform.position = DataController.Instance.nowPlayerData.playerPositionTutorial;
        GameManager.GM.hpGauge = DataController.Instance.nowPlayerData.playerHP;
    }

    void Update()
    {
        //move
        if (isLaddering < 2)    //땅에 있을 때
        {
            input = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(speed * input, rb.velocity.y);
        }
        else if (isLaddering == 2)  //사다리에 있을 때
        {
            if (Input.GetKey(KeyCode.DownArrow))
                transform.Translate(new Vector3(0, speed * Time.deltaTime * -1, 0));

            if (Input.GetKey(KeyCode.UpArrow))
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }

        if (input != 0)
        {
            anim.SetBool("Walking", true);
            if (input < 0)
                transform.localScale = new Vector3(-0.75f, transform.localScale.y, 1);
            else
                transform.localScale = new Vector3(0.75f, transform.localScale.y, 1);
        }
        else
        {
            anim.SetBool("Walking", false);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if(isRope && fixJoint.connectedBody is not null)
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
        if (rayHit.collider is not null)
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
            DataController.Instance.nowPlayerData.playerHP = GameManager.GM.hpGauge;
        }

        //if (other.gameObject.CompareTag("Pulley"))
        //{
        //    Debug.Log(other.gameObject.name);
        //    if (transform.position.x < other.gameObject.transform.position.x)
        //        this.transform.SetParent(other.gameObject.transform.GetChild(0));
        //    else
        //        this.transform.SetParent(other.gameObject.transform.GetChild(1));
        //}
    }

    //private void OnCollisionExit2D(Collision2D other)
    //{
        //if (other.gameObject.CompareTag("Pulley"))
        //{
        //    this.transform.SetParent(null);
        //}
    //}

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
            rb.gravityScale = 3;
        }

        if (other.gameObject.CompareTag("pRope"))
        {
            isRope = false;
        }
    }
}
