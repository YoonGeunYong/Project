using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerData
{
    // 시간, 위치, 아이템
    public int time = 1;
    public float playerHP = 1f;
    public Vector3 playerPositionTutorial = new Vector3(0f, 0f, 0f);
    public bool[] itemState = new bool[4];
    public int[] items = new int[4];
    public bool[] checkPoint = new bool[5];
    public GameObject[] checkPointObj = new GameObject[15];
}

public class DataController : MonoBehaviour
{
    static GameObject _container;
    static DataController _instance;

    public PlayerData _nowPlayerData;

    public string fileName = "ProjectJData";
    public string filePath;
    public int nowSlot;
    public bool[] savefile = new bool[3];


    static GameObject Container
    {
        get { return _container; }
    }

    public static DataController Instance
    {
        get
        {
            if (_instance) return _instance;
            _container = FindObjectOfType<DataController>() == null ? new GameObject("DataController") : GameObject.Find("DataController");

            _instance = _container.AddComponent(typeof(DataController)) as DataController;
            DontDestroyOnLoad(_container);
            return _instance;
        }
    }

    public PlayerData nowPlayerData
    {
        get
        {
            //if (nowPlayerData == null){
            //	nowPlayerData = LoadGameData();
            //	SaveGameData();
            //}
            return _nowPlayerData;
        }
        set { _nowPlayerData = value; }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadGameData();
    }

    void Start()
    {
        filePath = Application.dataPath + "/SaveFile/" + fileName;
    }

    public void LoadGameData()
    {
        //if (File.Exists(filePath))
        //{
        //Debug.Log("Load Success");
        string FromJsonData = File.ReadAllText(filePath + nowSlot.ToString());
        if (savefile[nowSlot])
        {
            nowPlayerData = JsonUtility.FromJson<PlayerData>(FromJsonData);
        }
        else
        {
            nowPlayerData = new PlayerData();
        }

        //}
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(_nowPlayerData);
        File.WriteAllText(filePath + nowSlot.ToString(), ToJsonData);

        //Debug.Log("Save Succes");
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayerData = new PlayerData();
    }

    public void GoGame()
    {
        if (savefile[nowSlot])
        {
            SaveGameData();
        }
        SceneManager.LoadScene(1);
    }

    public void UseItem(int num)
    {
        var itemBar = GameObject.Find("ItemBar");
        nowPlayerData.items[num] = 0;
        nowPlayerData.itemState[num] = false;
        itemBar.GetComponent<ItemManager>().chechItems[num] = 0;
        itemBar.GetComponent<ItemManager>().chechItemState[num] = false;
        itemBar.transform.GetChild(GameManager.GM.itemInt).gameObject.SetActive(false);
    }
}