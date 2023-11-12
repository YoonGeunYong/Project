using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    
    
    void Start()
    {
        
    }
    
    void On2dColliderEnter(Collision2D other)
    {
        if (other.gameObject.CompareTag("Interaction"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
