using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider soundFXSlider;
    [SerializeField] private Slider musicSlider;

    public void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 1f);
        soundFXSlider.value = PlayerPrefs.GetFloat("soundFXVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);

        audioMixer.SetFloat("masterVolume", Mathf.Log10(masterSlider.value) * 20f);
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(soundFXSlider.value) * 20f);
        audioMixer.SetFloat("musicVolume", Mathf.Log10(musicSlider.value) * 20f);
    }

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("masterVolume", level);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("soundFXVolume", level);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("musicVolume", level);
    }
}
