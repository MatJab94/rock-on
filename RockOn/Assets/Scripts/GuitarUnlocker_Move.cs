using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarUnlocker_Move : MonoBehaviour
{

    private Transform _tf;
    private Vector3 _position1, _position2;
    private bool direction;
    private Vector3 move;

    void Start()
    {
        _tf = GetComponent<Transform>();
        _position1 = _tf.position;
        _position2 = _position1;
        _position2.y += 0.2f;
        direction = false;
        move = _tf.position;

        // for depth, since it's not working in regular depth script
        move.z = move.y;
    }

    void FixedUpdate()
    {
        //Debug.Log("TF: " + _tf.position.y + "   pos1: " + _position1.y + "  pos2: " + _position2.y + "  direction: " + direction);

        if (_tf.position.y <= _position2.y && !direction)
        {
            move.y += 0.004f;
            move.z += 0.004f;
        }

        if (_tf.position.y >= _position1.y && direction)
        {
            move.y -= 0.004f;
            move.z -= 0.004f;
        }

        if (_tf.position.y >= _position2.y)
        {
            direction = true;
        }

        if (_tf.position.y <= _position1.y)
        {
            direction = false;
        }

        _tf.position = move;
    }
}
