using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButtonClick : MonoBehaviour
{
    void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = 3;
        }
    }
}
