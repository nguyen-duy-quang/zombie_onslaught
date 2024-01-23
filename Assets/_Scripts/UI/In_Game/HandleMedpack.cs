using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleMedpack : MonoBehaviour
{
    public void HandleUsingMedpack(ref float oldTime, float nextTime, ref int numberOfMedpack, ref Slider healthSlider, ref float heathIsAdded, float maxHealthValue, PlayerHealth playerHealth, TextEffect healthTextEffect)
    {
        if (Input.GetKeyDown(KeyCode.H) && Time.time > oldTime + nextTime)
        {
            if (numberOfMedpack > 0 && healthSlider.value < maxHealthValue)
            {
                numberOfMedpack -= 1;
                healthSlider.value += heathIsAdded;
                playerHealth.playerScript.presentHealth += heathIsAdded;
                if (playerHealth.playerScript.presentHealth >= maxHealthValue)
                {
                    playerHealth.playerScript.presentHealth = maxHealthValue;
                }
                playerHealth.UpdateHealthIndex();
                healthTextEffect.ShowHealthText(heathIsAdded.ToString());
                AudioManager._instance.SoundUsingMedpack();
            }
            oldTime = Time.time;
        }
    }

    public void HandleUsingMedpackOnMobile(ref float oldTime, float nextTime, ref int numberOfMedpack, ref Slider healthSlider, ref float heathIsAdded, float maxHealthValue, PlayerHealth playerHealth, TextEffect healthTextEffect)
    {
        if (Time.time > oldTime + nextTime)
        {
            if (numberOfMedpack > 0 && healthSlider.value < maxHealthValue)
            {
                numberOfMedpack -= 1;
                healthSlider.value += heathIsAdded;
                playerHealth.playerScript.presentHealth += heathIsAdded;
                if (playerHealth.playerScript.presentHealth >= maxHealthValue)
                {
                    playerHealth.playerScript.presentHealth = maxHealthValue;
                }
                playerHealth.UpdateHealthIndex();
                healthTextEffect.ShowHealthText(heathIsAdded.ToString());
                AudioManager._instance.SoundUsingMedpack();
            }
            oldTime = Time.time;
        }
    }
}    
