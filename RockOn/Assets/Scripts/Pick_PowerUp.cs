using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_PowerUp : MonoBehaviour
{
    private Player_AttackTimeOut _playerTimeout;
    private Player_Regular_Attack _playerAttack;
    private GameObject _pickGUI;
    private int _pickActiveTime;

    private void Start()
    {
        _playerTimeout = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_AttackTimeOut>();
        _playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Regular_Attack>();
        _pickGUI = GameObject.FindGameObjectWithTag("GUI_GuitarPick");
        _pickActiveTime = 15;
    }

    IEnumerator pickPowerup()
    {
        GUI_Countdown countdown = _pickGUI.GetComponentInChildren<GUI_Countdown>();
        // wait until powerup runs out and update timer in GUI
        for (int i = 0; i <= _pickActiveTime; i++)
        {
            countdown.updateCountdown(_pickActiveTime - i);
            yield return new WaitForSecondsRealtime(1);
        }

        // deactivate powerup things
        countdown.turnOffCountdown();
        _playerTimeout.pickPowerUpOff();
        _playerAttack.setIsPickActive(false);
        _pickGUI.SetActive(false);
        Destroy(gameObject, 0.025f);
    }

    // event that is called if player enters this Object's collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_playerAttack.getIsPickActive())
            {
                // if pick is already active do nothing
            }
            else
            {
                // disable the object so it cant be picked up again
                gameObject.GetComponent<Collider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = null;

                // change flag in attack script to push enemies back when attacking
                _playerAttack.setIsPickActive(true);

                // activate the icon and timer in gui
                _pickGUI.SetActive(true);

                // change timeout of players attack
                _playerTimeout.pickPowerUpOn();

                // start coroutine that deactivates the powerup and kills the object
                StartCoroutine(pickPowerup());
            }
        }
    }
}
