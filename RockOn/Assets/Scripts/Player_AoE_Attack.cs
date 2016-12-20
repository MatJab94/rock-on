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
    private ArrayList _targets;

    // a flag used to make attack trigger only once per click
    private bool _isAttacking;

    // this object's sprite renderer (for highlighting the range when attacking)
    private SpriteRenderer _sr;

    // current player's color, from it's Color_Change script
    private Player_Color_Change _playerColor;

    // player's audio script for making sounds when attacking
    private Player_Audio _playerAudio;

    // mana bar in GUI
    private Player_Mana _playerMana;

    // min and max opacity and +- step for highlighting collider;
    private float _minOpacity;
    private float _maxOpacity;
    private float _step;

    public void Start()
    {
        _targets = new ArrayList();

        _isAttacking = false;

        _sr = gameObject.GetComponent<SpriteRenderer>();
        _sr.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();
        _playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Audio>();
        _playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Mana>();

        _minOpacity = 0.0f;
        _maxOpacity = 0.4f;
        _step = 0.02f;
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

                // do the AoE attack
                aoeAttack();
            }
        }
        // if AoE Attack is no longer pressed
        if (Input.GetAxisRaw("Attack_AoE") == 0)
        {
            // changing the flag so the attack can be triggered on next click
            _isAttacking = false;
        }
    }

    private void aoeAttack()
    {
        // attack only if player has enough mana
        if (_playerMana.getMana() > 0)
        {
            // highlights the range while attacking
            StartCoroutine("highlightCollider", _playerColor.getCurrentColor());

            // subtract mana for attack
            _playerMana.subtractMana();

            // play attack's sound
            _playerAudio.playChordSound();

            // apply damage to every enemy in range
            foreach (GameObject target in _targets)
            {
                target.GetComponent<Demon_Health>().applyDamage();
            }
        }
    }

    // highlights the collider while attacking
    IEnumerator highlightCollider(Color c)
    {
        _sr.color = c;

        // highlights the range
        for (float f = _minOpacity; f <= _maxOpacity; f += _step)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }

        // and fades it back to transparent
        for (float f = _maxOpacity; f >= _minOpacity; f -= _step)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }

        // makes sure it equals exactly _minOpacity at the end, and not slightly less
        c.a = _minOpacity;
        _sr.color = c;
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
