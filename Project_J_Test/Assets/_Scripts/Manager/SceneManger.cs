using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour
{

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

    void Update()
    {

    }

	public void CreateNewGame()
	{
		SceneManager.LoadSceneAsync("Chapter_1");
		//TODO : Create new Game and GO new Scene
	}
}
