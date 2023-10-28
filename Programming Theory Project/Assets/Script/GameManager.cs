using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // To make GameManager accessible across scenes
    public static GameManager Instance;

    // ENCAPSULATION - variable declaration    
    public int playerLvl { get; private set; }
    public int playerMoney;
    public float effectVolumeValue = 0.5f, mainVolumeValue = 0.2f;
    public GameObject unitToSpawn;  // Object to instantiate

    private void Awake()
    {
        // To make GameManager accessible across scenes
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    // Serializable class for data storage
    [Serializable]
    class PlayerData
    {
        public int playerLvl;
        public int playerMoney;
    }

    // Save game data
    public void SaveData()
    {
        PlayerData data = new PlayerData();
        data.playerLvl = playerLvl;
        data.playerMoney = playerMoney;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Load game data
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            playerLvl = data.playerLvl;
            playerMoney = data.playerMoney;
        }
        else
        {
            playerLvl = 1;
            playerMoney = 40;
        }
    }

    // Load the next level
    // ABSTRACTION : by providing a higher-level method to load levels.
    public void LoadNextLevel()
    {
        playerLvl++;
        SaveData();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1;
    }

    // Reset game data upon winning
    // POLYMORPHISM : by resetting the game data differently depending on the level.
    public void WinningGame()
    {
        playerLvl = 1;
        playerMoney = 50;
        SaveData();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
