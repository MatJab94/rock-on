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

    // flag shows if attacking coroutine is in progress
    private bool _attackInProgress = false;

    // flag shows if enemy is firing a fireball right now
    private bool _firingFireball = false;

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
        if (_canAttack && !_attackInProgress && !_cooldown)
        {
            _attackInProgress = true;
            StartCoroutine(shootFireball());
        }
    }

    // shoots a fireball towards player
    IEnumerator shootFireball()
    {
        // wait for some time before attacking
        yield return new WaitForSeconds(0.75f);

        // change flag to stop moving during attack
        _firingFireball = true;

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

        // wait a little more
        yield return new WaitForSeconds(0.25f);

        // attack
        Instantiate(fireballPrefab, gameObject.transform.position, Quaternion.identity);

        // change color back to original
        c.r = 1.0f;
        c.g = 1.0f;
        c.b = 1.0f;
        _sr.color = c;

        // attack is completed, cooldown starts
        _firingFireball = false;
        _cooldown = true;

        // wait for some time before Mage can attack again
        yield return new WaitForSeconds(1.5f);
        _cooldown = false;
        _attackInProgress = false;
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
        return _firingFireball;
    }
}
