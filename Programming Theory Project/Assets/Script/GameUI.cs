using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI levelTxt, coin;
    private Slider lifeSlider;

    public Transform AssetContainer;
    public GameObject buttonTypeFriendly;
   // LevelManager levelManager;
    private void Start()
    {
        //levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelTxt = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        coin = GameObject.Find("coinTxt").GetComponent<TextMeshProUGUI>();
        lifeSlider = GameObject.Find("LifeSlider").GetComponent <Slider>();

        //Creer le menu de sélection des unités amis
        for(int i = 0; i < GameManager.Instance.playerLvl; i++)
        {
            Debug.Log(LevelManager.instance.frienlyPrefab.Length);
            GameObject unitButton = Instantiate(buttonTypeFriendly, AssetContainer);
            Image unitImage = unitButton.GetComponent<Image>();
            TextMeshProUGUI costText = unitButton.GetComponentInChildren<TextMeshProUGUI>();

            unitImage.sprite = LevelManager.instance.frienlyPrefab[i] .GetComponent<DefenceUnit>().image;
            costText.text = LevelManager.instance.frienlyPrefab[i].GetComponent<DefenceUnit>().costValue.ToString();
            unitButton.GetComponent<AsserButton>().asset = LevelManager.instance.frienlyPrefab[i];
        }

    }

    private void Update()
    {
        UpdateUI(GameManager.Instance.playerMoney, GameManager.Instance.playerLvl);
        CanBuyAsset();
    }

    // Gestion de l'affichage des unités disponible à l'achat
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
                //Lance l'achat d'une unité en cas de click sur le button
                asset.GetComponent<Button>().onClick.AddListener(delegate { buyAUnit(asset.GetComponent<AsserButton>().asset); });
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
        SceneManager.LoadScene(0);
    }

    public void buyAUnit(GameObject activeAsset)
    {
        int numberOfSpot = LevelManager.instance.ShowAvailableSpot();
        GameManager.Instance.unitToSpawn = activeAsset;
        if(numberOfSpot == 0) 
        { 
            Debug.Log("no spawn point avalaible");
            GameManager.Instance.unitToSpawn = null;
        }
    }

    public void NextLvl()
    {
        GameManager.Instance.loadNextLevel();
    }

    public void WinGame()
    {
        GameManager.Instance.WinningGame();
    }

}
