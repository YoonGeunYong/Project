using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class End : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public GameObject finish;
    public GameObject image;
    
    bool isRide;

    // Update is called once per frame
    void Update()
    {
        if (isRide)
        {
            StartCoroutine("EndMove");
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            vcam.Follow = null;
            isRide = true;
            other.gameObject.SetActive(false);
        }
        
    }
    
    IEnumerator EndMove()
    {
        yield return new WaitForSeconds(1.5f);
        if (transform.position != finish.transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, finish.transform.position, 0.01f);
        }
        else if (transform.position == finish.transform.position)
        {
            image.SetActive(true);
            Destroy(this);
        }
    }
}
