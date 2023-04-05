using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    Vector3 cameraPos;
    void Start()
    {
        target = GameObject.Find("Player");
    }

	private void Update()
	{
        cameraPos = new Vector3(target.transform.position.x, 0, -10);
	}

	void LateUpdate()
    {
        transform.position = cameraPos;
    }
}
