using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponUpgrades : MonoBehaviour
{
    public TextMeshProUGUI textCurrentGem;

    public Button btnUpgrade;
    public Slider levelSlider;
    public TextMeshProUGUI textDamage;
    public TextMeshProUGUI textWeaponLevel;

    public ScriptableObjectCurrentGem currentGem;
    public ScriptableObjectWeaponUpgrades weaponUpgrades;

    public HandleWeaponUpgrades handleWeaponUpgrades;
    public UICurrency uICurrency;

    public float currentValue = 25f;
    public int maxLevel;

    private void Start()
    {
        levelSlider.minValue = 0f;
        levelSlider.maxValue = 100f;

        handleWeaponUpgrades.saveWeaponLevel.LoadData();

        DisplayCurrentGem();
        DisplayWeaponUpgrades();

        btnUpgrade.onClick.AddListener(WeaponUpgrades);
    }

    private void DisplayCurrentGem()
    {
        textCurrentGem.text = currentGem.currentGem.ToString();
    }

    private void WeaponUpgrades()
    {
        handleWeaponUpgrades.WeaponUpgrades(uICurrency, currentGem.currentGem, ref maxLevel, ref currentValue, ref weaponUpgrades.levelValue, levelSlider.maxValue, ref weaponUpgrades.damage, ref weaponUpgrades.weaponLevel);

        DisplayWeaponUpgrades();

        AudioManager._instance.ButtonClickSound();
    }

    private void DisplayWeaponUpgrades()
    {
        levelSlider.value = weaponUpgrades.levelValue;

        textDamage.text = weaponUpgrades.damage.ToString();
        textWeaponLevel.text = "Lv." + weaponUpgrades.weaponLevel.ToString();
    }
}
