﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag_Movement : MonoBehaviour
{
    private float _speed; // movement speed
    private float _maxRange; // max range at which it stops chasing the target (it's too far)
    private Transform _target; // target's position
    private Transform _enemy; // this object's position
    private Rigidbody2D _rb; // this objects's rigidbody2d
    private Animator _anim; // this object's animator
    private Mag_Attack_Range _attackScript; // for changing speed when attacking
    private float _distance; // distance between enemy and target
    private float _pushBackPower; // how strong is enemy pushed back when pick is active
    private Enemy_Audio _ea; // C'mon! sounds
    private bool isMoving = false, beatEven = false; // flags for animating in rythm

    // Use this for initialization
    void Start()
    {
        // initializing variables
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _enemy = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _attackScript = GetComponentInChildren<Mag_Attack_Range>();
        _ea = GetComponent<Enemy_Audio>();

        _speed = 0.75f;
        _maxRange = 2.5f;
        _pushBackPower = 30.0f;
    }

    // called when enemy is attacked and pick power-up is active
    public void pushBack()
    {
        // calculate direction (and normalize it so it doesn't change the speed of movement)
        Vector2 direction = new Vector2(_enemy.position.x - _target.position.x, _enemy.position.y - _target.position.y).normalized;
        _rb.AddForce(direction * _speed * _pushBackPower, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        // calculate distance between enemy and target (player)
        _distance = Vector2.Distance(_enemy.position, _target.position);

        // enemy moves only if it's not attacking and in range
        if (!_attackScript.isAttacking() && _distance <= _maxRange)
        {
            isMoving = true;

            StartCoroutine(Wait(_ea, 2)); //play C'mon! sound

            // calculate direction (and normalize it so it doesn't change the speed of movement)
            Vector2 direction = new Vector2(_enemy.position.x - _target.position.x, _enemy.position.y - _target.position.y).normalized;

            // move enemy according to the direction vector
            _rb.AddForce(direction * _speed, ForceMode2D.Impulse);

        }
        else
        {
            isMoving = false;
        }
    }

    IEnumerator Wait(Enemy_Audio _ea, float delay)  // for playing "Come On!"
    {
        _ea.PlayComeOn();
        _ea.enabled = true;
        yield return new WaitForSeconds(delay);
        _ea.enabled = false;
    }

    // objects need to subscribe and unsubscribe from events when they're enabled/disabled
    private void OnEnable()
    {
        RythmBattle.OnGoodRythm += rythmAnimation;
    }
    private void OnDisable()
    {
        RythmBattle.OnGoodRythm -= rythmAnimation;
    }

    // animate player on rythm events
    void rythmAnimation()
    {
        beatEven = !beatEven;
        if (isMoving)
        {
            if (beatEven) _anim.SetTrigger("beatEven");
            else _anim.SetTrigger("beatOdd");
        }
    }
}
