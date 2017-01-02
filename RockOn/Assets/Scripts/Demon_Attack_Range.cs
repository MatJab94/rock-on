using System.Collections;
using UnityEngine;

public class Demon_Attack_Range : MonoBehaviour
{
    // Demon's SpriteRenderer
    private SpriteRenderer _sr;

    // flag shows if enemy is in range to attack the player
    private bool _canAttack = false;

    // flag shows if attacking coroutine can be started again
    private bool _attacking = false;

    // shows if attack is finished and enemy waits for next attack
    private bool _cooldown = false;

    // Player's Health script
    private Player_Health _playerHealth;

    void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();
        _sr = GetComponentInParent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (_canAttack && !_attacking)
        {
            _attacking = true;
            StartCoroutine("attackPlayer");
        }
    }
    
    IEnumerator attackPlayer()
    {
        // make enemy darker just before attack
        Color c = _sr.color;
        for (float f = 1.0f; f >= 0.67f; f -= Time.deltaTime*2)
        {
            c.r = f;
            c.g = f;
            c.b = f;
            _sr.color = c;
            yield return null;
        }

        // attack
        _playerHealth.applyDamage(1);

        for (float f = 0.25f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }

        // change color back to original
        c.r = 1.0f;
        c.g = 1.0f;
        c.b = 1.0f;
        _sr.color = c;

        _cooldown = true;
        // wait for some time before Demon can attack again
        for (float f = 1.0f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }
        _cooldown = false;

        // change flag so the next attack can start
        _attacking = false;
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
}
