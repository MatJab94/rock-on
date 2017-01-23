using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mic_PowerUp : MonoBehaviour
{
    private Player_Regular_Attack _playerAttack;
    private Player_AoE_Attack _playerAoE;
    private GameObject _micGUI;
    private int _micActiveTime;

    private void Start()
    {
        _playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Regular_Attack>();
        _playerAoE = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_AoE_Attack>();
        _micGUI = GameObject.FindGameObjectWithTag("GUI_Microphone");
        _micActiveTime = 15;
    }

    IEnumerator pickPowerup()
    {
        GUI_Countdown countdown = _micGUI.GetComponentInChildren<GUI_Countdown>();
        // wait until powerup runs out and update timer in GUI
        for (int i = 0; i <= _micActiveTime; i++)
        {
            countdown.updateCountdown(_micActiveTime - i);
            yield return new WaitForSecondsRealtime(1);
        }

        // deactivate powerup things
        countdown.turnOffCountdown();
        _playerAttack.setMicActive(false);
        _playerAoE.setMicActive(false);
        Destroy(gameObject, 0.025f);
    }

    // event that is called if player enters this Object's collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_playerAttack.getMicActive())
            {
                // if pick is already active do nothing
            }
            else
            {
                // disable the object so it cant be picked up again
                gameObject.GetComponent<Collider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = null;

                // change flags in attack scripts
                _playerAttack.setMicActive(true);
                _playerAoE.setMicActive(true);
                
                // start coroutine that deactivates the powerup and kills the object
                StartCoroutine(pickPowerup());
            }
        }
    }
}
