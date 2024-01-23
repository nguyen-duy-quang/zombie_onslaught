using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Currency", menuName = "Home/Currency")]
public class ScriptableObjectCurrency : ScriptableObject
{
    public Sprite currencyIcon;
    public int currency;
}
