using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButtonClick : MonoBehaviour
{
    private bool _isActive;

    private void Start()
    {
        if(DataController.Instance.nowPlayerData.isActiveTrap)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Interaction") && !collision.CompareTag("Player")) return;
        if (collision.gameObject.CompareTag("Interaction"))
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
            DataController.Instance.nowPlayerData.isActiveTrap = true;
        }
    }
}
