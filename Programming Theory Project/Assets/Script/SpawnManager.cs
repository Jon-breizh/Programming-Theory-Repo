using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float SpawnRate;
    public int numberOfLanes = 2;
    // INHERITANCE - Demonstrating inheritance
    //[SerializeField] GameObject[] enemiToSpawn;
    private int numberOfEnemyTypes;

    private void Start()
    {
        numberOfEnemyTypes = GameManager.Instance.playerLvl;
        // Start the coroutine for enemy spawning
        StartCoroutine(SpawnXTimes(LevelManager.instance.enemyNumber));
    }

    // Spawn enemies
    // Demonstrates POLYMORPHISM - Different enemy types can be spawned.
    void SpawnEnemy()
    {
        int enemyRange = Random.Range(0, numberOfEnemyTypes);
        Instantiate(LevelManager.instance.enemyPrefab[enemyRange], SpawnPosition(), Quaternion.Euler(new Vector3(0, 180, 0)));
    }

    // Return a random spawn position
    private Vector3 SpawnPosition()
    {
        if (numberOfLanes == 1)
        {
            return new Vector3(0, 1, 0);
        }
        else
        {
            int xPos = Random.Range(-numberOfLanes + 1, numberOfLanes);
            return new Vector3(xPos * 5, 0, 10);
        }
    }

    // Manage the spawn time rate of enemies
    IEnumerator SpawnXTimes(int spawnNumber)
    {
        while (spawnNumber > 0)
        {
            SpawnEnemy();
            spawnNumber--;
            yield return new WaitForSeconds(SpawnRate);
        }
    }

    // Destroy ammunition when it exits the scene
    // Demonstrates ENCAPSULATION as the trigger exit behavior is encapsulated.
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
