using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuBuyItem : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public Image itemImage;
    public TextMeshProUGUI numberOfItem;
    public TextMeshProUGUI price;
    public Image currencyImage;

    public ScriptableObjectMenuBuyItem menuBuyItem;
    public ScriptableObjectCurrency currency;

    private int currentItemNumber;
    public int maxNumberOfItem = 5;
    public Button buttonBuyItem;
    public HandleMenuBuyItem handleMenuBuyItem;

    public UIMedpack uIMedpack;

    private void Start()
    {
        DisplayItem();
        currentItemNumber = menuBuyItem.numberOfItem;

        buttonBuyItem.onClick.AddListener(BuyItem);

        uIMedpack.DisplayMedpack(currentItemNumber);
    }

    private void DisplayItem()
    {
        itemName.text = menuBuyItem.itemName;
        itemImage.sprite = menuBuyItem.itemIcon;
        DisplayNumberOfItem();
        price.text = menuBuyItem.price.ToString();
        currencyImage.sprite = currency.currencyIcon;
    }    

    private void DisplayNumberOfItem()
    {
        numberOfItem.text = currentItemNumber.ToString() + "/" + maxNumberOfItem.ToString();
    }

    private void BuyItem()
    {
        handleMenuBuyItem.HandleBuyItem(ref currentItemNumber, maxNumberOfItem, menuBuyItem.price, ref currency.currency);
        DisplayNumberOfItem();
        uIMedpack.DisplayNumberOfMedpack(currentItemNumber);
    }    
}
