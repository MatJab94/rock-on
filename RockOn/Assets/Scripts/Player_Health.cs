using UnityEngine;
using System.Collections;

public class Player_Health : MonoBehaviour
{
    // array that holds sprites of Health Bar, set in Inspector
    public Sprite[] sprites;

    // Player's SpriteRenderer and Transform, set in Inspector
    private SpriteRenderer _sr;
    private Transform _tf;

    // Health Bar's SpriteRenderer
    private SpriteRenderer _healthGUI;

    // Player's health
    private int _health;

    // maximum allowed health for Player
    private int _maxHealth;

    // how long is player invincible after he's hit
    private float _invincible;
    private bool _invincibleFlag;

    // used to respawn enemies, just for testing
    private Vector3 _respawnPosition;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _tf = GetComponent<Transform>();

        _respawnPosition = new Vector3(_tf.position.x, _tf.position.y, _tf.position.z);
        _healthGUI = GameObject.FindGameObjectWithTag("GUI_Health").GetComponent<SpriteRenderer>();

        _maxHealth = sprites.Length;

        _invincible = 1.0f;
        _invincibleFlag = false;

        spawnPlayer();
    }


    // called when enemy attacks the player
    public void applyDamage()
    {
        if (!_invincibleFlag)
        {
            // -1 HP
            _health--;

            // make player invincible for a moment, so enemies can't kill him instantly
            StartCoroutine("invincibleTime");

            // fades enemy after he's hit
            StartCoroutine("fadeEnemy");

            // if it's dead respawn it (just for testing, use Destroy(gameObject) to kill it)
            if (_health <= 0)
            {
                spawnPlayer();
            }
            // if not dead randomly change it's color and update sprite
            else
            {
                // Debug.Log("Player: " + _health + " HP");
                _healthGUI.sprite = sprites[_health - 1];
            }
        }
    }

    // respawn the player after death or spawn him at the beginning of the game
    private void spawnPlayer()
    {
        _tf.position = _respawnPosition;
        _health = _maxHealth;
        _healthGUI.sprite = sprites[_health - 1];

    }

    // timer counts down after player's hit, during this time player is invincible
    IEnumerator invincibleTime()
    {
        _invincibleFlag = true;
        for (float time = _invincible; time > 0; time -= Time.deltaTime)
        {
            yield return null;
        }
        _invincibleFlag = false;
    }

    // fades the player after he's hit
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
