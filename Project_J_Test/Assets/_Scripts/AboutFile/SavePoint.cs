using System;
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

    private void Update()
    {
	    if (transform.GetChild(2).transform.localScale.Equals(Vector3.zero))
		    transform.GetChild(2).gameObject.SetActive(false);
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
			transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
			transform.GetChild(2).gameObject.SetActive(true);
			transform.GetChild(2).GetComponent<Animation>().Play();
			
			DataController.Instance.SaveGameData();
		}
	}
}
