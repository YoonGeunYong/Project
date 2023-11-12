using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCart : MonoBehaviour
{
    public GameObject mainCamera;

    bool isRide;
    bool isActive;
    Animator anim;
    GameObject player;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isRide && Input.GetKeyDown(KeyCode.F))
        {            
            player.SetActive(false);
            mainCamera.GetComponent<MoveCamera>().targetObj = this.gameObject;
            mainCamera.GetComponent<MoveCamera>().enabled = true;            
            mainCamera.GetComponent<CinemachineBrain>().enabled = false;
            this.transform.GetChild(0).gameObject.SetActive(true);           
            isRide = false;
            
            StartCoroutine("CartAnim");
        }
    }

    IEnumerator CartAnim()
    {
        
        anim.SetBool("isCart", true);
        yield return new WaitForSeconds(9f);
        player.transform.position = transform.position;               
        this.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);      //카트에서 내리는 애니메이션 재생 시 시간 대기 추가
        mainCamera.GetComponent<MoveCamera>().enabled = false;
        player.gameObject.SetActive(true);
        mainCamera.GetComponent<CinemachineBrain>().enabled = true;
        

        isActive = false;        
        anim.SetBool("isCart", false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            isRide = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isRide = false;            
        }
    }
}
