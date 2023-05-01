using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager GM;

	[SerializeField] bool _dustInCheck;
	[SerializeField][Range(0f, 1f)] float _hpGauge = 1f;


	public bool dustInCheck
	{
		get { return _dustInCheck; }
		set { _dustInCheck = value; }
	}

	public float hpGauge
	{
		get { return _hpGauge; }
		set
		{
			if (hpGauge < 0)
				_hpGauge = 0;

			else _hpGauge = value;
		}
	}

	private void Awake()
	{
		if (GM == null) GM = this;
		else if (GM != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
