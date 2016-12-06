using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour
{

    // Use this for initialization

    public AudioClip D_chord;
    public AudioClip E_chord;
    public AudioClip G_chord;

    private AudioSource _mySound;
    private bool _isAttacking = false;

    public void playAudio()
    {
        if (GetComponent<Color_Change>().currentColorIndex == 0)
        {
            _mySound.PlayOneShot(D_chord);
        }
        if (GetComponent<Color_Change>().currentColorIndex == 1)
        {
            _mySound.PlayOneShot(E_chord);
        }
        if (GetComponent<Color_Change>().currentColorIndex == 2)
        {
            _mySound.PlayOneShot(G_chord);
        }
    }



    void Start()
    {
        _mySound = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (Input.GetAxisRaw("Attack1") != 0)
        {
            if (_isAttacking == false)
            {
                _isAttacking = true;
                if (GetComponent<Color_Change>().currentColorIndex == 0)
                {
                    _mySound.PlayOneShot(D_chord);
                }
                if (GetComponent<Color_Change>().currentColorIndex == 1)
                {
                    _mySound.PlayOneShot(E_chord);
                }
                if (GetComponent<Color_Change>().currentColorIndex == 2)
                {
                    _mySound.PlayOneShot(G_chord);
                }
            }
        }

        if (Input.GetAxisRaw("Attack1") == 0)
        {
            _isAttacking = false;
        }

    }
}
