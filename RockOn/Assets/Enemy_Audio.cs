using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Audio : MonoBehaviour {

    public AudioClip ComeOn1;
    public AudioClip ComeOn2;
    public AudioClip ComeOn3;

    private AudioSource _audioSource;
    private int _comeonSound;
    public int maxRange;
   
    // Use this for initialization
    void Start () {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.volume = 0.5f;
    }
	
    public void PlayComeOn()
    {
        _comeonSound = Random.Range(0, maxRange);

        switch (_comeonSound)
        {
            case 0:
                _audioSource.Stop();
                _audioSource.PlayOneShot(ComeOn1);
                break;
            case 1:
                _audioSource.Stop();
                _audioSource.PlayOneShot(ComeOn2);
                break;
            case 2:
                _audioSource.Stop();
                _audioSource.PlayOneShot(ComeOn3);
                break;
            case 3:
                break;
            default:
                break;
        }      
    }
}
