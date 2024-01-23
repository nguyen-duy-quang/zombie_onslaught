using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] audioClips; // Khai báo mảng AudioClip

    public ScriptableObjectAudio audio;

    void Start()
    {
        InnitMusicVolume();

        PlayRandomClip(); // để chạy random 1 AudioClip mỗi khi nhấn chạy chương trình
    }

    public void InnitMusicVolume()
    {
        musicSource.volume = audio.audioVolume;
    }

    void PlayRandomClip()
    {
        // Phát ngẫu nhiên một AudioClip trong mảng audioClips
        int randomIndex = Random.Range(0, audioClips.Length);
        musicSource.PlayOneShot(audioClips[randomIndex]); // phát ngẫu nhiên 1 bài tại 1 vị trí trong list
    }
}
