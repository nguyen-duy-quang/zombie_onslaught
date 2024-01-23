using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMedpack : MonoBehaviour
{
    public Image medpackImage;
    public TextMeshProUGUI textNumberOfMedpack;

    public ScriptableObjectMenuBuyItem menuBuyItem;

    public HandleMedpack handleMedpack;
    public PlayerHealth playerHealth;
    public TextEffect healthTextEffect;

    private int currentNumberOfMedpack;
    public float heathIsAdded;

    private float oldTime;
    public float nextTime = 3f;

    private void Update()
    {
        UsingMedpack();
    }

    public void DisplayMedpack(int numberOfMedpack)
    {
        medpackImage.sprite = menuBuyItem.itemIcon;
        DisplayNumberOfMedpack(numberOfMedpack);
    }    

    public void DisplayNumberOfMedpack(int numberOfMedpack)
    {
        UpdateNumberOfMedpack(numberOfMedpack);
        currentNumberOfMedpack = numberOfMedpack;
    }

    private void UpdateNumberOfMedpack(int numberOfMedpack)
    {
        textNumberOfMedpack.text = numberOfMedpack.ToString();
    }

    public void UsingMedpack()
    {
        handleMedpack.HandleUsingMedpack(ref oldTime, nextTime, ref currentNumberOfMedpack, ref playerHealth.health, ref heathIsAdded, playerHealth.health.maxValue, playerHealth, healthTextEffect);
        UpdateNumberOfMedpack(currentNumberOfMedpack);
    }

    // Mobile
    public void UsingMedpackOnMobile()
    {
        handleMedpack.HandleUsingMedpackOnMobile(ref oldTime, nextTime, ref currentNumberOfMedpack, ref playerHealth.health, ref heathIsAdded, playerHealth.health.maxValue, playerHealth, healthTextEffect);
        UpdateNumberOfMedpack(currentNumberOfMedpack);
    }    
}
