using System.Collections;
using System.Collections.Generic;
using BaekRyang;
using UnityEngine;

public class ComeOutOfCage : MonoBehaviour
{
    public GameObject monsterPrefab;

    int destroyTime = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(monsterPrefab, spawnPosition, monsterPrefab.transform.rotation);
            //Destroy(this.gameObject);
            
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<BoxCollider2D>());
            StartCoroutine(SharedFunction.DestroyFunc(gameObject, destroyTime));
        }
    }

    //SharedFunction.cs 로 대체됨
    IEnumerator DestroyFunc()
    {
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());

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
