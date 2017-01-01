using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackTimeOut : MonoBehaviour {

    private bool _timeoutFlag;
    private float _timeoutTime;

    // Use this for initialization
    void Start () {
        _timeoutFlag = false;
        _timeoutTime = 0.5f;
    }

    // timer counts down after player's attack, during this time player can't attack
    IEnumerator timeout()
    {
        _timeoutFlag = true;
        for (float time = _timeoutTime; time > 0; time -= Time.deltaTime)
        {
            yield return null;
        }
        _timeoutFlag = false;
    }

    public void startTimeout()
    {
        StartCoroutine("timeout");
    }

    public bool getTimeoutFlag()
    {
        return _timeoutFlag;
    }
}
