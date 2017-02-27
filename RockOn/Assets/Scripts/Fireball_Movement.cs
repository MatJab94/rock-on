using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Movement : MonoBehaviour
{
    private float _moveSpeed; // movement speed
    private float _rotateSpeed; // rotation speed
    private Transform _target; // target's position
    private Transform _fireball; // this object's position
    private Rigidbody2D _rb; // this objects's rigidbody2d
    private float _distance; // distance between enemy and target
    private Animator _anim; // animator of this object

    void Start()
    {
        // initializing variables
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _fireball = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _moveSpeed = 1.2f;
        _rotateSpeed = 1.5f;

        // rotate fireball towards target on spawn
        _fireball.rotation = getRotation();
    }

    void FixedUpdate()
    {
        rotateFireball();
        moveFireball();
    }

    void rotateFireball()
    {
        // some magic code from the internets, rotates the fireball towards target
        Quaternion q = getRotation();
        _fireball.rotation = Quaternion.RotateTowards(transform.rotation, q, _rotateSpeed);
    }

    void moveFireball()
    {
        // move fireball forward ("forward" direction is based on rotation)
        _rb.AddForce(_fireball.right * _moveSpeed, ForceMode2D.Impulse);
    }

    Quaternion getRotation()
    {
        Vector3 vectorToTarget = _target.position - _fireball.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
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
        _anim.SetTrigger("beat");
    }
}
