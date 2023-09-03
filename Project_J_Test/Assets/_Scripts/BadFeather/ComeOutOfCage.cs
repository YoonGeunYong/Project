using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeOutOfCage : MonoBehaviour
{
    public GameObject monsterPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 12, transform.position.z);
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            Instantiate(monsterPrefab, transform.position, monsterPrefab.transform.rotation);
        }
    }
}
