using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsScript : MonoBehaviour
{
    [SerializeField] int item;

	ItemManager ItemManager;
	Sprite sprite;
    void Start()
    {
		ItemManager = GameObject.Find("ItemBar").GetComponent<ItemManager>();
		sprite = GetComponent<SpriteRenderer>().sprite;
		for (int i = 0; i < ItemManager.chechItem.Length; i++)
		{
			if (ItemManager.chechItem[i] == item)
				gameObject.SetActive(false);
		}
    }

	// Update is called once per frame
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			ItemManager.GetItem(item, sprite);
			gameObject.SetActive(false);
		}
	}
}
