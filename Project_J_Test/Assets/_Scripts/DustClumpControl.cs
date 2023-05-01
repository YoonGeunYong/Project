using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustClumpControl : MonoBehaviour
{
    SpriteRenderer dcsprite;
    float colorAlpha = 0.8f;

    void Start()
    {
        dcsprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (colorAlpha >= 0)
        {
            dcsprite.color = new Color(1, 1, 1, colorAlpha);
            colorAlpha -= Time.deltaTime / 14;
        }
        else Destroy(gameObject);
	}
}
