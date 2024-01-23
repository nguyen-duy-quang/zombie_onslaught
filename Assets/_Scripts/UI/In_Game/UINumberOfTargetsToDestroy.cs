using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINumberOfTargetsToDestroy : MonoBehaviour
{
    public ScriptableObjectRule rule;

    public TextMeshProUGUI textNumberOfTargetsToDestroy;

    public int currentNumberOfTargetsToDestroy;
    public string complete;

    private void Start()
    {
        currentNumberOfTargetsToDestroy = rule.numberOfKill;

        DisplayNumberOfTargets();
    }

    public void CalculateNumberOfTargetsToDestroy()
    {
        currentNumberOfTargetsToDestroy -= 1;
        DisplayNumberOfTargets();

        if (currentNumberOfTargetsToDestroy <= 0)
        {
            currentNumberOfTargetsToDestroy = 0;
            textNumberOfTargetsToDestroy.text = $"<size=23><color=green>{complete}</color></size>";
        }
    }

    private void DisplayNumberOfTargets()
    {
        textNumberOfTargetsToDestroy.text = $"<size=23><color=green>{currentNumberOfTargetsToDestroy}</color></size>";
    }
}
