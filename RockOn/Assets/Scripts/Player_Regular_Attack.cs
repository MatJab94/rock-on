using System.Collections;
using UnityEngine;

public class Player_Regular_Attack : MonoBehaviour
{
    // a flag used to make attack trigger only once per click
    private bool _isAttacking = false;

    // targets in range
    private ArrayList _targetsLeft;
    private ArrayList _targetsRight;
    private ArrayList _targetsUp;
    private ArrayList _targetsDown;

    // sprites of the attacks
    private SpriteRenderer _left;
    private SpriteRenderer _right;
    private SpriteRenderer _up;
    private SpriteRenderer _down;

    // Rythm Battle flag for bonuses and stuff
    private RythmBattle rythmBattle;

    public void Start()
    {
        _targetsLeft = GameObject.Find("Regular_Attack_Left").GetComponent<Regular_Attack_Collider>().targets;
        _targetsRight = GameObject.Find("Regular_Attack_Right").GetComponent<Regular_Attack_Collider>().targets;
        _targetsUp = GameObject.Find("Regular_Attack_Up").GetComponent<Regular_Attack_Collider>().targets;
        _targetsDown = GameObject.Find("Regular_Attack_Down").GetComponent<Regular_Attack_Collider>().targets;

        _left = GameObject.Find("Regular_Attack_Left").GetComponent<SpriteRenderer>();
        _right = GameObject.Find("Regular_Attack_Right").GetComponent<SpriteRenderer>();
        _up = GameObject.Find("Regular_Attack_Up").GetComponent<SpriteRenderer>();
        _down = GameObject.Find("Regular_Attack_Down").GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        // if Attack is pressed (Left/Right/Up/Down arrow)
        if (Input.GetAxisRaw("Attack_Horizontal") != 0 || Input.GetAxisRaw("Attack_Vertical") != 0)
        {
            if (_isAttacking == false)
            {
                // changing the flag so this code runs only once per click
                _isAttacking = true;

                // attack enemies in range
                attackEnemies();
            }
        }
        // if Attack is no longer pressed
        if (Input.GetAxisRaw("Attack_Horizontal") == 0 && Input.GetAxisRaw("Attack_Vertical") == 0)
        {
            // changing the flag so the attack can be triggered on next click
            _isAttacking = false;
        }
    }

    private void attackEnemies()
    {
        if (Input.GetAxisRaw("Attack_Horizontal") < 0)
        {
            StartCoroutine("highlightCollider", _left);
            foreach (GameObject target in _targetsLeft)
            {
                target.GetComponent<Enemy_Health>().applyDamage();
            }
        }
        if (Input.GetAxisRaw("Attack_Horizontal") > 0)
        {
            StartCoroutine("highlightCollider", _right);
            foreach (GameObject target in _targetsRight)
            {
                target.GetComponent<Enemy_Health>().applyDamage();
            }
        }
        if (Input.GetAxisRaw("Attack_Vertical") < 0)
        {
            StartCoroutine("highlightCollider", _down);
            foreach (GameObject target in _targetsDown)
            {
                target.GetComponent<Enemy_Health>().applyDamage();
            }
        }
        if (Input.GetAxisRaw("Attack_Vertical") > 0)
        {
            StartCoroutine("highlightCollider", _up);
            foreach (GameObject target in _targetsUp)
            {
                target.GetComponent<Enemy_Health>().applyDamage();
            }
        }
    }

    // highlights the collider while attacking
    IEnumerator highlightCollider(SpriteRenderer sr)
    {
        Color c = sr.color;
        for (float f = 0.2f; f <= 0.75f; f += 0.05f)
        {
            c.a = f;
            sr.color = c;
            yield return null;
        }
        for (float f = 0.75f; f >= 0.2; f -= 0.05f)
        {
            c.a = f;
            sr.color = c;
            yield return null;
        }
    }
}
