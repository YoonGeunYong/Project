using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorControll : MonoBehaviour
{
    float positionY;
    // Start is called before the first frame update
    void Start()
    {
        positionY = transform.position.y + 20;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < positionY)
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, positionY, transform.position.z), 0.01f);
    }
}
