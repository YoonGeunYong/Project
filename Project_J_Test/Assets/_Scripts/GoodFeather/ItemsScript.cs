using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsScript : MonoBehaviour
{
    [SerializeField] int item;
    public bool removeItem = true;
    
	public ItemManager itemManager;
	Sprite sprite;
	
    void Start()
    {
		sprite = GetComponent<SpriteRenderer>().sprite;
		for (int i = 0; i < itemManager.chechItemState.Length; i++)
		{
			if ((itemManager.chechItemState[i] && itemManager.chechItems[i] == item))
			{
				itemManager.GetItem(item, sprite);
				gameObject.SetActive(false);
			}
		}
    }

	// Update is called once per frame
	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.CompareTag("Player") && Input.GetKey(KeyCode.E))
		{
			itemManager.GetItem(item, sprite);
			if(removeItem)
				gameObject.SetActive(false);
			else
			{
				GameManager.GM.checkitem2 = true;
				other.gameObject.GetComponent<PlayerMovement2>().haveStone = true;
			}
		}
	}
}
