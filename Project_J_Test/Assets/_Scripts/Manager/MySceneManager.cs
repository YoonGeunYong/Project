using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
	PlayerData playerData;
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		playerData = DataController.Instance.nowPlayerData;
	}

	void Update()
    {

    }

	public void CreateNewGame()
	{
		SceneManager.LoadSceneAsync("Create");
		//TODO : Create new Game and GO new Scene
	}
}
