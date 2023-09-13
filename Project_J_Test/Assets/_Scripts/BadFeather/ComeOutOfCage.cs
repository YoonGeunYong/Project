using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeOutOfCage : MonoBehaviour
{
    public GameObject monsterPrefab;

    int destroyTime = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(monsterPrefab, transform.position, monsterPrefab.transform.rotation);
            //Destroy(this.gameObject);
            StartCoroutine("DestroyFunc");
        }
    }

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
