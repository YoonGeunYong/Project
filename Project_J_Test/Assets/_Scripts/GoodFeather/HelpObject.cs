using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpObject : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject interactionKey;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionKey.gameObject.SetActive(false);
            helpPanel.gameObject.SetActive(false);
        }
    }
}
