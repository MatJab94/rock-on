using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_FollowCursor : MonoBehaviour
{
    private Transform _cursorTF;
    private Transform _playerTF;
    private Transform _cameraTF;
    private Vector2 _move;
    private Vector2 _velocity = Vector3.zero;
    //private float _range;

    void Start()
    {
        _cursorTF = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Transform>();
        _playerTF = GetComponentInParent<Transform>();
        _cameraTF = GetComponent<Transform>();
        _move = Vector2.zero;
        //_range = 1.0f;
    }

    void FixedUpdate()
    {
        //float _distance = Vector2.Distance(_playerTF.localPosition, _cursorTF.localPosition);
        //if (_distance > _range)
        //{
        _move = new Vector2(_cursorTF.localPosition.x - _playerTF.localPosition.x, _cursorTF.localPosition.y - _playerTF.localPosition.y);
        _move.x = _move.x * 0.05f;
        _move.y = _move.y * 0.1f;
        _cameraTF.localPosition = Vector2.SmoothDamp(_cameraTF.localPosition, _move, ref _velocity, 0.005f, 25.0f, Time.deltaTime);
        _cameraTF.localPosition = new Vector3(_cameraTF.localPosition.x, _cameraTF.localPosition.y, -20.0f);
        //}
        //else
        //{
        //    _cameraTF.localPosition = Vector2.SmoothDamp(_cameraTF.localPosition, _playerTF.localPosition, ref _velocity, 0.05f, 5.0f, Time.deltaTime);
        //    _cameraTF.localPosition = new Vector3(_cameraTF.localPosition.x, _cameraTF.localPosition.y, -20.0f);
        //}

    }
}
