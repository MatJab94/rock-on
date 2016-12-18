using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Range : MonoBehaviour
{

    private bool _canAttack = false;
    private Player_Health _playerHealth;

    // Use this for initialization
    void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Player_Health>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_canAttack)
        {
            _playerHealth.applyDamage();
        }
    }

    // event that is called if enemy enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _canAttack = true;
        }
    }

    // event that is called if enemy exits this Object's collider (is out of range)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _canAttack = false;
        }
    }

}
