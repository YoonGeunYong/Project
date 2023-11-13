using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBox : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject item2;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Interaction"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = new Vector3(10f, 0f , 0f);
            item2.SetActive(false);
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            rb.velocity = Vector3.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            
        }
    }
}
