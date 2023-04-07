using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    float moveX;
    [SerializeField] float jumpForce;
    [SerializeField] [Range(3f, 10f)] float moveSpeed = 5f;

    Rigidbody2D playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        Physics2D.gravity *= 1.5f;
    }

    
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + moveX,
            transform.position.y);
        if(Input.GetKeyDown(KeyCode.Space))
            playerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            
    }
}
