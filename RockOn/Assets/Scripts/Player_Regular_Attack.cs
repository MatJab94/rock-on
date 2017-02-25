﻿using System;
using System.Collections;
using UnityEngine;

public class Player_Regular_Attack : MonoBehaviour
{
    // player's transform for drawing line
    private Transform _playerAttackTransform;

    // target detection script from cursor
    private Cursor_TargetDetection _targetDetection;

    // transform of the cursor
    private Transform _cursorTF;

    // currently chosen target
    private GameObject _target;

    // max range at which target can be interacted with
    public float defaultMaxRange;
    private float _maxRange;
    private float maxRangeWithMicrophone;

    // player's audio script for making sounds when attacking
    private Player_Audio _playerAudio;

    // drawing line when attacking
    private LineRenderer _lr;

    // script that stops player from continuously attacking
    private Player_AttackTimeOut _timeoutScript;

    // to get value of rythm flag
    private RythmBattle rythmBattleScript;

    // for disabling the attacking on first level
    public bool attackDisabled;

    // is pick power-up active now?
    private bool _pickActive;

    // is microphpne power-up active?
    private bool _micActive;

    // damage of the regular attack
    private int _currentDamage;
    private int _regularDamage;
    private int _damageWithMicrophone;

    void Start()
    {
        _playerAttackTransform = GetComponent<Transform>();
        _targetDetection = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor_TargetDetection>();
        _cursorTF = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Transform>();
        _playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Audio>();
        _lr = GetComponent<LineRenderer>();
        _timeoutScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_AttackTimeOut>();
        rythmBattleScript = GameObject.FindGameObjectWithTag("RythmBattle").GetComponent<RythmBattle>();
        _lr.sortingLayerName = "UI";

        _regularDamage = 1;
        _damageWithMicrophone = 2;
        _currentDamage = _regularDamage;

        _target = null;
        _micActive = false;
        attackDisabled = false;
        _maxRange = defaultMaxRange;
        maxRangeWithMicrophone = defaultMaxRange * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_micActive)
        {
            _maxRange = maxRangeWithMicrophone;
            _currentDamage = _damageWithMicrophone;
        }
        else
        {
            _maxRange = defaultMaxRange;
            _currentDamage = _regularDamage;
        }

        // if player clicks the attack button (mouse 0 by default)
        if (Input.GetButtonDown("Regular_Attack") && _timeoutScript.getTimeoutFlag() == false && !attackDisabled)
        {
            //getting rythm flag for bonuses to mana if attacking in rythm
            regularAttack(rythmBattleScript.getRythmFlag());

            // start timeout after attacking
            _timeoutScript.startTimeout();
        }
    }

    private void regularAttack(bool rythmFlag)
    {
        // select current target
        _target = _targetDetection.getTarget();

        // if there is a target
        if (_target != null)
        {
            // and check if target is in range
            if (isInRange(_target.GetComponent<Transform>().position))
            {
                // play attack's sound
                _playerAudio.playChordSound();

                // draw a line from player to target
                StartCoroutine(drawLine(_target.GetComponent<Transform>().position));

                // hit or interact with the target object
                interactWithTarget(rythmFlag);
            }
            else // if target is out of range
            {
                // play bad sound (TO-DO, currently playing regular attack sound)
                _playerAudio.playChordSound();

                // draw the line from player to cursor
                calculateAndDrawLine();
            }
        }
        else // if there isn't any target there
        {
            // play bad sound (TO-DO, currently playing regular attack sound)
            _playerAudio.playChordSound();

            // draw the line from player to cursor
            calculateAndDrawLine();
        }
    }

    private void calculateAndDrawLine()
    {
        Vector2 pointInRange;
        // target is in range
        if (isInRange(_cursorTF.position))
        {
            pointInRange = _cursorTF.position; // final position
        }
        // if target is out of range
        else
        {
            pointInRange = _cursorTF.GetComponent<Transform>().position - _playerAttackTransform.position; // direction
            pointInRange = pointInRange.normalized * _maxRange * 0.9f; // distance
            pointInRange = (Vector2)_playerAttackTransform.position + pointInRange; // final position
        }
        StartCoroutine(drawLine(pointInRange)); // draw the line from player to target point
    }

    private void interactWithTarget(bool rythmFlag)
    {
        if (_target.transform.parent.gameObject.tag == "Demon")
        {
            _target.GetComponentInParent<Demon_Health>().applyDamage(_currentDamage, false, false, rythmFlag);
        }
        if (_target.transform.parent.gameObject.tag == "Mag")
        {
            _target.GetComponentInParent<Mag_Health>().applyDamage(_currentDamage, false, false);
        }
        if (_target.transform.parent.gameObject.tag == "Fireball")
        {
            _target.GetComponentInParent<Fireball_Health>().applyDamage(_currentDamage, false, false);
        }
        if (_target.transform.parent.gameObject.tag == "Chest")
        {
            _target.GetComponentInParent<Chest_Open>().hitChest();
        }
        if (_target.transform.parent.gameObject.tag == "ClosedDoor")
        {
            _target.GetComponentInParent<ClosedDoor_Open>().hitDoor();
        }
        if (_target.transform.parent.gameObject.tag == "Bomb")
        {
            _target.GetComponentInParent<Bomb_BlowUp>().hitBomb();
        }
    }

    private bool isInRange(Vector2 target)
    {
        // calculate distance between player and target
        float _distance = Vector2.Distance(_playerAttackTransform.position, target);

        if (_distance > _maxRange)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool getPickActive()
    {
        return _pickActive;
    }

    public void setPickActive(bool pickActive)
    {
        _pickActive = pickActive;
    }

    public bool getMicActive()
    {
        return _micActive;
    }

    public void setMicActive(bool micActive)
    {
        _micActive = micActive;
    }

    // draws a line between player and target when attacking
    IEnumerator drawLine(Vector2 target)
    {
        // set positions of player and target to draw the line
        _lr.SetPosition(0, _playerAttackTransform.position);
        _lr.SetPosition(1, target);

        // how long the line is visible
        yield return new WaitForSeconds(0.1f);

        // set position so the line is not visible
        _lr.SetPosition(0, _playerAttackTransform.position);
        _lr.SetPosition(1, _playerAttackTransform.position);
    }
}