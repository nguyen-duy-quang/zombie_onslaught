using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedGunBackground : MonoBehaviour
{
    public Image imageGunBackground;

    public void InitGunBackground()
    {
        Color imageColor = imageGunBackground.color;
        imageColor.a = 0f;
        imageGunBackground.color = imageColor;
    }    

    public void ShowGunBackground()
    {
        Color imageColor = imageGunBackground.color;
        imageColor.a = 0.5882353f;
        imageGunBackground.color = imageColor;
    }    
}
