using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OnClickSprite", menuName = "Home/OnClickSprite")]
public class ScriptableObjectOnClickSprite : ScriptableObject
{
    public Sprite onClickSprite;
    public Sprite currentOnClickSprite;
}
