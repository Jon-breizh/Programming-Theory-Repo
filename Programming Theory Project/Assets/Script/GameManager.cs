using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerXP;
    public int playerLvl = 1;
    public int playerMoney = 0;
    public GameObject[] enemyPrefab;
    public GameObject[] frienlyPrefab;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);   
        
        LoadData();
        playerLvl = 2;
        playerMoney = 0;
    }

    [Serializable]
    class PlayerData
    {
        public int playerLvl;
        public int playerMoney;
    }

    public void SaveData()
    {
        PlayerData data = new PlayerData();
        data.playerLvl = playerLvl;
        data.playerMoney = playerMoney;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            String json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            playerLvl = data.playerLvl;
            playerMoney = data.playerMoney;
        }
    }
}
