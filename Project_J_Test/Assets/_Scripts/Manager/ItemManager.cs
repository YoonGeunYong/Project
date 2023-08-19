using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager IM;
    public bool chechItem;

    private void Awake()
    {
        if (IM == null) IM = this;
        else if (IM != this) Destroy(gameObject);
    }

    void Start()
    {
		for (int i = 0; i < DataController.Instance.nowPlayerData.item.Length; i++)
		{
			if (DataController.Instance.nowPlayerData.item[i] != null)
				transform.GetChild(i).gameObject.SetActive(true);
		}
	}


	void Update()
    {
        
		if (chechItem)
		{
			for (int i = 0; i < DataController.Instance.nowPlayerData.item.Length; i++)
			{
                if (DataController.Instance.nowPlayerData.item[i] == null)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    DataController.Instance.nowPlayerData.item[i] = transform.GetChild(i).gameObject;
                    chechItem = false;
                    break;
                }
                else if (DataController.Instance.nowPlayerData.item[3] != null)
                    chechItem = false;
			}
		}
    }
}
