using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.IO;
using System;
using Unity.VisualScripting;

[Serializable]
public class PlayerData
{
    // 시간, 위치, 아이템
    public string time;
    public Vector3 playerPosition1;
    public int[] item = new int[3];
}

public class DataController : MonoBehaviour
{
	static GameObject _container;
	static GameObject Container{
		get{
			return _container;
		}
	}

	public static DataController _instance;

	public static DataController Instance{
		get{
			if (!_instance){
				_container = new GameObject();
				_container.name = "DataController";
				_instance = _container.AddComponent(typeof(DataController)) as DataController;
				DontDestroyOnLoad(_container);
			}
			return _instance;
		}
	}
	public string GameDataFileName = "ProjectJData.json";
	public PlayerData _playerData;

	public PlayerData PlayerData{
		get{
			if (_playerData == null){
				_playerData = LoadGameData();
				SaveGameData();
			}
			return _playerData;
		}
	}

	void Awake()
	{
        DontDestroyOnLoad(this.gameObject);
	}

	void Start()
    {
		_playerData = LoadGameData();
		SaveGameData();
    }   

    public PlayerData LoadGameData(){
		string filePath = Application.dataPath + "/SaveFile/" + GameDataFileName;
		if (File.Exists(filePath)){
			Debug.Log("Load Succes");
			string FromJsonData = File.ReadAllText(filePath);
			return JsonUtility.FromJson<PlayerData>(FromJsonData);
		}
		else{
			Debug.Log("Create New File");
			return new PlayerData();
		}
	}
	public void SaveGameData(){
		string ToJsonData = JsonUtility.ToJson(_playerData);
		string filePath = Application.dataPath + "/SaveFile/" + GameDataFileName;

		File.WriteAllText(filePath, ToJsonData);

		Debug.Log("Save Succes");
	}

	private void OnApplicationQuit(){
		SaveGameData();
	}
}
