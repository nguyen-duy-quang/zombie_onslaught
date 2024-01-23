using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleSettingsButton : MonoBehaviour
{
    public Image buttonSprite;
    public GameObject function;

    public void OnClickSettingsButton(Sprite onClickSprite)
    {
        buttonSprite.sprite = onClickSprite;
        function.SetActive(true);

        ButtonSettingsClickSound();
    }    

    public void CloseSettingsFunction(Sprite currentOnClickSprite)
    {
        buttonSprite.sprite = currentOnClickSprite;
        function.SetActive(false);

        ButtonSettingsClickSound();
    }    

    private void ButtonSettingsClickSound()
    {
        AudioManager._instance.ButtonClickSound();
    }
}
