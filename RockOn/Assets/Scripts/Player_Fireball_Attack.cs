using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fireball_Attack : MonoBehaviour
{
    // is microphpne power-up active?
    private bool _micActive;

    // max range at which target can be interacted with
    public float defaultMaxRange;
    private float _maxRange;
    private float maxRangeWithMicrophone;

    // damage of the regular attack
    private int _currentDamage;
    private int _regularDamage;
    private int _damageWithMicrophone;

    // script that stops player from continuously attacking
    private Player_AttackTimeOut _timeoutScript;

    // for disabling the attacking on first level
    public bool attackDisabled;

    // to get value of rythm flag
    private RythmBattle rythmBattleScript;

    // player's audio script for making sounds when attacking
    private Player_Audio _playerAudio;
    
    // prafab of player's fireball, set in Inspector
    public GameObject fireballPrefab;

    void Start()
    {
        _micActive = false;

        _maxRange = defaultMaxRange;
        maxRangeWithMicrophone = defaultMaxRange * 1.5f;

        _regularDamage = 1;
        _damageWithMicrophone = 2;
        _currentDamage = _regularDamage;

        _timeoutScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_AttackTimeOut>();

        attackDisabled = false;

        rythmBattleScript = GameObject.FindGameObjectWithTag("RythmBattle").GetComponent<RythmBattle>();

        _playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Audio>();
    }

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
        // play attack's sound
        _playerAudio.playChordSound();

        //shoot the fireball towards cursor's direction
        GameObject fireball = Instantiate(fireballPrefab, gameObject.transform.position, Quaternion.identity);
        fireball.gameObject.GetComponentInChildren<PlayerFireball_Attack>().setDamageAndRythm(_currentDamage, rythmFlag);
    }
}
