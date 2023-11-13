using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 10.0f;      //요괴보다 조금 빠르게 설정함(요괴 speed = 3)
    public float jumpForce = 14.0f;
    public float rayLength = 3f;

    Rigidbody2D rb;
    FixedJoint2D fixJoint;
    GameObject box;
    GameObject ladder;
    public Animator anim;
    public GameObject pebbleStone;
    public GameObject dropWheel;

    private int isLaddering; //타지않음 0, 닿았음 1, 탔음 2
    public bool isRunning;
    public bool isJumping;
    public bool isRope;
    public bool haveStone;
    public float inputH;
    public float inputV;
    public bool isPush;
    public bool isPushing;

    void Start()
    {
        fixJoint = GetComponent<FixedJoint2D>();
        fixJoint.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        DataController.Instance.LoadObjPosition();
        transform.position = DataController.Instance.nowPlayerData.playerPositionTutorial;
    }

    void Update()
    {
        if (GameManager.GM.dieing || !GameManager.GM.isRunning)
        {
            anim.speed = 0f;
            rb.velocity = Vector2.zero;
            return;
        }
        switch (isLaddering)
        {
            //move
            //땅에 있을 때
            case < 2:
            {
                inputH = Input.GetAxis("Horizontal");
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    rb.velocity = new Vector2(speed * inputH * 1.75f, rb.velocity.y);
                    isRunning = true;
                }
                else
                {
                    rb.velocity = new Vector2(speed * inputH, rb.velocity.y);
                    isRunning = false;
                }

                break;
            }
            //사다리에 있을 때
            case 2:
            {
                inputV = Input.GetAxis("Vertical");
                if (inputV != 0)
                {
                    anim.speed = 1f;
                    transform.Translate(new Vector3(0, speed * Time.deltaTime * inputV, 0));
                }
                else
                {
                    anim.speed = 0f;
                }

                break;
            }
        }

        if (!isPushing)
        {
            switch (isRunning)
            {
                case false when (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)):
                {
                    anim.SetBool("Walking", true);
                    anim.SetBool("Running", false);
                    break;
                }
                case true when (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)):
                {
                    anim.SetBool("Running", true);
                    anim.SetBool("Walking", false);
                    break;
                }
                default:
                    anim.SetBool("Walking", false);
                    anim.SetBool("Running", false);
                    break;
            }
            if (inputH < 0)
                transform.localScale = new Vector3(-0.75f, transform.localScale.y, 1);
            else
                transform.localScale = new Vector3(0.75f, transform.localScale.y, 1);
        }

        switch (isLaddering)
        {
            //gravity on the ladder
            //사다리 타기
            case 1 when Input.GetKeyDown(KeyCode.UpArrow):
                rb.velocity = Vector3.zero;
                transform.position = new Vector3(ladder.transform.position.x, transform.position.y, 0);
                isLaddering = 2;
                rb.gravityScale = 0;
                anim.SetBool("Laddering", true);
                anim.speed = 1f;
                //rb.bodyType = RigidbodyType2D.Kinematic;
                break;
            //탄 상태에서 점프
            case 2 when Input.GetKeyDown(KeyCode.Space):
                anim.speed = 1f;
                isLaddering = 1;
                rb.gravityScale = 1;
                //rb.bodyType = RigidbodyType2D.Dynamic;
                anim.SetBool("Laddering", false);
                anim.SetBool("Jumpping", true);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                break;
        }
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector3.down, rayLength,
            LayerMask.GetMask("Platform"));
        
        if (isPush && !rayHit.collider.CompareTag("MoveObj") && Input.GetKeyDown(KeyCode.F))
        {
            box.gameObject.GetComponent<MoveBox>().isMove = true;
            anim.SetBool("Box", true);
            isPushing = true;
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            box.gameObject.GetComponent<MoveBox>().isMove = false;
            anim.SetBool("Box", false);
            isPushing = false;
            isPush = false;
        }


        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //merge 11.08
            if (!isJumping || (isRope && fixJoint.connectedBody is not null))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            fixJoint.connectedBody = null;
            fixJoint.enabled = false;
            anim.SetBool("Rope", false);
        }

        switch (GameManager.GM.itemNum)
        {
            case 2:
                if (haveStone && Input.GetKeyDown(KeyCode.Q))
                {
                    DataController.Instance.UseItem(GameManager.GM.itemInt);
                    haveStone = false;
                    pebbleStone = Instantiate(pebbleStone, transform.position + new Vector3(2f, 2f, 0f),
                        Quaternion.identity);
                    pebbleStone.GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 10f), ForceMode2D.Impulse);
                }

                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    DataController.Instance.UseItem(GameManager.GM.itemInt);
                    dropWheel = Instantiate(dropWheel, transform.position + new Vector3(2f, 0f, 0f),
                        Quaternion.identity);
                    dropWheel.GetComponent<Rigidbody2D>().AddForce(new Vector2(5f, -0.2f), ForceMode2D.Impulse);
                }

                break;

        }

        //jump raycast
        Debug.DrawRay(transform.position, Vector3.down * rayLength, new Color(1, 0, 0));

        //jump check with eyes
        if (rayHit.collider is not null)
        {
            isJumping = false;
            isRope = false;
            anim.SetBool("Jumpping", false);
        }
        else
        {
            isJumping = true;
            anim.SetBool("Jumpping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            GameManager.GM.dieing = true;
        }
        
        if(!isPush && other.gameObject.CompareTag("MoveObj"))
        {            
            isPush = true;
            box = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("MoveObj"))
        {            
            isPush = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            ladder = other.gameObject;
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
            anim.SetBool("Rope", true);
            isRope = true;
        }

        if (other.name == "SceneMove")
        {
            GameManager.GM.dieing = true;
            DataController.Instance.nowPlayerData.tutorialClear = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Interaction") || GameManager.GM.itemNum != 1) return;
        if (Input.GetKey(KeyCode.Q))
        {
            other.GetComponent<DoorControll>().checkItem = true;
            DataController.Instance.UseItem(GameManager.GM.itemInt);
        }
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isLaddering = 0;
            rb.gravityScale = 3;
            anim.SetBool("Laddering", false);
        }

        if (other.gameObject.CompareTag("pRope"))
        {
            isRope = false;
        }
    }
}
