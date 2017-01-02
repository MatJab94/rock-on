using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery_AddMana : MonoBehaviour
{
    private Player_Mana _playerMana;

    private void Start()
    {
        _playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Mana>();
    }

    // event that is called if player enters this Object's collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_playerMana.getMana() < _playerMana.getMaxMana())
            {
                _playerMana.addMana(3);

                Destroy(gameObject, 0.05f);
            }
        }
    }
}
