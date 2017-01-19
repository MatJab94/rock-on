using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag_Health : MonoBehaviour
{
    // this object's animator
    private Animator _anim;

    // this Object's SpriteRenderer and Transform
    private SpriteRenderer _sr;

    // the sprite of the enemy, based on it's color
    private Sprite _currentSprite;

    // randomized on spawn, determines enemy's colors
    private int _primaryColorIndex;
    private int _secondaryColorIndex;

    // current health of the enemy
    private int _health;

    // maximum allowed health for the enemy
    private int _maxHealth;

    // current player's color, from it's Color_Change script
    private Player_Color_Change _playerColor;

    // Rythm Battle flag for bonuses and stuff
    private RythmBattle rythmBattle;

    // you can specify what color it spawns with in the Inspector
    // 0 = red, 1 = green, 2 = blue, anything else = random
    public int spawnColor;

    // to get the flag _isPickActive
    private Player_Regular_Attack _playerAttackScript;

    // to push back enemy when pick is active
    private Mag_Movement _magMoveScript;

    private AudioSource _audioSource; // this gameObject's audio source

    // stuff for making corpses
    public GameObject corpsePrefab;
    public Sprite[] corpseSprites;

    // Use this for initialization
    void Start()
    {
        // initialise variables
        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();
        _playerAttackScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Regular_Attack>();
        rythmBattle = GameObject.FindGameObjectWithTag("RythmBattle").GetComponent<RythmBattle>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _magMoveScript = GetComponent<Mag_Movement>();

        // max health for Mag is 2
        _maxHealth = 2;

        _audioSource = gameObject.GetComponent<AudioSource>();

        // spawn the enemy with random health and color
        spawnEnemy();
    }


    // called when player attacks the enemy
    public void applyDamage(int damage, bool ignoreColor)
    {
        // if Player's and Mag's color match
        if ((_playerColor.currentColorIndex == _primaryColorIndex) || (_playerColor.currentColorIndex == _secondaryColorIndex) || ignoreColor)
        {
            // add bonus if enemy was hit in rythm
            if (rythmBattle.rythmFlag == true)
            {
                rythmBattle.addBonus();
            }
            if (rythmBattle.rythmFlag == false)
            {
                rythmBattle.addReprimand();
            }

            // -1 HP
            _health -= damage;

            //if pick is active push back the enemy
            if (_playerAttackScript.getIsPickActive())
            {
                _magMoveScript.pushBack();
            }

            // fades enemy after he's hit
            StartCoroutine(fadeEnemy());

            // if it's dead destroy the object
            if (_health <= 0)
            {
                _audioSource.Play(); //play dying sound
                StartCoroutine(killEnemy());
            }
        }
        else
        {
            // if Player's and Demon's color don't match restart bonus
            rythmBattle.resetBonus();
            rythmBattle.addReprimand();
        }
    }


    // spawn an enemy with random stats
    private void spawnEnemy()
    {
        _health = _maxHealth;

        switch (spawnColor)
        {
            case 0:
            case 1:
            case 2:
                //spawn enemy based on spawnColor
                _primaryColorIndex = spawnColor;
                break;
            default:
                //spawn random enemy
                _primaryColorIndex = Random.Range(0, 3);
                break;
        }

        // secondary color of the Mage, it's just the next one in array
        _secondaryColorIndex = (_primaryColorIndex + 1) % 3;

        // update animation color
        _anim.SetInteger("colorIndex", _primaryColorIndex);
    }

    // fades enemy after he's hit
    IEnumerator fadeEnemy()
    {
        Color c = Color.white;
        for (float f = 1.0f; f >= 0.25f; f -= Time.deltaTime * 7)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }
        for (float f = 0.25f; f <= 1.0f; f += Time.deltaTime * 7)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }

        // restart to default color at the end
        _sr.color = Color.white;
    }

    // kills enemy when health <= 0
    IEnumerator killEnemy()
    {
        GameObject corpse = Instantiate(corpsePrefab, gameObject.transform.position, Quaternion.identity);
        corpse.GetComponent<SpriteRenderer>().sprite = corpseSprites[_primaryColorIndex];
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

}
