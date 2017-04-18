using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Audio : MonoBehaviour {

    public AudioClip ComeOn2;

    private AudioSource _audioSource;
    private int _comeonSound;
    public int maxRange;
    private bool _played;
   
    // Use this for initialization
    void Start () {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.volume = 0.5f;
        _played = false;
    }
	
    public void PlayComeOn()
    {
        _comeonSound = Random.Range(0, maxRange);

        if (_played == false)
        {
            if (_comeonSound == 1)
            {
                _audioSource.Stop();
                _audioSource.PlayOneShot(ComeOn2);
                _played = true;
            }
        }
    }
}
