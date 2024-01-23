using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public ScriptableObjectOnClickSprite onClickSprite;

    public HandleSettingsButton audioSettings;
    public HandleSettingsButton graphicsSettings;

    public Button closeSettings;

    public Button audioButton;
    public Button graphicsButton;

    public SaveAudio saveAudio;

    private void Start()
    {
        AudioButtonClick();

        saveAudio.LoadData();

        audioButton.onClick.AddListener(AudioButtonClick);
        graphicsButton.onClick.AddListener(GraphicsButtonClick);

        closeSettings.onClick.AddListener(OnClickCloseSettings);
    }

    private void OnClickCloseSettings()
    {
        gameObject.SetActive(false);
        AudioManager._instance.ButtonClickSound();

        saveAudio.SaveData();
    }    

    private void AudioButtonClick()
    {
        audioSettings.OnClickSettingsButton(onClickSprite.onClickSprite);
        graphicsSettings.CloseSettingsFunction(onClickSprite.currentOnClickSprite);
    }
    private void GraphicsButtonClick()
    {
        graphicsSettings.OnClickSettingsButton(onClickSprite.onClickSprite);
        audioSettings.CloseSettingsFunction(onClickSprite.currentOnClickSprite);
    }
}
