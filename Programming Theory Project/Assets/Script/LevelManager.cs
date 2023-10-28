using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelManager : MonoBehaviour
{
    // ENCAPSULATION - variable declaration
    public static LevelManager instance;  // Singleton instance

    // Variables for SpawnPoint management
    [SerializeField] private GameObject[] spawnSpot;  // Array of spawn points

    // Handling win conditions
    public int enemyNumber { get; private set; } // ENCAPSULATION
    private int enemyKilled;
    public GameObject WinScreen, WinGameScreen, LoseGameScreen;  // UI screens for game outcomes

    // Prefabs for enemies and friendly units in the level
    public GameObject[] friendlyPrefab;
    public GameObject[] enemyPrefab;

    //Sound Management
    private AudioSource audioLevel;

    private void Awake()
    {
        instance = this;  // Initialize the Singleton instance
        HideSpot();  // Hide all spawn spots

        // Initialize the number of enemies to kill based on the level
        enemyNumber = GameManager.Instance.playerLvl * 10;
        enemyKilled = 0;
        audioLevel = gameObject.GetComponent<AudioSource>();
        audioLevel.volume = GameManager.Instance.mainVolumeValue;
    }

    // Show available SpawnPoints and return their count
    // Demonstrates ABSTRACTION as it provides a higher-level method to interact with SpawnPoints.
    public int ShowAvailableSpot()
    {
        int freeSpawnPointCount = 0;
        foreach (GameObject go in spawnSpot)
        {
            if (go.GetComponent<SpawnPoint>().IsFree)
            {
                freeSpawnPointCount++;
                go.GetComponent<Renderer>().enabled = true;
                go.GetComponent<Collider>().enabled = true;
            }
        }
        return freeSpawnPointCount;
    }

    // Hide all SpawnPoints
    // Demonstrates ABSTRACTION by abstracting the task of hiding SpawnPoints.
    public void HideSpot()
    {
        foreach (GameObject spawnSpot in spawnSpot)
        {
            spawnSpot.GetComponent<Renderer>().enabled = false;
            spawnSpot.GetComponent<Collider>().enabled = false;
        }
    }

    // Buy a Defense Unit
    // Demonstrates ENCAPSULATION by interacting with DefenseUnit's costValue.
    public void BuyUnit(GameObject spawnSpot)
    {
        GameManager.Instance.playerMoney -= GameManager.Instance.unitToSpawn.GetComponent<DefenseUnit>().costValue;
        Vector3 spawnPosition = new Vector3(spawnSpot.transform.position.x, spawnSpot.transform.position.y, spawnSpot.transform.position.z);
        Instantiate(GameManager.Instance.unitToSpawn, spawnPosition, Quaternion.identity);
        GameManager.Instance.unitToSpawn = null;
        spawnSpot.GetComponent<SpawnPoint>().IsFree = false;
        HideSpot();
    }

    // Track the number of enemy kills
    // Launch the appropriate game outcome screen when all enemies are killed
    // Pause the game
    // Demonstrates POLYMORPHISM by overriding the same method for different outcomes.
    public void EnemyKill()
    {
        enemyKilled++;
        if (enemyKilled >= enemyNumber)
        {
            if (GameManager.Instance.playerLvl == 3)
            {
                WinGameScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                WinScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
