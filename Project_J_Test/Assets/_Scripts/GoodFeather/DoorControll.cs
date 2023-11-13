using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorControll : MonoBehaviour
{
    public bool checkItem;
    public bool itemMoved;
    public int num;
    float positionY;
    public GameObject item;
    
    // Start is called before the first frame update
    void Start()
    {
        if (DataController.Instance.nowPlayerData.doorused[num])
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 20, transform.position.z);
            item.SetActive(false);
        }

        positionY = transform.position.y + 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkItem && transform.position.y < positionY)
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, positionY, transform.position.z), 0.01f);
            if (!itemMoved && num == 2 && !DataController.Instance.nowPlayerData.doorused[1])
            {
                item.transform.position = new Vector3(item.transform.position.x - 10, item.transform.position.y, item.transform.position.z);
                itemMoved = true;
            }
            DataController.Instance.nowPlayerData.doorused[num] = true;
        }
    }
}
