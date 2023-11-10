using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject targetObj;
    void Start()
    {
        
    }

    void Update()
    {
        if(targetObj != null)
        {
            transform.position = new Vector3(targetObj.transform.position.x,
                targetObj.transform.position.y, transform.position.z);
        }       
    }
}
