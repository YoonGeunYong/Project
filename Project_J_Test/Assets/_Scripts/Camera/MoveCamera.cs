using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject targetObj;
    void Start()
    {
        //targetObj = GameObject.Find("Player");
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.U))
            targetObj = GameObject.Find("Cart");*/

        transform.position = new Vector3(targetObj.transform.position.x,
            targetObj.transform.position.y, transform.position.z);
    }
}
