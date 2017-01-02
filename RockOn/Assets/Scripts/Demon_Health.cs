using UnityEngine;
using System.Collections;

public class Demon_Health : MonoBehaviour
{
    // this object's animator
    private Animator _anim;

    // this Object's SpriteRenderer and Transform
    private SpriteRenderer _sr;
    private Transform _tf;

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

    // used to respawn enemies, just for testing
    private Vector3 _respawnPosition;

    // Rythm Battle flag for bonuses and stuff
    private RythmBattle rythmBattle;

    void Start()
    {
        // initialise variables
        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();
        rythmBattle = GameObject.FindGameObjectWithTag("RythmBattle").GetComponent<RythmBattle>();
        _sr = GetComponent<SpriteRenderer>();
        _tf = GetComponent<Transform>();
        _respawnPosition = new Vector3(_tf.position.x, _tf.position.y, _tf.position.z);

        _anim = GetComponent<Animator>();

        // max health for Demon is 3
        _maxHealth = 3;

        // spawn the enemy with random health and color
        spawnEnemy();
    }


    // called when player attacks the Demon
    public void applyDamage(int damage)
    {
        // if Player's and Demon's color match
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
                StartCoroutine("killEnemy");
            }
            // if not dead just update sprite
            else
            {
                changeForm();
            }
        }
        else
        {
            // if Player's and Demon's color don't match restart bonus
            rythmBattle.resetBonus();
        }
    }

    
    private void changeForm()
    {
        // update animation form based on health
        _anim.SetInteger("form", _health);
    }

    // spawn an enemy with random stats
    private void spawnEnemy()
    {
        _tf.position = _respawnPosition;
        
        // initial health
        _health = _maxHealth;

        // initial color is random
        _currentColorIndex = Random.Range(0, 300) % 3; // 0 = red, 1 = green, 2 = blue

        // update animation color
        _anim.SetInteger("colorIndex", _currentColorIndex);

        // update sprite
        changeForm();
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

        // restart to default color at the end
        _sr.color = Color.white;
    }

    // kills enemy when HP<0
    IEnumerator killEnemy()
    {
        // small delay before killing the object
        for (float f = 0.2f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }

        // there's some bugs when destroying the object immediately,
        // so I'm moving it somewhere else and killing it after a second
        _tf.position = new Vector3(-10000, -10000, -10000);

        for (float f = 1.0f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }
        Destroy(gameObject);
    }

}
