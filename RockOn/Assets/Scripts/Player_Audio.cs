using UnityEngine;

public class Player_Audio : MonoBehaviour
{
    // variables holding sound assets, set in Inspector
    public AudioClip D_chord;
    public AudioClip E_chord;
    public AudioClip G_chord;

    // this GameObject's AudioSource component
    private AudioSource _audioSource;

    private Player_Color_Change playerColor;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();
    }

    // called in attack scripts when player is attacking
    public void playChordSound()
    {
        if (playerColor.currentColorIndex == 0) // if color is red
        {
            _audioSource.Stop(); // stop playing any previous chord
            _audioSource.PlayOneShot(D_chord); // and play this chord
        }
        if (playerColor.currentColorIndex == 1) // if color is green
        {
            _audioSource.Stop(); // stop playing any previous chord
            _audioSource.PlayOneShot(E_chord); // and play this chord
        }
        if (playerColor.currentColorIndex == 2) // if color is blue
        {
            _audioSource.Stop(); // stop playing any previous chord
            _audioSource.PlayOneShot(G_chord); // and play this chord
        }
    }
}
