using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {


    public AudioSource audioSource;
    public Slider VolumeSlider;
    public string volumeSliderValue;

    public void Start()
    {
        // VolumeSlider.value = audioSource.volume;
      //  audioSource.volume = PlayerPrefs.GetFloat(volumeSliderValue);
        VolumeSlider.value = PlayerPrefs.GetFloat(volumeSliderValue);
    }
    public void changeVolume()
    {
        audioSource.volume = VolumeSlider.value;
        PlayerPrefs.SetFloat(volumeSliderValue, VolumeSlider.value);
    }

}
