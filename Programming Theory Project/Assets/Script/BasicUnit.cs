using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicUnit : MonoBehaviour
{
    [SerializeField] private float life;

    GameUI gameUI;
    LevelManager levelManager;

    // INHERITANCE - MonoBehaviour (parent class) is inherited.
    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        gameUI = GameObject.Find("CanvasGameUI").GetComponent<GameUI>();
    }

    // Method to manage damage received.
    // POLYMORPHISM - It can be overridden in child classes.
    public void ReceivedDamage(int damageValue, GameObject enemy)
    {
        life -= damageValue;

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
                LevelManager.instance.EnemyKill();
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
