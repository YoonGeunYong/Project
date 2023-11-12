using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustFadeOut : MonoBehaviour
{
    SpriteRenderer dcsprite;
    [SerializeField] float colorAlpha = 2f; //0.8f

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.gameObject.layer = LayerMask.NameToLayer("HidePlayer");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
