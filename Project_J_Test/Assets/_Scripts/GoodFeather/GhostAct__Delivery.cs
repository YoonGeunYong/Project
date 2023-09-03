using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAct__Delivery : MonoBehaviour
{
    public GameObject[] objectsPosition;
    public GameObject[] afterPosition;

    public bool pressF = false;

    public float speed = 10;

    void Start()
    {
        
    }

    void Update()
    {
        if (!pressF && Input.GetKeyDown(KeyCode.F))
        {
            //movetoward·Î º¯°æ
            //transform.position = Vector3.MoveTowards(transform.position, objectsPosition[0].transform.position, Time.deltaTime * speed);
            //transform.Translate(objectsPosition[0].transform.position - transform.position);
            pressF = true;
        }
		if (pressF)
		{
			transform.position = Vector3.MoveTowards(transform.position,
				objectsPosition[0].transform.position, Time.deltaTime * speed);
			if (transform.position == objectsPosition[0].transform.position)
				pressF = false;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
        if (other.tag == "Block")
		{
            if(transform.position == objectsPosition[0].transform.position)
			{
                other.transform.SetParent(transform);
                other.GetComponent<Rigidbody2D>().isKinematic = true;
                //transform.Translate(afterPosition[0].transform.position - transform.position);
                transform.position = Vector3.MoveTowards(transform.position,
                    afterPosition[0].transform.position, Time.deltaTime * speed);
            }

            if (transform.position == afterPosition[0].transform.position)
            {
                other.transform.SetParent(null);
                other.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}
