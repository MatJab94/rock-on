using UnityEngine;
using System.Collections;

public class Enemy_Health : MonoBehaviour
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
    private const int _maxHealth = 5;

    // current player's color, from it's Color_Change script
    private Color_Change _playerColor;

    // used to respawn enemies, just for testing
    private Vector3 _respawnPosition;

    void Start()
    {
        // initialise variables
        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Color_Change>();
        _sr = GetComponent<SpriteRenderer>();
        _tf = GetComponent<Transform>();
        _respawnPosition = new Vector3(_tf.position.x, _tf.position.y, _tf.position.z);

        // spawn the enemy with random health and color
        spawnEnemy();
    }


    // called when player attacks the Demon
    public void applyDamage()
    {
        // if Player's and Demon's color match
        if (_playerColor.currentColorIndex == _currentColorIndex)
        {
            // -1 HP
            _health--;

            // fades enemy after he's hit
            StartCoroutine("fadeEnemy");

            // if it's dead respawn it (just for testing, use Destroy(gameObject) to kill it)
            if (_health < 0)
            {
                spawnEnemy();
            }
            // if not dead randomly change it's color and update sprite
            else
            {
                _currentColorIndex = Random.Range(0, 3);
                changeForm();
            }
        }
    }

    // updates Demon's sprite based on current health and color
    // health is also an index in the sprite arrays (0-4)
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
        _health = Random.Range(0, _maxHealth); // 0, 1, 2, 3, 4
        _currentColorIndex = Random.Range(0, 3); // 0 = red, 1 = green, 2 = blue
        changeForm();
    }

    // fades enemy after he's hit
    IEnumerator fadeEnemy()
    {
        Color c = _sr.color;
        for (float f = 1f; f >= 0.25; f -= 0.05f)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }
        for (float f = 0.25f; f <= 1; f += 0.05f)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }
    }

}
