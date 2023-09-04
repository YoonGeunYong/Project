using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsScript : MonoBehaviour
{
    [SerializeField] int item;

	ItemManager itemManager;
	Sprite sprite;
    void Start()
    {
		itemManager = GameObject.Find("ItemBar").GetComponent<ItemManager>();
		sprite = GetComponent<SpriteRenderer>().sprite;
		for (int i = 0; i < itemManager.chechItemState.Length; i++)
		{
			if (itemManager.chechItemState[i] && itemManager.chechItems[i] == item)
			{
				itemManager.GetItem(item, sprite);
				gameObject.SetActive(false);
			}
		}
    }

	// Update is called once per frame
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			itemManager.GetItem(item, sprite);
			gameObject.SetActive(false);
		}
	}
}
