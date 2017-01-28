using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBgMusic : MonoBehaviour {

    // Use this for initialization

    public AudioSource bgAudio;

	void Start () {

        AudioListener.pause = false;
        Time.timeScale = 1;
        bgAudio.Play();
	}
	
}
