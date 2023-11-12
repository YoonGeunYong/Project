using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform player;
    public Transform ground;
    public int moveDir;
    public float speed = 3f;
    public float cliffRayLen = 2.0f;
    public float noticeDis = 6.0f;
    public float attackDis = 2.0f;
    public float rayXPos = 2.0f;

    private Rigidbody2D rb;
    private bool isNotice;
    private bool isAttack;  
    private bool isBack;

    private float delayTime;

    public enum MonsterStates { Patrol, Notice, Attack, Delay }    //Delay는 아직 미사용

    public  MonsterStates currentState = MonsterStates.Patrol;
    private float         attackDelay = .5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //처음 방향은 랜덤
        moveDir = Random.Range(0, 2);
        if (moveDir == 0) moveDir = -1;
    }

    void Update()
    {
        //절벽 검사 (왔다갔다)
        Vector2 cliffV = new Vector2(transform.position.x + moveDir * rayXPos, rb.position.y);
        Debug.DrawRay(cliffV, Vector3.down * cliffRayLen, new Color(0, 0, 1));
        RaycastHit2D cliffRayHit = Physics2D.Raycast(cliffV, Vector3.down, cliffRayLen, LayerMask.GetMask("Platform"));

        //인식 검사
        var distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackDis && isNotice)
            currentState = MonsterStates.Attack;
        else if(distance <= noticeDis && isNotice)//&& !isBack
            currentState = MonsterStates.Notice;
        else
            currentState = MonsterStates.Patrol;

        switch (currentState)
        {
            case MonsterStates.Patrol:
                this.GetComponent<Renderer>().material.color = Color.green;

                //go back to default position
                /*if(isBack)
                {
                    if (this.transform.position.x < ground.position.x)
                        moveDir = 1;
                    else
                        moveDir = -1;
                    //디폴트 위치로 되돌아간 다음 isBack을 false로 바꿔주는 것까지(디폴트 위치에 도달까지 한 뒤 바꿔야함)
                    transform.Translate(new Vector3(moveDir * speed * Time.deltaTime, 0, 0));

                    if (this.transform.position.x == ground.position.x)
                        isBack = false;
                    break;
                }*/

                //move forward
                speed = 3f;
                transform.Translate(new Vector3(moveDir * speed * Time.deltaTime, 0, 0));

                //switch direction
                if (cliffRayHit.collider == null)
                    moveDir *= -1;
                break;

            case MonsterStates.Notice:
                this.GetComponent<Renderer>().material.color = Color.yellow;

                //monster is so far from default position
                if(cliffRayHit.collider == null)
                {
                    /*moveDir *= -1;
                    isBack = true;*/
                    delayTime += Time.deltaTime;
                    if(delayTime >= 3)
                        isNotice = false;
                    break;
                }    

                //calc position with player
                if (this.transform.position.x < player.transform.position.x)
                    moveDir = 1;
                else
                    moveDir = -1;

                //chase speedly
                speed = 5f;
                transform.Translate(new Vector3(moveDir * speed * Time.deltaTime, 0, 0));
                break;

            case MonsterStates.Attack:
                this.GetComponent<Renderer>().material.color = Color.red;

                if (!isAttack)
                    StartCoroutine("attackMotion");
                break;
        }

    }

    IEnumerator attackMotion()  //공격 모션 함수 (괴물에 따라 달라질 수 있음)
    {
        isAttack = true;

        yield return Attack();

        yield return new WaitForSeconds(attackDelay);
        isAttack = false;
    }

    protected virtual IEnumerator Attack()
    {
        Vector3 _cachedScale = transform.localScale;

        transform.localScale = _cachedScale + Vector3.right * 2.0f;
        yield return new WaitForSeconds(0.2f);
        transform.localScale = _cachedScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNotice = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNotice = false;
        }
    }
}
