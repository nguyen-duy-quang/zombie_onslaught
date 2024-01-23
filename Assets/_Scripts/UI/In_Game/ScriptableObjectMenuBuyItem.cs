using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuBuyItem", menuName = "MenuBuyItem/MenuBuyItem")]
public class ScriptableObjectMenuBuyItem : ScriptableObject
{
    public string itemName;
    public Sprite  itemIcon;
    public int numberOfItem;
    public int price;
}
