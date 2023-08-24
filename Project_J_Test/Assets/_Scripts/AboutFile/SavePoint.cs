using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
	public bool healCheck;

	void Awake()
	{
		for (int i = 0; this.gameObject.name != "SaveTestObject" + $"{i}"; i++)
		{
			healCheck = DataController.Instance.nowPlayerData.checkPoint[i];
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			DataController.Instance.nowPlayerData.playerPositionTutorial = this.transform.position;
			for (int i = 0; gameObject.name != "SaveTestObject" + $"{i}"; i++)
			{
				if (!healCheck && gameObject.name == "SaveTestObject" + $"{i + 1}")
				{
					Debug.Log("ffff");
					GameManager.GM.hpGauge += 0.34f;
					healCheck = true;
					DataController.Instance.nowPlayerData.checkPoint[i] = healCheck;
					break;
				}
			}
		}
	}
}
