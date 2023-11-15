using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public int interactableNum = 0;
    public Sprite[] sprite = new Sprite[7];
    public GameObject[] interactable = new GameObject[5];
    
    SpriteRenderer spriteRenderer;
    
    
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite[interactableNum];
    }

    
    void Update()
    {
        if(interactableNum == 0 && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            interactableNum += 1;
            spriteRenderer.sprite = sprite[interactableNum];
        }
        if(interactableNum == 1 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            interactableNum += 1;
            spriteRenderer.sprite = sprite[interactableNum];
        }
        if(interactableNum == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            interactableNum += 1;
            spriteRenderer.sprite = sprite[interactableNum];
            gameObject.SetActive(false);
            interactable[0].SetActive(true);
        }
        if(interactableNum == 3 && Input.GetKeyDown(KeyCode.F))
        {
            interactableNum += 1;
            spriteRenderer.sprite = sprite[interactableNum];
            gameObject.SetActive(false);
            interactable[1].SetActive(true);
        }
        if(interactableNum == 4 && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            interactableNum += 1;
            spriteRenderer.sprite = sprite[interactableNum];
            gameObject.SetActive(false);
            interactable[2].SetActive(true);
        }
        if(interactableNum == 5 && Input.GetKeyDown(KeyCode.E))
        {
            interactableNum += 1;
            spriteRenderer.sprite = sprite[interactableNum];
            gameObject.SetActive(false);
            interactable[3].SetActive(true);
        }
        if (interactableNum == 6 && Input.GetKeyDown(KeyCode.Alpha1))
        {
            interactableNum += 1;
            spriteRenderer.sprite = sprite[interactableNum];
            gameObject.SetActive(false);
            interactable[4].SetActive(true);
        }
        if (interactableNum == 7 && Input.GetKeyDown(KeyCode.Q))
        {
            spriteRenderer.sprite = sprite[interactableNum];
            gameObject.SetActive(false);
        }
    }
}
