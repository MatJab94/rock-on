using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball_Health : MonoBehaviour
{
    // this object's animator
    private Animator _anim;

    // this Object's SpriteRenderer and Transform
    private SpriteRenderer _sr;
    private Transform _tf;

    // the sprite of the enemy, based on it's color
    private Sprite _currentSprite;

    // randomized on spawn, determines enemy's color
    private int _colorIndex;

    // current player's color, from it's Color_Change script
    private Player_Color_Change _playerColor;

    // Rythm Battle flag for bonuses and stuff
    private RythmBattle rythmBattle;

    // collider of the fireball
    public CircleCollider2D attackCollider;

    // Use this for initialization
    void Start()
    {
        // initialise variables
        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();
        rythmBattle = GameObject.FindGameObjectWithTag("RythmBattle").GetComponent<RythmBattle>();
        _sr = GetComponent<SpriteRenderer>();
        _tf = GetComponent<Transform>();
        _anim = GetComponent<Animator>();

        // spawn the fireball with random health and color
        spawnFireball();
    }

    // apply damage to this fireball
    public void applyDamage()
    {
        StartCoroutine(fadeFireball());
        attackCollider.enabled = false;
        StartCoroutine(killFireball());
    }

    // spawn an enemy with random stats
    private void spawnFireball()
    {
        _colorIndex = _playerColor.currentColorIndex;

        // update animation color
        _anim.SetInteger("colorIndex", _colorIndex);

        // kills fireball after some time to prevent to many fireballs fired
        StartCoroutine(timedKillFireball());
    }

    // fades enemy after he's hit
    IEnumerator fadeFireball()
    {
        Color c = _sr.color;
        for (float f = 1.0f; f >= 0.25f; f -= 0.05f)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }
        for (float f = 0.25f; f <= 1.0f; f += 0.05f)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }
    }

    // kills fireball when health <= 0
    IEnumerator killFireball()
    {
        // there's some bugs when destroying the object immediately,
        // so I'm moving it somewhere else and killing it after a second
        _tf.position = new Vector3(-100000, -100000, -100000);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject, 0.5f);
    }

    // kills fireball after some time to prevent to many fireballs fired
    IEnumerator timedKillFireball()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(killFireball());
    }
}
