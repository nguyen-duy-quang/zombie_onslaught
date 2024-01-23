using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;

    public AudioSource audioFx;
    public AudioSource audioShooting;

    [Header("Audio Clips")]
    public AudioClip[] footStepsSound;
    public AudioClip clickingSound;
    public AudioClip shootingSound;
    public AudioClip gunPickupSound;
    public AudioClip victorySound;
    public AudioClip defeatSound;
    public AudioClip reloadingSound;
    public AudioClip jumpSound;
    public AudioClip medpackSound;

    public ScriptableObjectAudio audio;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitFXVolume();
    }

    public void InitFXVolume()
    {
        audioFx.volume = audio.audioVolume;
        audioShooting.volume = audio.audioVolume;
    }   

    private AudioClip GetRandomFootStep()
    {
        return footStepsSound[Random.Range(0, footStepsSound.Length)];
    }

    public void StepSound()
    {
        AudioClip clip = GetRandomFootStep();
        audioFx.PlayOneShot(clip);
    }

    public void ButtonClickSound()
    {
        audioFx.clip = clickingSound;
        audioFx.Play();
    }

    public void ShootSound(bool isShooting)
    {
        if(isShooting)
        {
            audioShooting.clip = shootingSound;
            audioShooting.Play();
        }    
        else
        {
            audioShooting.clip = shootingSound;
            audioShooting.Stop();
        }    
    }

    public void GunPickupSound()
    {
        audioFx.clip = gunPickupSound;
        audioFx.Play();
    }

    public void GunReloadingSound()
    {
        audioFx.clip = reloadingSound;
        audioFx.Play();
    }

    public void JumpSound()
    {
        audioFx.clip = jumpSound;
        audioFx.Play();
    }

    public void SoundUsingMedpack()
    {
        audioFx.clip = medpackSound;
        audioFx.Play();
    }    

    public void VictoryGameSound()
    {
        audioFx.clip = victorySound;
        audioFx.Play();
    }

    public void DefeatGameSound()
    {
        audioFx.clip = defeatSound;
        audioFx.Play();
    }
}
