using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag_Attack_Range : MonoBehaviour
{
    // set in Inspector
    public GameObject fireballPrefab;

    // Mage's SpriteRenderer
    private SpriteRenderer _sr;

    // flag shows if enemy is in range to attack the player
    private bool _canAttack = false;

    // flag shows if attacking coroutine can be started again
    private bool _attacking = false;

    // shows if attack is finished and enemy waits for next attack
    private bool _cooldown = false;

    // Use this for initialization
    void Start()
    {
        _sr = GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_canAttack && !_attacking && !_cooldown)
        {
            _attacking = true;
            StartCoroutine("shootFireball");
        }
    }

    // shoots a fireball towards player
    IEnumerator shootFireball()
    {
        // wait for some time before attacking
        for (float f = 0.75f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }
        
        // make enemy darker just before the attack
        Color c = _sr.color;
        for (float f = 1.0f; f >= 0.6f; f -= Time.deltaTime)
        {
            c.r = f;
            c.g = f;
            c.b = f;
            _sr.color = c;
            yield return null;
        }

        for (float f = 0.25f; f >= 0; f -= Time.deltaTime)
        {
            // wait a little more
            yield return null;
        }

        // attack
        Instantiate(fireballPrefab, gameObject.transform.position, Quaternion.identity);

        // change color back to original
        c.r = 1.0f;
        c.g = 1.0f;
        c.b = 1.0f;
        _sr.color = c;

        // attack is completed, cooldown starts
        _attacking = false;
        _cooldown = true;

        // wait for some time before Mage can attack again
        for (float f = 1.5f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }
        _cooldown = false;
    }

    // event that is called if player enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _canAttack = true;
        }
    }

    // event that is called if player exits this Object's collider (is out of range)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _canAttack = false;
        }
    }

    // can be used to stop enemy from moving while attacking or something
    public bool isCooldown()
    {
        return _cooldown;
    }

    public bool isAttacking()
    {
        return _attacking;
    }
}
