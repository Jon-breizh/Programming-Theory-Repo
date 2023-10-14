using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int playerXP;
    int playerLvl = 1;
    public int playerMoney = 0;
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] GameObject[] frienlyPrefab;

    GameUI gameUI;
    // Start is called before the first frame update
    void Start()
    {
        gameUI = GameObject.Find("Canvas").GetComponent<GameUI>();
        gameUI.UpdateUI(playerMoney, playerLvl);
    }
    private void Update()
    {
        gameUI.UpdateUI(playerMoney, playerLvl);
    }
    public void LifeUpdate(float life)
    {
        gameUI.UpdateUI(life);
    }
}
