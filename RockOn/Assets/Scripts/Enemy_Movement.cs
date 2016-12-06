using UnityEngine;
using System.Collections;

public class Enemy_Movement : MonoBehaviour
{

    public float speed;
    public float maxRange;
    public float minRange;

    private Transform _target;
    private float _distance;
    private Transform _enemy;
    private Rigidbody2D _rb;

    // Use this for initialization
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _enemy = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _distance = Vector2.Distance(_enemy.position, _target.position);

        if (_distance <= maxRange && _distance >= minRange)
        {
            Vector2 direction = new Vector2(_target.position.x - _enemy.position.x, _target.position.y - _enemy.position.y);
            _rb.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
        }
    }
}
