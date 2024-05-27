using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MoveCart : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;

    private bool isRide;
    private bool animStart;
    private Animator anim;
    private GameObject player;
    public GameObject keyE;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isRide && !DataController.Instance.nowPlayerData.activedCart && Input.GetKeyDown(KeyCode.E))
        {
            player.SetActive(false);
            mainCamera.Follow = gameObject.transform;
            this.transform.GetChild(0).gameObject.SetActive(true);           
            isRide = false;
            animStart = true;
            keyE.SetActive(false);

            StartCoroutine("CartAnim");
        }
    }

    IEnumerator CartAnim()
    {
        
        anim.SetBool("isCart", true);
        yield return new WaitForSeconds(14f);
        player.transform.position = transform.position;
        this.transform.GetChild(0).gameObject.SetActive(false);
        yield return null;      //카트에서 내리는 애니메이션 재생 시 시간 대기 추가
        player.gameObject.SetActive(true);
        mainCamera.Follow = player.transform;
        keyE.SetActive(false);

        DataController.Instance.nowPlayerData.activedCart = true;
        anim.speed = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !animStart)
        {
            keyE.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            isRide = true;
            //keyE.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            keyE.SetActive(false);
    }
}
