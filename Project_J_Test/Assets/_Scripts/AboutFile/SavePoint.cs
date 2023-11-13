using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

	ItemManager itemManager;

    private void Start()
    {
        itemManager = GameObject.Find("ItemBar").GetComponent<ItemManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			DataController.Instance.nowPlayerData.playerPositionTutorial = this.transform.position;
			for (int i = 0; gameObject.name != "SaveTestObject" + $"{i}"; i++)
			{
				if (gameObject.name == "SaveTestObject" + $"{i + 1}")
				{
					DataController.Instance.nowPlayerData.itemState = itemManager.chechItemState;
                    DataController.Instance.nowPlayerData.items = itemManager.chechItems;
                    DataController.Instance.SaveObjPosition();
                    break;
				}
			}
			DataController.Instance.SaveGameData();
		}
	}
}
