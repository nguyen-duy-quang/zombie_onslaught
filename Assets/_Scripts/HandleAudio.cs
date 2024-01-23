using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleAudio : MonoBehaviour
{
    public Slider music;
    public Slider FX;

    public AudioMenu audioMenu;

    public AudioSlider musicSlider;
    public AudioSlider FXSlider;

    private void Start()
    {
        music.value = audioMenu.audio.audioVolume;
        FX.value = AudioManager._instance.audio.audioVolume;

        music.onValueChanged.AddListener(HandleMusicSliderValueChanged);

        FX.onValueChanged.AddListener(HandleFXSliderValueChanged);
    }

    private void MusicVolume()
    {
        musicSlider.InitAudioVolume(music, audioMenu.audio);
    }

    private void FXVolume()
    {
        FXSlider.InitAudioVolume(FX, AudioManager._instance.audio);
    }

    private void HandleMusicSliderValueChanged(float value)
    {
        MusicVolume();
        audioMenu.InnitMusicVolume();
    }

    private void HandleFXSliderValueChanged(float value)
    {
        FXVolume();
        AudioManager._instance.InitFXVolume();
    }
}
