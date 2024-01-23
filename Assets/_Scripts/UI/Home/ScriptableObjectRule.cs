using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rule", menuName = "Home/Rule")]
public class ScriptableObjectRule : ScriptableObject
{
    public string introduction;
    public int numberOfKill;
    public int time;
}
