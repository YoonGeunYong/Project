using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager PM;

	private void Awake()
	{
		if (PM == null) PM = this;
		else if (PM != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }
}
