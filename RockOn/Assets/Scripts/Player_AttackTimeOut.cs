using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackTimeOut : MonoBehaviour
{
    private bool _timeoutFlag;

    // set timeout in inspector, 0.3 seems fine
    public float timeoutTime;

    // Use this for initialization
    void Start()
    {
        _timeoutFlag = false;
    }

    // timer counts down after player's attack, during this time player can't attack
    IEnumerator timeout()
    {
        _timeoutFlag = true;
        yield return new WaitForSeconds(timeoutTime);
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
