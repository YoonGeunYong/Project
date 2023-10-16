using System.Collections;
using System.Collections.Generic;
using BaekRyang;
using UnityEngine;

public class RollingRock : MonoBehaviour
{
    public float rollForce;
    public float rollSpeed;
    public float lifeTime;
    public float destroyTime;

    Rigidbody2D rb;
    float time = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.left * rollForce, ForceMode2D.Impulse);
    }

    private void Update()
    {
        transform.Rotate(Vector3.back * (rollSpeed * Time.deltaTime));

        if (time >= lifeTime)
            StartCoroutine(SharedFunction.DestroyFunc(gameObject, destroyTime));


        time += Time.deltaTime;
    }

    //SharedFunction.cs 로 대체됨

    IEnumerator destroy()
    {
        float time = 0f;
        Color color = this.GetComponent<SpriteRenderer>().color;
        while (true)
        {
            color.a -= (Time.deltaTime / destroyTime);
            this.GetComponent<SpriteRenderer>().color = color;
            time += Time.deltaTime;

            if (time >= destroyTime)
                Destroy(this.gameObject);
            yield return null;
        }
    }
}
