using UnityEngine;
using System.Collections;

/*
 * Script attached to "AoE_Attack_Range" (child of Player).
 * It has a circle collider with "Is Trigger" checked.
 * 
 * Once an object with RigidBody2D collides with it,
 * the OnTrigger methods are called.
 */

public class Player_AoE_Attack : MonoBehaviour
{
    // list that contains all enemies (gameObjects) in range
    private ArrayList _targets = new ArrayList();

    // a flag used to make attack trigger only once per click
    private bool _isAttacking = false;

    // this object's sprite renderer
    private SpriteRenderer _sr;

    private Player_Mana _playerMana;

    public void Start()
    {
        _sr = gameObject.GetComponent<SpriteRenderer>();
        _playerMana = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Player_Mana>();
    }

    public void Update()
    {
        // if AoE Attack is pressed (Space)
        if (Input.GetAxisRaw("Attack_AoE") != 0)
        {

            if (_isAttacking == false)
            {
                // changing the flag so this code runs only once per click
                _isAttacking = true;

                if (_playerMana.getMana() > 0)
                {
                    // highlights the collider while attacking
                    StartCoroutine("highlightCollider");

                    // subtract mana for attack
                    _playerMana.subtractMana();

                    // calling applyDamage() method for every enemy in range
                    foreach (GameObject target in _targets)
                    {
                        target.GetComponent<Enemy_Health>().applyDamage();
                    }
                }
                
            }
        }
        // if AoE Attack is no longer pressed
        if (Input.GetAxisRaw("Attack_AoE") == 0)
        {
            // changing the flag so the attack can be triggered on next click
            _isAttacking = false;
        }
    }

    // highlights the collider while attacking
    IEnumerator highlightCollider()
    {
        Color c = _sr.color;
        for (float f = 0.25f; f <= 0.75f; f += 0.05f)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }
        for (float f = 0.75f; f >= 0.25f; f -= 0.05f)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
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
