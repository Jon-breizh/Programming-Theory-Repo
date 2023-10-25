using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float SpawnRate;
    public int numberOfLigne = 1;
    //[SerializeField] GameObject[] enemiToSpawn;
    private int numberOfEnemyType;
    private void Start()
    {
        numberOfEnemyType = GameManager.Instance.playerLvl;
        //lance la coroutine d'apparistion des enemies
        StartCoroutine(SpawnXTime(LevelManager.instance.enemyNumber));
    }
    //FAit apparaitre les enemy
    void SpawnEnemy()
    {
        int enemyRange = Random.Range(0, numberOfEnemyType);
        Instantiate(LevelManager.instance.enemyPrefab[enemyRange], SpawnPosition(), Quaternion.Euler(new Vector3(0, 180, 0)));
    }
    //Retourne la position ou faire apparaitre les enemy de facon aléatoire
    private Vector3 SpawnPosition()
    {
        if (numberOfLigne == 1) 
        {
            return new Vector3(0, 1, 0);
        }
        else
        {
            int xPos = Random.Range(-numberOfLigne+1, numberOfLigne);
            return new Vector3(xPos*5, 0, 10);
        }
    }

    //Gére le timeRate d'apparition des enemy
    IEnumerator SpawnXTime(int spawnNumber)
    {
        while (spawnNumber > 0)
        {
            SpawnEnemy();
            spawnNumber--;
            yield return new WaitForSeconds(SpawnRate);
        }
    }
    // Détruit les munitions quand elles sortent de la scene
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
