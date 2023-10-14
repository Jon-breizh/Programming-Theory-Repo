using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicUnit : MonoBehaviour
{
    [SerializeField] private float life;

    GameManager gameManagerScript;
     private void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //Fonction de gestion des dégâts
    public void RecievedDammage(int dammageValue, GameObject enemy)
    {
        
        life -= dammageValue;
        if (gameObject.CompareTag("Player"))
        {
            gameManagerScript.LifeUpdate(life / 60);
        }
        if (life <= 0)
        {
            if (gameObject.CompareTag("enemy"))
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().playerMoney += gameObject.GetComponent<EnemyScript>().coinValue;
            }
            else
            {
                enemy.GetComponent<EnemyScript>().inCombat = false;
                enemy.GetComponent<EnemyScript>().canMove = true;
            }
            Destroy(gameObject);
            
        }
    }
}
