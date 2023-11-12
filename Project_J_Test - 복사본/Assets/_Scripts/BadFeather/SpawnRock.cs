using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRock : MonoBehaviour
{
    public GameObject rock;
    public float spawnTime;

    bool isSpawn; //온오프 스위치(조건부 온오프 용)
    float time = 0f;

    void Start()
    {
        isSpawn = true;
    }

    void Update()
    {
        if (isSpawn && time >= spawnTime)
        {
            Instantiate(rock, transform.position, transform.rotation);
            time = 0f;
        }

        time += Time.deltaTime;
    }
}
