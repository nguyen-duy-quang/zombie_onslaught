using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITrophies : MonoBehaviour
{
    public Image imageTrophies;
    public TextMeshProUGUI textQuantity;

    public ScriptableObjectUITrophies scriptableObjectUITrophies;

    private void Start()
    {
        LoadTrophiesItem();
    }

    public void LoadTrophiesItem()
    {
        imageTrophies.sprite = scriptableObjectUITrophies.imageTrophies;
        textQuantity.text = scriptableObjectUITrophies.quantity.ToString();
    }    
}
