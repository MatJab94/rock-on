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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (bgAudio.mute)
                bgAudio.mute = false;
            else
                bgAudio.mute = true;
        }
    }

}
