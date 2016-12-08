using UnityEngine;
using System.Collections;

public class Enemy_Movement : MonoBehaviour
{
    // set these public values in Inspector
    public float speed; // movement speed
    public float maxRange; // max range at which it stops chasing the target
    public float minRange; // min range at which it stops chasing the target

    private Transform _target; // target's position
    private Transform _enemy; // this object's position
    private Rigidbody2D _rb; // this objects's rigidbody2d
    private float _distance; // distance between enemy and target

    void Start()
    {
        // initializing variables
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _enemy = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // calculate distance between enemy and target (player)
        _distance = Vector2.Distance(_enemy.position, _target.position);

        // chase target only within set range
        if (_distance <= maxRange && _distance >= minRange)
        {
            // calculate direction (and normalize it so it doesn't change the speed of movement)
            Vector2 direction = new Vector2(_target.position.x - _enemy.position.x, _target.position.y - _enemy.position.y).normalized;
            // move enemy according to the direction vector
            _rb.AddForce(direction * speed, ForceMode2D.Impulse);
        }
    }
}
