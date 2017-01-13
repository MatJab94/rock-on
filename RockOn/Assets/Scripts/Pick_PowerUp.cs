using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_PowerUp : MonoBehaviour
{
    private Player_AttackTimeOut _playerTimeout;
    private Player_Regular_Attack _playerAttack;
    private GameObject _pickGUI;
    private float _pickActiveTime;

    private void Start()
    {
        _playerTimeout = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_AttackTimeOut>();
        _playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Regular_Attack>();
        _pickGUI = GameObject.FindGameObjectWithTag("GUI_GuitarPick");
        _pickActiveTime = 15.0f;
    }

    IEnumerator pickPowerup()
    {
        yield return new WaitForSeconds(_pickActiveTime);
        _playerAttack.setIsPickActive(false);
        _pickGUI.SetActive(false);
        Destroy(gameObject, 0.025f);
    }

    // event that is called if player enters this Object's collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // disable the object so it cant be picked up again
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            
            // change timeout of players attack
            _playerTimeout.pickPowerUp(_pickActiveTime);

            // change flag in attack script to push enemies back when attacking
            _playerAttack.setIsPickActive(true);

            // activate the icon and timer in gui
            _pickGUI.SetActive(true);
            _pickGUI.GetComponentInChildren<GUI_Countdown>().startCountdown(_pickActiveTime);

            // start coroutine that deactivates the powerup and kills the object
            StartCoroutine(pickPowerup());
        }
    }
}
