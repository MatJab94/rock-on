using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Range : MonoBehaviour {

    public GameObject playerHealth;

    private bool _canAttack=false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_canAttack)
        {
            playerHealth.SendMessage("applyDamage");
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
