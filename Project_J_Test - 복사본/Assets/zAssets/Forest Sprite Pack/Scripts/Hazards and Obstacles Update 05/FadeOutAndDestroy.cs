using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAndDestroy : MonoBehaviour
{
    public float fadeOutTimer;
    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Color fade = _renderer.color;
        fade.a = Mathf.MoveTowards(fade.a, 0, 1/fadeOutTimer * Time.deltaTime);
        _renderer.color = fade;


        if (fade.a <= 0)
            gameObject.SetActive(false);
    }
}
