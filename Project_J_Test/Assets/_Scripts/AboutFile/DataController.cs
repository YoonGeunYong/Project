using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerData
{
    public float time = 1;
    public Vector3 playerPositionTutorial = new Vector3(0f, 0f, 0f);
    public bool[] itemState = new bool[4];
    public int[] items = new int[4];
    public bool[] checkPoint = new bool[8];
    public Vector3[] setObjPosition = new Vector3[8];
    public bool[] doorused = new bool[4];
    public bool activedCart = false;
    public bool isActiveTrap = false;
    public bool tutorialClear = false;
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
    
    private GameObject setObj;


    static GameObject Container
    {
        get { return _container; }
    }

    public static DataController Instance
    {
        get
        {
            if (_instance) return _instance;
            _container = FindObjectOfType<DataController>() is null ? new GameObject("DataController") : GameObject.Find("DataController");

            _instance = _container.AddComponent(typeof(DataController)) as DataController;
            DontDestroyOnLoad(_container);
            return _instance;
        }
    }

    public PlayerData nowPlayerData
    {
        get { return _nowPlayerData; }
        set { _nowPlayerData = value; }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadGameData();
    }

    void Start()
    {
        filePath = Application.persistentDataPath + "/SaveFile/" + fileName;
    }

    public void LoadGameData()
    {
        string fromJsonData = File.ReadAllText(filePath + nowSlot.ToString());
        if (savefile[nowSlot])
        {
            nowPlayerData = JsonUtility.FromJson<PlayerData>(fromJsonData);
        }
        else
        {
            nowPlayerData = new PlayerData();
        }
    }

    public void SaveGameData()
    {
        //filepath에 SaveFile이라는 폴더가 없으면 생성
        if (!Directory.Exists(Application.persistentDataPath + "/SaveFile"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveFile");
        }
        
        try
        {
            string toJsonData = JsonUtility.ToJson(_nowPlayerData);
            File.WriteAllText(filePath + nowSlot.ToString(), toJsonData);

        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
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

        if (nowPlayerData.tutorialClear)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(2);
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
    
    public void SaveObjPosition()
    {
        setObj = GameObject.Find("CanMoveObj");
        for (int i = 0; i < 8; i++)
        {
            if (setObj.transform.GetChild(i) is null) break;
            nowPlayerData.setObjPosition[i] = setObj.transform.GetChild(i).gameObject.transform.position;
        }
    }

    public void LoadObjPosition()
    {
        setObj = GameObject.Find("CanMoveObj");
        if(nowPlayerData.setObjPosition[0] == new Vector3(0f, 0f, 0f)) return;
        for (int i = 0; i < 8; i++)
        {
            if (setObj.transform.GetChild(i) is null) break;
            setObj.transform.GetChild(i).gameObject.transform.position = nowPlayerData.setObjPosition[i];
        }
    }
}