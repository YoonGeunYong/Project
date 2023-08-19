using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SelectSlot : MonoBehaviour
{
    public GameObject creat;
    public Text[] slotText;
    public Text newPlayerName;
    
    void Start()
    {
        //슬롯별로 저장된 데이터가 존재하는지 판단
        for(int i = 0; i < 3; i++)
		{
            if(File.Exists(DataController.Instance.filePath + $"{i}"))
			{
                DataController.Instance.savefile[i] = true;
                DataController.Instance.nowSlot = i;
                DataController.Instance.LoadGameData();
                slotText[i].text = (DataController.Instance.nowPlayerData.time + i).ToString();
            }
            else
			{
                slotText[i].text = "비어있음";
			}
		}
        DataController.Instance.DataClear();
    }

    
    void Update()
    {
        
    }

    public void Slot(int num)
	{
        DataController.Instance.nowSlot = num;

        // 1. 저장된 데이터가 있을 때
        if(DataController.Instance.savefile[num])
		{
            DataController.Instance.LoadGameData();
        }
        DataController.Instance.GoGame();
    }

    public void CheckNum(int num)
	{
        DataController.Instance.nowSlot = num;
    }

    public void Creat()
	{
        creat.gameObject.SetActive(true);
	}


    public void ClickDeleteButton()
    {
        File.Delete(DataController.Instance.filePath + DataController.Instance.nowSlot.ToString());
    }

	public void Close()
	{
		//슬롯별로 저장된 데이터가 존재하는지 판단
		for (int i = 0; i < 3; i++)
		{
			if (!File.Exists(DataController.Instance.filePath + $"{i}"))
			{
				DataController.Instance.savefile[i] = false;
				DataController.Instance.nowSlot = i;
				slotText[i].text = "비어있음";
			}
		}
		DataController.Instance.DataClear();
	}
}
