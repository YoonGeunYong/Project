using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

    public bool[] chechItemState = new bool[4];
    public int[] chechItems = new int[4];
    public Image[] items = new Image[4];

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            chechItemState[i] = DataController.Instance._nowPlayerData.itemState[i];
            chechItems[i] = DataController.Instance._nowPlayerData.items[i];
        }
    }

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            items[i] = transform.GetChild(i).gameObject.GetComponent<Image>();
        }
    }


	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.GM.itemNum = chechItems[0];
            GameManager.GM.itemInt = 0;
            items[0].color = new Color(1f, 1f, 1f, 1f);
            items[1].color = new Color(1f, 1f, 1f, 0.5f);
            items[2].color = new Color(1f, 1f, 1f, 0.5f);
            items[3].color = new Color(1f, 1f, 1f, 0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.GM.itemNum = chechItems[1];
            GameManager.GM.itemInt = 1;
            items[0].color = new Color(1f, 1f, 1f, 0.5f);
            items[1].color = new Color(1f, 1f, 1f, 1f);
            items[2].color = new Color(1f, 1f, 1f, 0.5f);
            items[3].color = new Color(1f, 1f, 1f, 0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.GM.itemNum = chechItems[2];
            GameManager.GM.itemInt = 2;
            items[0].color = new Color(1f, 1f, 1f, 0.5f);
            items[1].color = new Color(1f, 1f, 1f, 0.5f);
            items[2].color = new Color(1f, 1f, 1f, 1f);
            items[3].color = new Color(1f, 1f, 1f, 0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameManager.GM.itemNum = chechItems[3];
            GameManager.GM.itemInt = 3;
            items[0].color = new Color(1f, 1f, 1f, 0.5f);
            items[1].color = new Color(1f, 1f, 1f, 0.5f);
            items[2].color = new Color(1f, 1f, 1f, 0.5f);
            items[3].color = new Color(1f, 1f, 1f, 1f);
        }
    }
    
    public void GetItem(int number, Sprite image)
	{
        for (int i = 0; i < chechItemState.Length; i++)
        {
            if (!chechItemState[i] || chechItems[i] == number)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).GetComponent<Image>().sprite = image;
                chechItemState[i] = true;
                chechItems[i] = number;
                if (GameManager.GM.itemInt == 1)
                {
                    GameManager.GM.itemNum = number;
                }
                
                break;
            }
            
            //else if (DataController.Instance.nowPlayerData.item[3] != null)
            //    chechItem = false;
        }
    }
}
