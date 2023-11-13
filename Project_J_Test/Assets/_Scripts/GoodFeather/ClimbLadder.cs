using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    GameObject subLadder;
    GameObject player;
    Vector3 goalPos;

    public bool isOpen;
    public float goalY;
    public bool isInteract;

    void Start()
    {
        if (transform.childCount > 0)
        {
            subLadder = this.transform.GetChild(0).gameObject;
            goalPos = new Vector3(subLadder.transform.position.x,
                subLadder.transform.position.y - goalY, subLadder.transform.position.z);
        }
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (subLadder is not null && isOpen && subLadder.transform.position != goalPos)
        {
            subLadder.transform.position = Vector3.MoveTowards(
                subLadder.transform.position, goalPos, 0.1f);
        }
    }
}
