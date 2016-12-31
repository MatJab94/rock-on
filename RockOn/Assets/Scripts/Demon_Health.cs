using UnityEngine;
using System.Collections;

public class Demon_Health : MonoBehaviour
{
    // arrays that hold sprites of the enemies, set in Inspector
    public Sprite[] spriteRed;
    public Sprite[] spriteGreen;
    public Sprite[] spriteBlue;

    // this Object's SpriteRenderer and Transform
    private SpriteRenderer _sr;
    private Transform _tf;

    // the sprite of the enemy, based on it's color and health
    private int _currentColorIndex;
    private Sprite _currentSprite;
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

        // max health is equal to amount of sprites
        _maxHealth = spriteRed.Length;

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
            if (_health < 0)
            {
                // there's a bug when destroying the enemy immediately, so I'm 
                // moving him somewhere else and killing him after a second
                _tf.position = new Vector3(1000, 1000, 1000);
                StartCoroutine("killEnemy");
                // spawnEnemy();
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

    // updates Demon's sprite based on current health and color
    // health is also an index in the sprite arrays (0-2)
    private void changeForm()
    {
        switch (_currentColorIndex)
        {
            case 0: // red
                _sr.sprite = spriteRed[_health];
                break;
            case 1: // green
                _sr.sprite = spriteGreen[_health];
                break;
            case 2: // blue
                _sr.sprite = spriteBlue[_health];
                break;
            default:
                Destroy(gameObject); // illegal color number?
                break;
        }
    }

    // spawn an enemy with random stats
    private void spawnEnemy()
    {
        _tf.position = _respawnPosition;

        // initial health is random
        //_health = Random.Range(0, _maxHealth); // 0, 1, 2
        _health = _maxHealth-1;

        // initial color is random
        _currentColorIndex = Random.Range(0, 300)%3; // 0 = red, 1 = green, 2 = blue

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
    }

    // kills enemy when HP<0
    IEnumerator killEnemy()
    {
        for (float f = 1.0f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }
        Destroy(gameObject);
    }

}
