using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {


    public AudioSource audioSource;
    public Slider VolumeSlider;

    public void Start()
    {
        //  audioSource = GetComponent<AudioSource>();
        VolumeSlider.value = audioSource.volume;
        audioSource.volume = PlayerPrefs.GetFloat("CurVol");
    }
    public void changeVolume()
    {
        audioSource.volume = VolumeSlider.value;
        PlayerPrefs.SetFloat("CurVol", VolumeSlider.value);
    }

}
