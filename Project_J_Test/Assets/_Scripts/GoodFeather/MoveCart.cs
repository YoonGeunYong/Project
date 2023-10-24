using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCart : MonoBehaviour
{
    public float moveForce;
    public int horizontal;
    public int vertical;

    Rigidbody2D rb;
    bool isClick;

    bool isRide;
    bool isActive;
    Animator anim;
    GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isRide && Input.GetKeyDown(KeyCode.F))
        {            
            player.SetActive(false);
            this.transform.GetChild(0).gameObject.SetActive(true);           
            isRide = false;
            
            StartCoroutine("CartAnim");
        }
    }

    IEnumerator CartAnim()
    {
        anim.SetBool("isCart", true);
        yield return new WaitForSeconds(2.5f);
        player.transform.position = transform.position;
        this.transform.GetChild(0).gameObject.SetActive(false);
        yield return null;      //카트에서 내리는 애니메이션 재생 시 시간 대기 추가
        player.gameObject.SetActive(true);

        isActive = false;        
        //anim.SetBool("isCart", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            isRide = true;
        }
    }
}
