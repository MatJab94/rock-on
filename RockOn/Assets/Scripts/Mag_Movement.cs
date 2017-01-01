using System.Collections;
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

    // Use this for initialization
    void Start()
    {
        // initializing variables
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _enemy = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _attackScript = GetComponentInChildren<Mag_Attack_Range>();

        _speed = 0.75f;
        _maxRange = 2.5f;
    }

    void FixedUpdate()
    {
        // calculate distance between enemy and target (player)
        _distance = Vector2.Distance(_enemy.position, _target.position);

        // enemy moves only if it's not attacking and in range
        if (!_attackScript.isAttacking() && _distance <= _maxRange)
        {
            // moving the object, animate
            _anim.SetBool("isMoving", true);

            // calculate direction (and normalize it so it doesn't change the speed of movement)
            Vector2 direction = new Vector2(_enemy.position.x - _target.position.x, _enemy.position.y - _target.position.y).normalized;

            // move enemy according to the direction vector
            _rb.AddForce(direction * _speed, ForceMode2D.Impulse);

        }
        else
        {
            // object not moving, stop animation
            _anim.SetBool("isMoving", false);
        }
    }
}
