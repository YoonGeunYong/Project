using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIntoPulley : MonoBehaviour
{
    GameObject LBasket;
    GameObject RBasket;

    bool isMoving;
    bool isInteract;

    public  float moveSpeed = 0.05f;
    private int   stoneNum;
    public  int   maxStone = 3;

    Vector3 newLPos;
    Vector3 newRPos;

    void Start()
    {
        LBasket  = this.transform.GetChild(0).gameObject;
        RBasket  = this.transform.GetChild(1).gameObject;
        stoneNum = maxStone;
    }

    void Update()
    {
        if (isMoving)
        {
            LBasket.transform.position = Vector3.MoveTowards(
                LBasket.transform.position, newLPos, moveSpeed);
            RBasket.transform.position = Vector3.MoveTowards(
                RBasket.transform.position, newRPos, moveSpeed);
        }

        if (LBasket.transform.position == newLPos && RBasket.transform.position == newRPos)
            isMoving = false;

        if (Input.GetKeyDown(KeyCode.F) && isInteract && !isMoving)
        {
            if (stoneNum > 0)
            {
                stoneNum--;
                Debug.Log("stone: " + stoneNum);
            }
            else
            {
                newLPos = new Vector3(
                    LBasket.transform.position.x, RBasket.transform.position.y, LBasket.transform.position.z);
                newRPos = new Vector3(
                    RBasket.transform.position.x, LBasket.transform.position.y, RBasket.transform.position.z);
                isMoving = true;
                stoneNum = maxStone;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        
        isInteract = true;

        bool _isLeft = transform.position.x > collision.gameObject.transform.position.x;
        collision.transform.SetParent(gameObject.transform.GetChild(_isLeft ? 0 : 1));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        
        isInteract = false;

        collision.transform.SetParent(null);
    }
}