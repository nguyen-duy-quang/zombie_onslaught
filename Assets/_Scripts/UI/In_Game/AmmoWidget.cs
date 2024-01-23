using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoWidget : MonoBehaviour
{
    public TextMeshProUGUI ammoText;

    public void Refresh(int currentAmmo, int initialAmmo)
    {
        ammoText.text = currentAmmo.ToString() + " / " + initialAmmo.ToString();
    }
}
