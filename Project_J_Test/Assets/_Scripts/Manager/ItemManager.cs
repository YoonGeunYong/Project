using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public int?[] chechItem;

    private void Awake()
    {
        chechItem = DataController.Instance._nowPlayerData.item;
		for (int i = 0; i < chechItem.Length; i++)
		{
            chechItem[i] = DataController.Instance._nowPlayerData.item[i];
		}
    }

    void Start()
    {
		//for (int i = 0; i < DataController.Instance.nowPlayerData.item.Length; i++)
		//{
		//	if (DataController.Instance.nowPlayerData.item[i] != null)
		//		transform.GetChild(i).gameObject.SetActive(true);
		//}

        
	}


	void Update()
    {
        
		//if (chechItem)
		//{
		//	for (int i = 0; i < DataController.Instance.nowPlayerData.item.Length; i++)
		//	{
  //              if (DataController.Instance.nowPlayerData.item[i] == null)
  //              {
  //                  transform.GetChild(i).gameObject.SetActive(true);
  //                  DataController.Instance.nowPlayerData.item[i] = transform.GetChild(i).gameObject;
  //                  chechItem = false;
  //                  break;
  //              }
  //              else if (DataController.Instance.nowPlayerData.item[3] != null)
  //                  chechItem = false;
		//	}
		//}
    }
    
    public void GetItem(int number, Sprite image)
	{
        for (int i = 0; i < DataController.Instance.nowPlayerData.item.Length; i++)
        {
            if (chechItem[i] == null)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).GetComponent<Image>().sprite = image;
                chechItem[i] = number;
                DataController.Instance.nowPlayerData.item[i] = number;
                break;
            }
            //else if (DataController.Instance.nowPlayerData.item[3] != null)
            //    chechItem = false;
        }
    }
}
