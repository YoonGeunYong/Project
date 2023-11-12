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
        //���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�
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
                slotText[i].text = "�������";
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

        // 1. ����� �����Ͱ� ���� ��
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
        //���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�
        for (int i = 0; i < 3; i++)
        {
            if (!File.Exists(DataController.Instance.filePath + $"{i}"))
            {
                DataController.Instance.savefile[i] = false;
                DataController.Instance.nowSlot = i;
                slotText[i].text = "�������";
            }
        }
        DataController.Instance.DataClear();
    }
}
