using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINumberKills : MonoBehaviour
{
    public TextMeshProUGUI textNumberKills;

    public void DisplayNumberKills()
    {
        int score = int.Parse(textNumberKills.text);
        score += 1;
        textNumberKills.text = score.ToString();
    }    
}
