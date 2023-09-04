using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

    public bool[] chechItemState = new bool[4];
    public int[] chechItems = new int[4];

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
        for (int i = 0; i < chechItemState.Length; i++)
        {
            if (!chechItemState[i] || chechItems[i] == number)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).GetComponent<Image>().sprite = image;
                chechItemState[i] = true;
                chechItems[i] = number;
                break;
            }
            
            //else if (DataController.Instance.nowPlayerData.item[3] != null)
            //    chechItem = false;
        }
    }
}
