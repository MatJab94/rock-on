﻿using UnityEngine;
using System.Collections;
using System;

/*
 * Script attached to "AoE_Attack_Range" (child of Player).
 * It has a circle collider with "Is Trigger" checked.
 * 
 * Once an object with RigidBody2D collides with it,
 * the OnTrigger methods are called.
 */

public class Player_AoE_Attack : MonoBehaviour
{
    // sprites with range "animation", set in Inspector
    public Sprite[] _sprites;

    // list that contains all enemies (gameObjects) in range
    private ArrayList _targets;

    // a flag used to make attack trigger only once per click
    private bool _isAttacking;

    // this object's sprite renderer (for "animating" the range when attacking)
    private SpriteRenderer _sr;

    // this transform for changing scale ("animating")
    private Transform _tf;

    // current player's color, from it's Color_Change script
    private Player_Color_Change _playerColor;

    // player's audio script for making sounds when attacking
    private Player_Audio _playerAudio;

    // mana bar in GUI
    private Player_Mana _playerMana;

    public void Start()
    {
        _targets = new ArrayList();

        _isAttacking = false;

        _sr = GetComponent<SpriteRenderer>();
        _tf = GetComponent<Transform>();
        _tf.localScale = Vector3.zero;

        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();
        _playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Audio>();
        _playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Mana>();
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
            // change sprite of the range to match chosen color
            updateSprite();

            // subtract mana for attack
            _playerMana.subtractMana();

            // play attack's sound
            _playerAudio.playChordSound();

            // animates the range while attacking and applies damage to targets
            StartCoroutine("animateAndAttack");
        }
    }

    private void updateSprite()
    {
        _sr.sprite = _sprites[_playerColor.currentColorIndex];
    }

    private void attackTargets()
    {
        foreach (GameObject target in _targets)
        {
            target.GetComponent<Demon_Health>().applyDamage();
        }
    }

    // highlights the collider while attacking
    IEnumerator animateAndAttack()
    {
        Color c = Color.white;
        Vector3 scale = Vector3.zero;

        _sr.color = c;
        _tf.localScale = scale;

        // scales the range UP
        for (float f = 0.0f; f <= 1.0f; f += 0.05f)
        {
            scale.x = f;
            scale.y = f;
            _tf.localScale = scale;
            yield return null;
        }

        // apply damage to every enemy in range after range's scale is maximum
        attackTargets();

        // fades range to transparent
        for (float f = 1.0f; f >= 0.0f; f -= 0.03f)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }

        c.a = 1.0f;
        _sr.color = c;

        _tf.localScale = Vector3.zero;
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
