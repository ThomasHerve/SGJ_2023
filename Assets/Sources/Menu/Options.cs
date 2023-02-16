using System.Collections;
using System.Collections.Generic;
using Unity.MultiPlayerGame.Shared;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    [SerializeField]
    private  AudioMixer mainMixer;


    public void ChangeMasterVolume(float sliderVal)
    {
        PlayerOptions.masterVolume = Mathf.Log10(sliderVal) * 20;
        mainMixer.SetFloat("MasterVolume", PlayerOptions.masterVolume);
    }

    public void ChangeMusicVolume(float sliderVal)
    {
        PlayerOptions.musicVolume = Mathf.Log10(sliderVal) * 20;
        mainMixer.SetFloat("MusicVolume", PlayerOptions.musicVolume);
    }
}
