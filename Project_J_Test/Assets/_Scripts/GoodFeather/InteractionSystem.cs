using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            GameObject.Find("HelpPanel").transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
