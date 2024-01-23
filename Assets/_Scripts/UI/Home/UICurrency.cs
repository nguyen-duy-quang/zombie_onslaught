using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICurrency : MonoBehaviour
{
    public TextMeshProUGUI textCurrency;
    public Image currencyImage;

    public ScriptableObjectCurrency scriptableObjectCurrency;

    public SaveCurrency saveCurrency;

    private void Start()
    {
        saveCurrency.LoadData();
        DisplayCurrency();
    }

    public void DisplayCurrency()
    {
        currencyImage.sprite = scriptableObjectCurrency.currencyIcon;
        DisplayCurrencyAccount();
    }    

    public void DisplayCurrencyAccount()
    {
        textCurrency.text = scriptableObjectCurrency.currency.ToString();
    }
}
