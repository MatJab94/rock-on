using System.Collections;
using UnityEngine;

public class Player_Regular_Attack : MonoBehaviour
{
    // player's transform for drawing line
    private Transform _playerTransform;

    // target detection script from cursor
    private Cursor_TargetDetection _targetDetection;

    // currently chosen target
    private GameObject _target;

    // max range at which target can be interacted with
    public float maxRange;

    // player's audio script for making sounds when attacking
    private Player_Audio _playerAudio;

    // drawing line when attacking
    private LineRenderer _lr;

    // script that stops player from continuously attacking
    private Player_AttackTimeOut _timeoutScript;

    void Start()
    {
        _playerTransform = GetComponentInParent<Transform>();
        _targetDetection = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor_TargetDetection>();
        _playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Audio>();
        _lr = GetComponent<LineRenderer>();
        _timeoutScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_AttackTimeOut>();

        _lr.sortingLayerName = "UI";

        _target = null;
    }

    // Update is called once per frame
    void Update()
    {
        // if player clicks the attack button (mouse 0 by default)
        if (Input.GetButtonDown("Regular_Attack") && _timeoutScript.getTimeoutFlag() == false)
        {
            // select current target
            _target = _targetDetection.getTarget();

            // if there is a target
            if (_target != null)
            {
                // start timeout after attacking
                _timeoutScript.startTimeout();

                // and check if target is in range
                if (isInRange(_target.GetComponent<Transform>().position))
                {
                    // play attack's sound
                    _playerAudio.playChordSound();

                    // draw a line from player to target
                    StartCoroutine("drawLine", (Vector2)_target.GetComponent<Transform>().position);

                    // hit or interact with the target object
                    if (_target.transform.parent.gameObject.tag == "Demon")
                    {
                        _target.GetComponentInParent<Demon_Health>().applyDamage(1);
                    }
                    if (_target.transform.parent.gameObject.tag == "Mag")
                    {
                        _target.GetComponentInParent<Mag_Health>().applyDamage(1);
                    }
                    if (_target.transform.parent.gameObject.tag == "Fireball")
                    {
                        _target.GetComponentInParent<Fireball_Health>().applyDamage(1, false);
                    }
                    if (_target.transform.parent.gameObject.tag == "Chest")
                    {
                        _target.GetComponentInParent<Chest_Open>().hitChest();
                    }
                    if (_target.transform.parent.gameObject.tag == "ClosedDoor")
                    {
                        _target.GetComponentInParent<ClosedDoor_Open>().hitDoor();
                    }
                }
                else
                {
                    // target is not in range
                    // calculate the point in maxRange from player
                    Vector2 pointInRange;
                    pointInRange = _target.GetComponent<Transform>().position - _playerTransform.position;
                    pointInRange = pointInRange.normalized * maxRange;
                    pointInRange = (Vector2)_playerTransform.position + pointInRange;

                    StartCoroutine(drawLine(pointInRange));
                }
            }
        }
    }

    private bool isInRange(Vector2 target)
    {
        // calculate distance between player and target
        float _distance = Vector2.Distance(_playerTransform.position, target);

        if (_distance > maxRange)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // draws a line between player and target when attacking
    IEnumerator drawLine(Vector2 target)
    {
        // set positions of player and target to draw the line
        _lr.SetPosition(0, _playerTransform.position);
        _lr.SetPosition(1, target);

        // how long the line is visible
        yield return new WaitForSeconds(0.1f);

        // set position so the line is not visible
        _lr.SetPosition(0, _playerTransform.position);
        _lr.SetPosition(1, _playerTransform.position);
    }
}