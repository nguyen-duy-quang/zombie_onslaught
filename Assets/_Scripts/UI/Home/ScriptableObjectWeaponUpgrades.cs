using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgrades", menuName = "Home/WeaponUpgrades")]
public class ScriptableObjectWeaponUpgrades : ScriptableObject
{
    public int damage;
    public float levelValue;
    public int weaponLevel;
}
