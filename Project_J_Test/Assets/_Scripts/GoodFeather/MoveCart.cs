using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCart : MonoBehaviour
{
    public float moveForce;
    public int horizontal;
    public int vertical;

    Rigidbody2D rb;
    bool isClick;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
           *//* collision.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 2);
            collision.gameObject.transform.SetParent(rb.transform);*//*
            rb.AddForce(new Vector2(horizontal, vertical) * moveForce, ForceMode2D.Impulse);
        }
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            //collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            //collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            collision.gameObject.transform.SetParent(rb.transform);
            
            if (Input.GetKeyDown(KeyCode.F))
                rb.AddForce(new Vector2(horizontal, vertical) * moveForce, ForceMode2D.Impulse);
        }
    }
}
