using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager GM;
	PlayerData playerData;

	[SerializeField] bool _dustInCheck;
	[SerializeField][Range(0f, 1f)] float _hpGauge = 1f;
	[SerializeField] int _itemNum = 0;
	[SerializeField] int _itemInt = 0;
	[SerializeField] bool _pebbleStone = false;
	[SerializeField] bool _dieing = false;
	[SerializeField] bool _isRunning = true;
	[SerializeField] bool _checkitem2 = false;

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
	
	public int itemNum
	{
		get { return _itemNum; }
		set { _itemNum = value; }
	}
	
	public int itemInt
	{
		get { return _itemInt; }
		set { _itemInt = value; }
	}
	
	public bool pebbleStone
	{
		get { return _pebbleStone; }
		set { _pebbleStone = value; }
	}
	
	public bool dieing
	{
		get { return _dieing; }
		set { _dieing = value; }
	}
	
	public bool isRunning
	{
		get { return _isRunning; }
		set { _isRunning = value; }
	}
	
	public bool checkitem2
	{
		get { return _checkitem2; }
		set { _checkitem2 = value; }
	}
	
	private void Awake()
	{
		if (GM == null) GM = this;
		else if (GM != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start()
    {
	    playerData = DataController.Instance.nowPlayerData;
    }

    
    void Update()
    {
	    if (SceneManager.GetActiveScene().name == "TitleScene") return;
	    DataController.Instance.nowPlayerData.time += Time.deltaTime;
    }
}
