using UnityEngine;
using System.Collections;

public class Player_Audio : MonoBehaviour
{
    // variables holding sound assets, set in Inspector
    public AudioClip E_chord;
    public AudioClip D_chord;
    public AudioClip G_chord;

    // this GameObject's AudioSource component
    private AudioSource _audioSource;

    // a flag used to make sound trigger only once per click
    private bool _isAttacking = false;

    void Start()
    {
        // initializing this GameObject's AudioSource
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // playing different chords based on current Attack's Color
        // the IF makes sure that sound is triggered only once per click

        // if Attack is pressed (Left Mouse Button)
        if (Input.GetAxisRaw("Attack_Horizontal") != 0 || Input.GetAxisRaw("Attack_Vertical") != 0 || Input.GetAxisRaw("Attack_AoE") != 0)
        {
            if (_isAttacking == false)
            {
                // changing the flag so this code runs only once per click
                _isAttacking = true;
                if (GetComponent<Color_Change>().currentColorIndex == 0) // if color is red
                {
                    _audioSource.Stop(); // stop playing any previous chord
                    _audioSource.PlayOneShot(E_chord); // and play this chord
                }
                if (GetComponent<Color_Change>().currentColorIndex == 1) // if color is green
                {
                    _audioSource.Stop(); // stop playing any previous chord
                    _audioSource.PlayOneShot(D_chord); // and play this chord
                }
                if (GetComponent<Color_Change>().currentColorIndex == 2) // if color is blue
                {
                    _audioSource.Stop(); // stop playing any previous chord
                    _audioSource.PlayOneShot(G_chord); // and play this chord
                }
            }
        }
        // if Attack is no longer pressed
        if (Input.GetAxisRaw("Attack_Horizontal") == 0 && Input.GetAxisRaw("Attack_Vertical") == 0 && Input.GetAxisRaw("Attack_AoE") == 0)
        {
            // changing the flag so the chords can be played on next click
            _isAttacking = false;
        }
    }
}
