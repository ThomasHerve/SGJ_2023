using System.Collections;
using System.Collections.Generic;
using Unity.MultiPlayerGame.Shared;
using UnityEngine;
using UnityEngine.Audio;

namespace Unity.MultiPlayerGame.Menu
{
    public class Options : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer mainMixer;


        public void ChangeMasterVolume(float sliderVal)
        {
            GameOptions.masterVolume = Mathf.Log10(sliderVal) * 20;
            mainMixer.SetFloat("MasterVolume", GameOptions.masterVolume);
        }

        public void ChangeMusicVolume(float sliderVal)
        {
            GameOptions.musicVolume = Mathf.Log10(sliderVal) * 20;
            mainMixer.SetFloat("MusicVolume", GameOptions.musicVolume);
        }
    }
}