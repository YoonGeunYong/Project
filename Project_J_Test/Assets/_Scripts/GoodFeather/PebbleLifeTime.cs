using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleLifeTime : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
