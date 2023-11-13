using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            rb.bodyType = RigidbodyType2D.Kinematic;
        
        if (other.gameObject.CompareTag("Player") && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            GameManager.GM.dieing = true;
            //얘가 문제인데;;;
            DataController.Instance.nowPlayerData.setObjPosition[1] = gameObject.transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
