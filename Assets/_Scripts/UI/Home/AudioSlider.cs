using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public void InitAudioVolume(Slider audioSlider, ScriptableObjectAudio audio)
    {
        audio.audioVolume = audioSlider.value;
    }
}
