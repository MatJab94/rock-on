using UnityEngine;

public class Demon_Movement : MonoBehaviour
{
    private float _speed; // movement speed
    private float _maxRange; // max range at which it stops chasing the target (it's too far)
    private float _minRange; // min range at which it stops chasing the target (it's too close)

    private Transform _target; // target's position
    private Transform _enemy; // this object's position
    private Rigidbody2D _rb; // this objects's rigidbody2d
    private Animator _anim; // this object's animator
    private float _distance; // distance between enemy and target

    void Start()
    {
        // initializing variables
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _enemy = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _speed = 1.25f;
        _maxRange = 3.0f;
        _minRange = 0.67f;
    }

    void FixedUpdate()
    {
        // calculate distance between enemy and target (player)
        _distance = Vector2.Distance(_enemy.position, _target.position);

        // chase target only within set range
        if (_distance <= _maxRange && _distance >= _minRange)
        {
            // moving the object, animate
            _anim.SetBool("isMoving", true);

            // calculate direction (and normalize it so it doesn't change the speed of movement)
            Vector2 direction = new Vector2(_target.position.x - _enemy.position.x, _target.position.y - _enemy.position.y).normalized;
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
