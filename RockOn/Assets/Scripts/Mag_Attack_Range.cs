using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag_Attack_Range : MonoBehaviour
{
    // set in Inspector
    public GameObject fireballPrefab;

    // this Object's SpriteRenderer
    private SpriteRenderer _sr;

    // flag shows if enemy is in range to attack the player
    private bool _canAttack = false;

    // flag shows if attacking coroutine can be started again
    private bool _canStartCoroutine = true;

    // Health Bar in GUI
    private Player_Health _playerHealth;

    // Use this for initialization
    void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();
        _sr = GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_canAttack && _canStartCoroutine)
        {
            _canStartCoroutine = false;
            StartCoroutine("shootFireball");
        }
    }

    // kills enemy when health <= 0
    IEnumerator shootFireball()
    {
        // wait for a second before attacking
        for (float f = 0.75f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }
        
        // make enemy darker just before attack
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


        // wait for some time before Mag can attack again
        for (float f = 1.5f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }
        _canStartCoroutine = true;
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
}
