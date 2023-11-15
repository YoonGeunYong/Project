using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 StartAct2 = new Vector3(2717, -65, 0);
    public float EndAct2X = 2897f;

    public float speed = 10f;
    public int act;

    void Start()
    {
        act = 0;
    }

    void Update()
    {
        int xDir = player.transform.position.x < transform.position.x ? -1 : 1;
        transform.localScale = new Vector3(-xDir * 2, 2, 1);

        if (act == 1)
        {            
            transform.Translate(Vector2.right * speed * xDir * Time.deltaTime);
        }
        else if (act == 2)
        {
            if (transform.position.y < -80)
                transform.position = StartAct2;

            speed = 17f;

            if(transform.position.x < EndAct2X)
                transform.Translate(Vector2.right * speed * xDir * Time.deltaTime);
        }
    }
}
