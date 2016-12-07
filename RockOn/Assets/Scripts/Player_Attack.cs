using UnityEngine;
using System.Collections;

/*
 * Script attached to "Attack_Range" (child of Player).
 * Attack_Range has a collider with "Is Trigger" checked.
 * 
 * Once an object with RigidBody2D collides with it,
 * the OnTrigger methods are called.
 */

public class Player_Attack : MonoBehaviour
{
    // list that contains all enemies in range
    private ArrayList _targets = new ArrayList();

    // a flag used to make attack trigger only once per click
    private bool _isAttacking = false;

    public void Update()
    {
        // if Attack1 is pressed (Left Mouse Button)
        if (Input.GetAxisRaw("Attack1") != 0)
        {

            if (_isAttacking == false)
            {
                // changing the flag so this code runs only once per click
                _isAttacking = true;

                // calling applyDamage() method for every enemy in range
                foreach (GameObject target in _targets)
                {
                    target.SendMessage("applyDamage");
                }
            }
        }
        // if Attack1 is no longer pressed
        if (Input.GetAxisRaw("Attack1") == 0)
        {
            // changing the flag so the attack can be triggered on next click
            _isAttacking = false;
        }
    }


    // event that is called if enemy enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // add this object to the list of enemies in range
            _targets.Add(collision.gameObject);
        }
    }

    // event that is called if enemy exits this Object's collider (is out of range)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // remove this object from the list of enemies in range
            _targets.Remove(collision.gameObject);
        }
    }
}
