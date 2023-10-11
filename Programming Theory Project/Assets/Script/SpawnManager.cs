using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float SpawnRate;
    public int numberOfLigne = 1;
    [SerializeField] GameObject[] enemiToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2, SpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemy()
    {
        int enemyRange = Random.Range(0, enemiToSpawn.Length);
        Instantiate(enemiToSpawn[enemyRange], SpawnPosition(), Quaternion.identity);
    }
    private Vector3 SpawnPosition()
    {
        if (numberOfLigne == 1) 
        {
            return new Vector3(0, 1, 0);
        }
        else
        {
            int xPos = Random.Range(-numberOfLigne+1, numberOfLigne);
            return new Vector3(xPos*5, 1, 0);
        }
    }
}
