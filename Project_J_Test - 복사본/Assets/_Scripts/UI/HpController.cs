using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    Image hpimage;
    void Start()
    {
        hpimage = GetComponent<Image>();
    }

    
    void Update()
    {
        hpimage.fillAmount = GameManager.GM.hpGauge;
    }
}
