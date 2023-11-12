using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButtonClick : MonoBehaviour
{
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Interaction") && !collision.CompareTag("Player")) return;
        if (collision.gameObject.CompareTag("Interaction"))
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
