using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI levelTxt, coin;
    private Slider lifeSlider;

    void Start()
    {
        levelTxt = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        coin = GameObject.Find("coinTxt").GetComponent<TextMeshProUGUI>();
        lifeSlider = GameObject.Find("LifeSlider").GetComponent <Slider>();
    }

    private void Update()
    {
        UpdateUI(GameManager.Instance.playerMoney, GameManager.Instance.playerLvl);
    }

    public void UpdateUI(int scoreToPrint, int actualLevel)
    {
        coin.text = "$ : " + scoreToPrint;
        levelTxt.text = "Level : " + actualLevel;
    }
   
    public void UpdateUI(float life)
    {
        lifeSlider.value = life;
    }
    public void GoBackToMenu()
    {
        GameManager.Instance.SaveData();
        SceneManager.LoadScene(0);
    }
}
