using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeImageController : MonoBehaviour
{
    public GameObject fadeImage;
    public Image image;
    void Start()
    {
        image = fadeImage.GetComponent<Image>();
        image.color = new Color(0, 0, 0, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GM.dieing && image.color.a < 0.8f)
        {
            image.color += new Color(0, 0, 0, Time.deltaTime);
        }
        else if (image.color.a > 0.8f)
        {
            GameManager.GM.dieing = false;
            SceneManager.LoadScene(1);
        }

        if (!GameManager.GM.dieing && image.color.a >= 0)
            image.color -= new Color(0, 0, 0, Time.deltaTime);
        
    }
}
