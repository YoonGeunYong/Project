using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject line;
    
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
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            DataController.Instance.nowPlayerData.setObjPosition[1] = gameObject.transform.position;
            rb.velocity = Vector2.zero;
            DataController.Instance.nowPlayerData.isActiveTrap = true;
        }

        if (other.gameObject.CompareTag("Player") && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            GameManager.GM.dieing = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
        }
    }
}
