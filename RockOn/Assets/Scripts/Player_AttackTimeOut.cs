using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackTimeOut : MonoBehaviour
{
    private bool _timeoutFlag;

    // set timeout in inspector, 0.3 seems fine
    public float defaultTimeout;

    // current timeout
    private float _currentTimeout;

    // timeout when pick powerup is active
    private float _pickTimeout;

    // Use this for initialization
    void Start()
    {
        _timeoutFlag = false;
        _currentTimeout = defaultTimeout;

        // pick timeout will be 55% of regular timeout
        _pickTimeout = defaultTimeout * 0.55f;
    }

    public void pickPowerUpOn()
    {
        _currentTimeout = _pickTimeout;
    }

    public void pickPowerUpOff()
    {
        _currentTimeout = defaultTimeout;
    }

    // timer counts down after player's attack, during this time player can't attack
    IEnumerator timeout()
    {
        _timeoutFlag = true;
        yield return new WaitForSeconds(_currentTimeout);
        _timeoutFlag = false;
    }

    public void startTimeout()
    {
        StartCoroutine(timeout());
    }

    public bool getTimeoutFlag()
    {
        return _timeoutFlag;
    }
}
