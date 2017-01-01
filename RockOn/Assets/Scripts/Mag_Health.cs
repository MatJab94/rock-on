using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag_Health : MonoBehaviour
{
    // sprites of the enemies, set in Inspector
    public Sprite spriteRed;
    public Sprite spriteGreen;
    public Sprite spriteBlue;

    // this Object's SpriteRenderer and Transform
    private SpriteRenderer _sr;
    private Transform _tf;

    // the sprite of the enemy, based on it's color
    private Sprite _currentSprite;

    // randomized on spawn, determines enemy's color
    private int _currentColorIndex;

    // current health of the enemy
    private int _health;

    // maximum allowed health for the enemy
    private int _maxHealth;

    // current player's color, from it's Color_Change script
    private Player_Color_Change _playerColor;

    // Rythm Battle flag for bonuses and stuff
    private RythmBattle rythmBattle;

    // Use this for initialization
    void Start()
    {
        // initialise variables
        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();
        rythmBattle = GameObject.FindGameObjectWithTag("RythmBattle").GetComponent<RythmBattle>();
        _sr = GetComponent<SpriteRenderer>();
        _tf = GetComponent<Transform>();

        // max health for Mag is 2
        _maxHealth = 2;

        // spawn the enemy with random health and color
        spawnEnemy();
    }


    // called when player attacks the enemy
    public void applyDamage(int damage)
    {
        // if Player's and Mag's color match
        if (_playerColor.currentColorIndex == _currentColorIndex)
        {
            // add bonus if enemy was hit in rythm
            if (rythmBattle.rythmFlag == true)
            {
                rythmBattle.addBonus();
            }
            else
            {
                rythmBattle.resetBonus();
            }

            // -1 HP
            _health -= damage;

            // fades enemy after he's hit
            StartCoroutine("fadeEnemy");

            // if it's dead destroy the object
            if (_health <= 0)
            {
                // there's a bug when destroying the enemy immediately, so I'm 
                // moving him somewhere else and killing him after a second
                _tf.position = new Vector3(1000, 1000, 1000);
                StartCoroutine("killEnemy");
                // spawnEnemy();
            }
        }
        else
        {
            // if Player's and Demon's color don't match restart bonus
            rythmBattle.resetBonus();
        }
    }


    // spawn an enemy with random stats
    private void spawnEnemy()
    {
        _health = _maxHealth;

        // initial color is random
        _currentColorIndex = Random.Range(0, 300) % 3; // 0 = red, 1 = green, 2 = blue

        // update sprite
        switch (_currentColorIndex)
        {
            case 0: // red
                _sr.sprite = spriteRed;
                break;
            case 1: // green
                _sr.sprite = spriteGreen;
                break;
            case 2: // blue
                _sr.sprite = spriteBlue;
                break;
            default:
                Destroy(gameObject); // illegal color number?
                break;
        }
    }

    // fades enemy after he's hit
    IEnumerator fadeEnemy()
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

    // kills enemy when health <= 0
    IEnumerator killEnemy()
    {
        for (float f = 1.0f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }
        Destroy(gameObject);
    }

}
