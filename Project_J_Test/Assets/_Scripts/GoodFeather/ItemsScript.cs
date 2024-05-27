using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemsScript : MonoBehaviour
{
    [SerializeField] int item;
    public bool removeItem = true;
    
	public ItemManager itemManager;
    public GameObject keyE;
    private Sprite sprite;
	
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            keyE.SetActive(true);
    }
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			keyE.SetActive(true);
			if (Input.GetKey(KeyCode.E))
			{
				itemManager.GetItem(item, sprite);
				if (removeItem)
					gameObject.SetActive(false);
				else
				{
					GameManager.GM.checkitem2 = true;
					other.gameObject.GetComponent<PlayerMovement2>().haveStone = true;
				}
                keyE.SetActive(false);
            }
		}
	}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            keyE.SetActive(false);
    }
}
