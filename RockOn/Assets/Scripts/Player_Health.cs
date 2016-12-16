using UnityEngine;
using System.Collections;

public class Player_Health : MonoBehaviour
{
    // arrays that hold sprites of the enemies, set in Inspector
    public Sprite[] sprites;

    // Player's SpriteRenderer and Transform, set in Inspector
    public SpriteRenderer sr;
    public Transform tf;

    private SpriteRenderer _healthSR;

    // the sprite of the enemy, based on it's color and health
    private Sprite _currentSprite;
    private int _health;

    // maximum allowed health for Demon
    private const int _maxHealth = 2;

    private const float _invincible = 1;
    private bool _invincibleFlag = false;

    // used to respawn enemies, just for testing
    private Vector3 _respawnPosition;

    void Start()
    {
        _respawnPosition = new Vector3(tf.position.x, tf.position.y, tf.position.z);
        _healthSR = gameObject.GetComponent<SpriteRenderer>();
        // spawn the enemy with random health and color
        spawnPlayer();
    }


    // called when player attacks the Demon
    public void applyDamage()
    {
        if (!_invincibleFlag)
        {
            // -1 HP
            _health--;

            StartCoroutine("invincibleTime");

            // fades enemy after he's hit
            StartCoroutine("fadeEnemy");

            // if it's dead respawn it (just for testing, use Destroy(gameObject) to kill it)
            if (_health < 0)
            {
                spawnPlayer();
            }
            // if not dead randomly change it's color and update sprite
            else
            {
                Debug.Log("Player: " + _health + " HP");
                _healthSR.sprite = sprites[_health];
            } 
        }
        
    }

   

    // spawn an enemy with random stats
    private void spawnPlayer()
    {
        tf.position = _respawnPosition;
        _health = _maxHealth;
        _healthSR.sprite = sprites[_health];

    }

    IEnumerator invincibleTime()
    {
        _invincibleFlag = true;
        for (float time = _invincible; time>0; time -= Time.deltaTime)
        {
            yield return null;
        }
        _invincibleFlag = false;
    }

    // fades enemy after he's hit
    IEnumerator fadeEnemy()
    {
        Color c = sr.color;
        for (float f = 1f; f >= 0.25; f -= 0.05f)
        {
            c.a = f;
            sr.color = c;
            yield return null;
        }
        for (float f = 0.25f; f <= 1; f += 0.05f)
        {
            c.a = f;
            sr.color = c;
            yield return null;
        }
    }

}
