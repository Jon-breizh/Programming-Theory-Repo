using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    // Variable pour la gestion des SpawPoints
    public GameObject[] spawnSpot;
    public LayerMask layerMask;
    float checkRadius = 1.0f;
    List<GameObject> availableSpawnPoints;

    //Gestion de la partie gagné
    public int enemyNumber, ennemyKilled;
    public GameObject WinScreen, WinGameScreen;

    // Prefab des ennemies et amis du level
    public GameObject[] frienlyPrefab;
    public GameObject[] enemyPrefab;

    private void Awake()
    {
        instance = this;
        HideSpot();

        //initialise le nobre d'ennemie à tuer en fonction du niveau
        enemyNumber = GameManager.Instance.playerLvl * 10;
        ennemyKilled = 0;
    }
    private void Start()
    {
         availableSpawnPoints = new List<GameObject>();
    }

    //Fait apparaitre les spots non occupés
    public int ShowAvailableSpot()
    {
        foreach (GameObject spawnspot in spawnSpot)
        {
            Collider[] colliders = Physics.OverlapSphere(spawnspot.transform.position, checkRadius, layerMask);

            if (colliders.Length == 0)
            {
                availableSpawnPoints.Add(spawnspot);
            }
        }
        foreach (GameObject spawnspot in availableSpawnPoints)
        {
            if (availableSpawnPoints != null)
            {
                spawnspot.GetComponent<Renderer>().enabled = true;
                spawnspot.GetComponentInChildren<Light>().enabled = true;
            }
        }
        return availableSpawnPoints.Count;
    }

    //Cahche tout les spots
    public void HideSpot()
    {
        foreach (GameObject spawnspot in spawnSpot)
        {
            spawnspot.GetComponent<Renderer>().enabled = false;
            spawnspot.GetComponentInChildren<Light>().enabled = false;
        }
    }

    //Achat d'une unitée
    public void BuyUnit(GameObject spawnSpot)
    {
        GameManager.Instance.playerMoney -= GameManager.Instance.unitToSpawn.GetComponent<DefenceUnit>().costValue;
        Vector3 spawnPosit = new Vector3(spawnSpot.transform.position.x, spawnSpot.transform.position.y + 1, spawnSpot.transform.position.z);
        Instantiate(GameManager.Instance.unitToSpawn, spawnPosit, Quaternion.identity);
        GameManager.Instance.unitToSpawn = null;
        HideSpot();
    }

    //Suit le nombre d'ennemie tué
    //Lance l'écran partie de partie gagné quand tout les ennemies ont été tués
    //Met le jeu en pause
    public void enyKill()
    {
        ennemyKilled++;
        if (ennemyKilled >= enemyNumber)
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