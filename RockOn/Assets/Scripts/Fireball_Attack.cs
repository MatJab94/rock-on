using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Attack : MonoBehaviour
{
    // Player's health script
    private Player_Health _playerHealth;

    // This fireball's health script
    private Fireball_Health _fireballHealth;

    void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();
        _fireballHealth = GetComponentInParent<Fireball_Health>();
    }
    
    // event that is called if player enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerHealth.applyDamage(2);
            _fireballHealth.applyDamage(1, true, false, false);
        }

        if (collision.gameObject.tag == "Wall")
        {
            _fireballHealth.applyDamage(1, true, false, false);
        }

        if (collision.gameObject.tag == "ClosedDoor")
        {
            _fireballHealth.applyDamage(1, true, false, false);
        }
    }
}
