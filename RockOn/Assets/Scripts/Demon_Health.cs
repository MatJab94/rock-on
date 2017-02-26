using UnityEngine;
using System.Collections;

public class Demon_Health : MonoBehaviour
{
    // this object's animator
    private Animator _anim;

    // this Object's SpriteRenderer and Transform
    private SpriteRenderer _sr;

    // the sprite of the enemy, based on it's color and health
    private Sprite _currentSprite;

    // randomized on spawn, determines enemy's color
    private int _currentColorIndex;

    // current health of the enemy
    private int _health;

    // maximum allowed health for Demon
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
    private Demon_Movement _demonMoveScript;

    private AudioSource _audioSource; // this gameObject's audio source

    // stuff for making corpses
    public GameObject corpsePrefab;
    public Sprite[] corpseSprites;

    void Start()
    {
        // initialise variables
        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();
        _playerAttackScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Regular_Attack>();
        rythmBattle = GameObject.FindGameObjectWithTag("RythmBattle").GetComponent<RythmBattle>();
        _sr = GetComponent<SpriteRenderer>();
        _demonMoveScript = GetComponent<Demon_Movement>();

        _anim = GetComponent<Animator>();

        _audioSource = gameObject.GetComponent<AudioSource>();


        // max health for Demon is 3
        _maxHealth = 3;

        // spawn the enemy with random health and color
        spawnEnemy();
    }


    // called when player attacks the Demon
    public void applyDamage(int damage, bool ignoreColor, bool damageOtherColors, bool rythmFlag)
    {
        // if Player's and Demon's color match
        if (_playerColor.currentColorIndex == _currentColorIndex || ignoreColor || damageOtherColors)
        {
            if (_playerColor.currentColorIndex == _currentColorIndex || ignoreColor)
            {
                // subtract damage
                _health -= damage;
            }
            else
            {
                if (damageOtherColors)
                {
                    // subtract damage
                    _health--;
                }
            }

            // add bonus if enemy was hit in rythm
            if (rythmFlag)
            {
                rythmBattle.addBonus();
            }
            else
            {
                if (_health != 0)
                {
                    rythmBattle.addReprimand();
                }
            }

            //if pick is active push back the enemy
            if (_playerAttackScript.getPickActive())
            {
                _demonMoveScript.pushBack();
            }

            // fades enemy after he's hit
            StartCoroutine(fadeEnemy());

            // if it's dead destroy the object
            if (_health <= 0)
            {
                rythmBattle.addPraise();
                _audioSource.pitch = 0.90f;
                _audioSource.Play(); //play dying sound
                StartCoroutine(killEnemy());
            }
            // if not dead just update sprite
            else
            {
                // update animation form
                _anim.SetTrigger("applyDamage");
                _audioSource.pitch = 1;
                _audioSource.Play(); // play damage sound
            }
        }
        else
        {
            // if Player's and Demon's color don't match restart bonus
            // rythmBattle.resetBonus();  // not needed anymore
            rythmBattle.addSpecialReprimand();
        }
    }

    // spawn an enemy
    private void spawnEnemy()
    {
        // initial health
        _health = _maxHealth;

        switch (spawnColor)
        {
            case 0:
            case 1:
            case 2:
                //spawn enemy based on spawnColor
                _currentColorIndex = spawnColor;
                break;
            default:
                //spawn random enemy
                _currentColorIndex = Random.Range(0, 3);
                break;
        }

        // update animation color
        _anim.SetInteger("colorIndex", _currentColorIndex);
    }

    // fades enemy after he's hit
    IEnumerator fadeEnemy()
    {
        Color c = _sr.color;
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

    // kills enemy when HP<0
    IEnumerator killEnemy()
    {
        GameObject corpse = Instantiate(corpsePrefab, gameObject.transform.position, Quaternion.identity);
        corpse.GetComponent<SpriteRenderer>().sprite = corpseSprites[_currentColorIndex];
        yield return new WaitForEndOfFrame();
        gameObject.transform.position = new Vector3(-10000, -10000, -10000);
        Destroy(gameObject, 0.5f);
    }

}
