using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
	public GameObject image;
	public PlayerMovement2 player;

	void Start()
	{
	}

	void Update()
    {
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
		    if (!GameManager.GM.isRunning)
		    {
			    image.SetActive(false);
			    GameManager.GM.isRunning = true;
			    player.anim.speed = 1f;
			    return;
		    }
		    image.SetActive(true);
		    GameManager.GM.isRunning = false;
		    
	    }
    }

	public void GoTitleScene()
	{
		SceneManager.LoadScene(0);
	}

	public void CreateNewGame()
	{
		SceneManager.LoadSceneAsync("Create");
		//TODO : Create new Game and GO new Scene
	}
}
