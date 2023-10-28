using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicUnit : MonoBehaviour
{
    // ENCAPSULATION - variable declaration
    [SerializeField] private float life;
    GameUI gameUI;
    
    private void Start()
    {
        gameUI = GameObject.Find("CanvasGameUI").GetComponent<GameUI>();
    }

    // Method to manage damage received.
    public void ReceivedDamage(int damageValue, GameObject enemy)
    {
        life -= damageValue;

        if (gameObject.CompareTag("Player"))
        {
            gameUI.UpdateUI(life / 100);
        }

        if (life <= 0)
        {
            if (gameObject.CompareTag("enemy"))
            {
                GameManager.Instance.playerMoney += gameObject.GetComponent<EnemyScript>().coinValue;
                LevelManager.instance.EnemyKill();
            }
            else
            {
                enemy.GetComponent<EnemyScript>().inCombat = false;
                enemy.GetComponent<EnemyScript>().canMove = true;
            }
            if (gameObject.CompareTag("Player"))
            {
                LevelManager.instance.LoseGameScreen.SetActive(true);
                Time.timeScale = 0;
            }
            Destroy(gameObject);
        }
    }
}
