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
    // ENCAPSULATION - variable declaration
    private TextMeshProUGUI levelTxt, coin;
    private Slider lifeSlider;

    [SerializeField] private Transform AssetContainer;
    [SerializeField] private GameObject buttonTypeFriendly;

    private void Start()
    {
        //levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelTxt = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        coin = GameObject.Find("coinTxt").GetComponent<TextMeshProUGUI>();
        lifeSlider = GameObject.Find("LifeSlider").GetComponent<Slider>();

        // INHERITANCE - Demonstrating inheritance
        // Create the menu for selecting friendly units
        for (int i = 0; i < GameManager.Instance.playerLvl; i++)
        {
            GameObject unitButton = Instantiate(buttonTypeFriendly, AssetContainer);
            Image unitImage = unitButton.GetComponent<Image>();
            TextMeshProUGUI costText = unitButton.GetComponentInChildren<TextMeshProUGUI>();

            unitImage.sprite = LevelManager.instance.friendlyPrefab[i].GetComponent<DefenseUnit>().image;
            costText.text = LevelManager.instance.friendlyPrefab[i].GetComponent<DefenseUnit>().costValue.ToString();
            unitButton.GetComponent<AsserButton>().asset = LevelManager.instance.friendlyPrefab[i];
        }
    }

    private void Update()
    {
        UpdateUI(GameManager.Instance.playerMoney, GameManager.Instance.playerLvl);
        CanBuyAsset();
    }

    // Gestion de l'affichage des unités disponible à l'achat
    // Demonstrates ABSTRACTION by handling the display of available purchase units.
    public void CanBuyAsset()
    {
        int availableMoney = GameManager.Instance.playerMoney;
        GameObject[] assetButton = GameObject.FindGameObjectsWithTag("UIAsset");

        foreach (GameObject asset in assetButton)
        {
            string assetCost = asset.GetComponentInChildren<TextMeshProUGUI>().text;
            if (int.Parse(assetCost) > availableMoney)
            {
                // Disable the button
                asset.GetComponent<Button>().enabled = false;
                // Make the button slightly transparent
                ColorBlock colorBlock = asset.GetComponent<Button>().colors;
                colorBlock.normalColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                asset.GetComponent<Button>().colors = colorBlock;
            }
            else
            {
                // Enable the button
                asset.GetComponent<Button>().enabled = true;
                // Make the button fully visible
                ColorBlock colorBlock = asset.GetComponent<Button>().colors;
                colorBlock.normalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                asset.GetComponent<Button>().colors = colorBlock;
                // POLYMORPHISM - Demonstrating polymorphism by dynamically binding the buyAUnit method.
                // Launch the purchase of a unit when the button is clicked
                asset.GetComponent<Button>().onClick.AddListener(delegate { buyAUnit(asset.GetComponent<AsserButton>().asset); });
            }
        }
    }

    //POLYMORPHISM - UpdateUI Methode
    public void UpdateUI(int scoreToPrint, int actualLevel)
    {
        coin.text = "$ : " + scoreToPrint;
        levelTxt.text = "Level : " + actualLevel;
    }
    //POLYMORPHISM - UpdateUI Methode
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
        if (numberOfSpot == 0)
        {
            GameManager.Instance.unitToSpawn = null;
        }
    }

    public void NextLvl()
    {
        GameManager.Instance.LoadNextLevel();
    }

    public void WinGame()
    {
        GameManager.Instance.WinningGame();
    }
}
