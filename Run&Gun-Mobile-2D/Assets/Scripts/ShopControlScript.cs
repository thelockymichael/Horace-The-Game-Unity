using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControlScript : MonoBehaviour
{
    int currencyAmount;
    int isRifleSold;

    public Text riflePrice;
    public Text currencyAmountText;

    public Button buyButton;

    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        currencyAmount = PlayerPrefs.GetInt("currencyPref");

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        currencyAmountText.text = currencyAmount.ToString();

        isRifleSold = PlayerPrefs.GetInt("IsRifleSold");
        Debug.Log(isRifleSold);
        //PlayerPrefs.DeleteKey("IsRifleSold");

        if (currencyAmount >= 5 && isRifleSold == 0)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }
    public void buyRifle()
    {
        //gameController.currencyAmount -= 5;
        currencyAmount -= 5;
        PlayerPrefs.SetInt("currencyPref", currencyAmount);
        PlayerPrefs.SetInt("IsRifleSold", 1);
        riflePrice.text = "Sold!";
        buyButton.gameObject.SetActive(false);
    }

    public void exitShop()
    {
        PlayerPrefs.SetInt("currencyPref", currencyAmount);
    }

    public void resetPlayerPrefs()
    {
        currencyAmount = 0;
        buyButton.gameObject.SetActive(true);
        riflePrice.text = "Price: 5$";
        PlayerPrefs.DeleteKey("currencyPref");
    }
}
