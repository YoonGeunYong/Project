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

	[SerializeField][Range(0f, 1f)] float _hpGauge = 1f;

	public float hpGauge
	{
		get { return _hpGauge; }
		set { _hpGauge = value; }
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }
}
