using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider health;
    public TextMeshProUGUI textPlayerHealth;

    private float presentHealth;

    public PlayerScript playerScript;

    private void Start()
    {
        health.maxValue = playerScript.presentHealth;
        health.value = playerScript.presentHealth;
        health.minValue = 0f;

        presentHealth = playerScript.presentHealth;
        InitHealth();
    }

    public void InitHealth()
    {
        textPlayerHealth.text = playerScript.presentHealth.ToString() + "/" + playerScript.presentHealth.ToString();
    }

    public void ReducePlayerHealth(float takeDamge)
    {
        health.value -= takeDamge;
        UpdateHealthIndex();
    }    

    public void UpdateHealthIndex()
    {
        textPlayerHealth.text = health.value.ToString() + "/" + presentHealth;
    }
}
