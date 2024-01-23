using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunsDisplay : MonoBehaviour
{
    public TextMeshProUGUI textAmountBullet;
    public Image imageGun;

    public ScriptableObjectChangeGuns changeGuns;

    public SelectedGunBackground selectedGunBackground;

    private void Start()
    {
        LoadGun();
    }

    private void LoadGun()
    {
        textAmountBullet.text = changeGuns.amountBullet;
        imageGun.sprite = changeGuns.imageGun;

        HideColor();
    }

    private void HideColor()
    {
        Color textColor = textAmountBullet.color;
        textColor.a = 0f;
        textAmountBullet.color = textColor;

        Color imageColor = imageGun.color;
        imageColor.a = 0f;
        imageGun.color = imageColor;

        selectedGunBackground.InitGunBackground();
    }

    public void ColorDisplay()
    {
        Color textColor = textAmountBullet.color;
        textColor.a = 1f;
        textAmountBullet.color = textColor;

        Color imageColor = imageGun.color;
        imageColor.a = 1f;
        imageGun.color = imageColor;
    }    
}
