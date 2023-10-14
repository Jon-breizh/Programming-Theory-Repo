using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    private TextMeshProUGUI levelTxt, coin;
    private Slider lifeSlider;

    void Start()
    {
        levelTxt = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        levelTxt.text = "Level : 1 ";
        coin = GameObject.Find("coinTxt").GetComponent<TextMeshProUGUI>();
        coin.text = "$ : 0 ";
        lifeSlider = GameObject.Find("LifeSlider").GetComponent <Slider>();
        lifeSlider.value = 1.0f;
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
}
