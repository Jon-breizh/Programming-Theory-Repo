using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //permet de rendre accessible GameManager dans les autres scenes
    public static GameManager Instance;

    public int playerXP;
    public int playerLvl;
    public int playerMoney;

    //Object à instantier
    public GameObject unitToSpawn;
    private void Awake()
    {
        //permet de rendre accessible GameManager dans les autres scenes
        Debug.Log(Instance);
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Detruit le nouveau");
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
        Debug.Log("lvl : " + playerLvl + "money : " + playerMoney);
    }

    private void Start()
    {
        //recharge les données de la partie précédente
        //LoadData();
    }

    //Classe pour la sauvegarde de donné
    [Serializable]
    class PlayerData
    {
        public int playerLvl;
        public int playerMoney;
    }

    //fonction de sauvegarde des données
    public void SaveData()
    {
        PlayerData data = new PlayerData();
        data.playerLvl = playerLvl;
        data.playerMoney = playerMoney;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    //Fonction de chargement des données
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
        else
        {
            playerLvl = 1;
            playerMoney = 40;
        }
    }
    //Charge le niveau suivant
    public void loadNextLevel()
    {
        playerLvl++;
        SaveData();
        int currentSceneName = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;

    }
    public void WinningGame()
    {
        playerLvl = 1;
        playerMoney = 50;
        SaveData();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
