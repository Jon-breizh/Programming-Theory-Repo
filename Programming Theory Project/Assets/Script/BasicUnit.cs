using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicUnit : MonoBehaviour
{
    [SerializeField] private float life;

    GameUI gameUI;
    LevelManager levelManager;
    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        gameUI = GameObject.Find("CanvasGameUI").GetComponent<GameUI>();
    }

    //Fonction de gestion des dégâts
    public void RecievedDammage(int dammageValue, GameObject enemy)
    {
        
        life -= dammageValue;
        if (gameObject.CompareTag("Player"))
        {
            gameUI.UpdateUI(life / 60);
        }
        if (life <= 0)
        {
            if (gameObject.CompareTag("enemy"))
            {
                GameManager.Instance.playerMoney += gameObject.GetComponent<EnemyScript>().coinValue;
            }
            else
            {
                enemy.GetComponent<EnemyScript>().inCombat = false;
                enemy.GetComponent<EnemyScript>().canMove = true;
            }
            if (gameObject.CompareTag("enemy"))
            {
                LevelManager.instance.enyKill();
            }
            if (gameObject.CompareTag("Player"))
            {
                LevelManager.instance.LooseGameScreen.SetActive(true);
                Time.timeScale = 0;
            }
            Destroy(gameObject);
        }
    }
}
