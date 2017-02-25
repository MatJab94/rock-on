using UnityEngine;
using System.Collections;
using System;

/*
 * Script attached to "AoE_Attack_Range" (child of Player).
 * It has a circle collider with "Is Trigger" checked.
 * 
 * Once an object with RigidBody2D collides with it,
 * the OnTrigger methods are called.
 */

public class Player_AoE_Attack : MonoBehaviour
{
    // sprites with range "animation", set in Inspector
    public Sprite[] _sprites;

    // list that contains all enemies (gameObjects) in range
    private ArrayList _targets;

    // this object's sprite renderer (for "animating" the range when attacking)
    private SpriteRenderer _sr;

    // this transform for changing scale ("animating")
    private Transform _tf;

    // current player's color, from it's Color_Change script
    private Player_Color_Change _playerColor;

    // player's audio script for making sounds when attacking
    private Player_Audio _playerAudio;

    // mana bar in GUI
    private Player_Mana _playerMana;

    // script that stops player from continuously attacking
    private Player_AttackTimeOut _timeoutScript;

    // to get value of rythm flag
    private RythmBattle rythmBattleScript;

    // for disabling the attacking on first level [no longer used, but can stay]
    public bool aoeDisabled;

    // scale of the AoE attack
    private float _scaleAoE;

    // damage of the AoE attack
    private int _damageAoE;

    // flags based on amount of mana
    private bool _shakeCamera;
    private bool _damageOtherColors;
    private bool _pushBack;

    // camera shake script
    private Camera_Shake _camShake;

    // is microphpne power-up active?
    private bool _micActive;

    public void Start()
    {
        _targets = new ArrayList();

        _sr = GetComponent<SpriteRenderer>();
        _tf = GetComponent<Transform>();
        _tf.localScale = Vector3.zero;

        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();
        _playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Audio>();
        _playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Mana>();
        _timeoutScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_AttackTimeOut>();
        _camShake = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera_Shake>();
        rythmBattleScript = GameObject.FindGameObjectWithTag("RythmBattle").GetComponent<RythmBattle>();

        _scaleAoE = 1;
        _damageAoE = 1;
        _shakeCamera = false;
        _damageOtherColors = false;
        _pushBack = false;
        
        _micActive = false;

        // no longer used, but can stay
        aoeDisabled = false;
    }

    public void Update()
    {
        // if AoE Attack is pressed (Space)
        if (Input.GetButtonDown("Attack_AoE") && _timeoutScript.getTimeoutFlag() == false && !aoeDisabled)
        {
            // do the AoE attack
            aoeAttack(rythmBattleScript.getRythmFlag());

            // start timeout after attacking
            _timeoutScript.startTimeout();
        }
    }

    private void aoeAttack(bool rythmFlag)
    {
        // attack only if player has mana
        if (_playerMana.getMana() > 0)
        {
            // set damage, range and so on, based on amount of mana
            setAttackStats();

            // change sprite of the range to match chosen color
            updateSprite();

            // subtract mana for attack
            _playerMana.subtractMana();

            // play attack's sound
            _playerAudio.playChordSound();

            // animates the range while attacking and applies damage to targets
            StartCoroutine(animateAndAttack(rythmFlag));
        }
    }

    private void setAttackStats()
    {
        // 'awesomness' of AoE attack depends on amount of mana player has
        switch (_playerMana.getMana())
        {
            case 1:
                _scaleAoE = 1.5f;
                _damageAoE = 1;
                _shakeCamera = false;
                _damageOtherColors = false;
                _pushBack = false;
                break;
            case 2:
                _scaleAoE = 2;
                _damageAoE = 1;
                _shakeCamera = false;
                _damageOtherColors = false;
                _pushBack = false;
                break;
            case 3:
                _scaleAoE = 2.5f;
                _damageAoE = 2;
                _shakeCamera = true;
                _damageOtherColors = false;
                _pushBack = false;
                break;
            case 4:
                _scaleAoE = 3;
                _damageAoE = 2;
                _shakeCamera = true;
                _damageOtherColors = false;
                _pushBack = true;
                break;
            case 5:
                _scaleAoE = 3;
                _damageAoE = 3;
                _shakeCamera = true;
                _damageOtherColors = true;
                _pushBack = true;
                break;
            default:
                _scaleAoE = 0;
                _damageAoE = 0;
                _shakeCamera = false;
                _damageOtherColors = false;
                _pushBack = false;
                break;
        }
        
        // better AoE when Microphone power-up is active
        if (_micActive)
        {
            _damageAoE++;
            _scaleAoE++;
            _pushBack = true;
            _damageOtherColors = true;
            _shakeCamera = true;
        }
    }

    private void updateSprite()
    {
        if (_damageOtherColors)
        {
            _sr.sprite = _sprites[3]; // awesome range when ignoring color
        }
        else
        {
            _sr.sprite = _sprites[_playerColor.currentColorIndex];
        }
    }

    private void attackTargets(bool rythmFlag)
    {
        foreach (GameObject target in _targets)
        {
            if (target.tag == "Demon")
            {
                if (_pushBack)
                {
                    target.GetComponent<Demon_Movement>().pushBack();
                }
                target.GetComponent<Demon_Health>().applyDamage(_damageAoE, false, _damageOtherColors, rythmFlag);
            }
            if (target.tag == "Mag")
            {
                if (_pushBack)
                {
                    target.GetComponent<Mag_Movement>().pushBack();
                }
                target.GetComponent<Mag_Health>().applyDamage(_damageAoE, false, _damageOtherColors);
            }
            if (target.tag == "Fireball")
            {
                target.GetComponent<Fireball_Health>().applyDamage(_damageAoE, false, _damageOtherColors);
            }
        }
    }

    // highlights the collider while attacking
    IEnumerator animateAndAttack(bool rythmFlag)
    {
        // setting starting values of color and scale
        Color c = Color.white;
        Vector3 scale = Vector3.zero;
        _sr.color = c;
        _tf.localScale = scale;

        if (_shakeCamera)
        {
            _camShake.shakeCamera();
        }

        // scales the range UP
        for (float f = 0.0f; f <= _scaleAoE; f += Time.deltaTime * 8)
        {
            scale.x = f;
            scale.y = f;
            _tf.localScale = scale;
            yield return null;
        }

        // apply damage to every enemy in range after range's scale is maximum
        attackTargets(rythmFlag);

        // fades range to transparent
        for (float f = 1.0f; f >= 0.0f; f -= Time.deltaTime * 7)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }

        c.a = 1.0f;
        _sr.color = c;

        _tf.localScale = Vector3.zero;
    }

    public void setMicActive(bool micActive)
    {
        _micActive = micActive;
    }

    // event that is called if enemy enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Demon" || collision.gameObject.tag == "Mag" || collision.gameObject.tag == "Fireball")
        {
            // add this object to the list of enemies in range
            _targets.Add(collision.gameObject);
        }
    }

    // event that is called if enemy exits this Object's collider (is out of range)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Demon" || collision.gameObject.tag == "Mag" || collision.gameObject.tag == "Fireball")
        {
            // remove this object from the list of enemies in range
            _targets.Remove(collision.gameObject);
        }
    }
}
