using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI levelTxt, coin;
    private Slider lifeSlider;

    public Transform AssetContainer;
    public GameObject buttonTypeFriendly;

    void Start()
    {
        levelTxt = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        coin = GameObject.Find("coinTxt").GetComponent<TextMeshProUGUI>();
        lifeSlider = GameObject.Find("LifeSlider").GetComponent <Slider>();

        GameObject[] FriendlyUnit = GameManager.Instance.frienlyPrefab;

        for(int i = 0; i < GameManager.Instance.playerLvl; i++)
        {
            GameObject unitButton = Instantiate(buttonTypeFriendly, AssetContainer);
            Image unitImage = unitButton.GetComponent<Image>();
            TextMeshProUGUI costText = unitButton.GetComponentInChildren<TextMeshProUGUI>();

            unitImage.sprite = FriendlyUnit[i].GetComponent<DefenceUnit>().image;
            costText.text = FriendlyUnit[i].GetComponent<DefenceUnit>().costValue.ToString();

        }
    }

    private void Update()
    {
        UpdateUI(GameManager.Instance.playerMoney, GameManager.Instance.playerLvl);
        CanBuyAsset();
    }

    public void CanBuyAsset()
    {
        int availableMoney = GameManager.Instance.playerMoney;
        GameObject[] assetButton = GameObject.FindGameObjectsWithTag("UIAsset");

        foreach ( GameObject asset in assetButton)
        {
            string assetCoast = asset.GetComponentInChildren<TextMeshProUGUI>().text;
            if (int.Parse(assetCoast) > availableMoney)
            {
                // désactive le boutton
                asset.GetComponent<Button>().enabled = false;
                //Place le boutton légèrement transparent
                ColorBlock colorBlock = asset.GetComponent<Button>().colors;
                colorBlock.normalColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                asset.GetComponent<Button>().colors = colorBlock;
            }
            else
            {
                //active le boutton
                asset.GetComponent<Button>().enabled = true;
                //Place le boutton pleinement visible
                ColorBlock colorBlock = asset.GetComponent<Button>().colors;
                colorBlock.normalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                asset.GetComponent<Button>().colors = colorBlock;
            }
        }
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
