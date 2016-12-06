using UnityEngine;
using System.Collections;

public class Enemy_Health : MonoBehaviour
{

    public Sprite[] spriteRed;
    public Sprite[] spriteGreen;
    public Sprite[] spriteBlue;

    private SpriteRenderer _sr;
    private Transform _tf;
    private int _currentColorIndex;
    private Sprite _currentSprite;
    private int _health;
    private const int _maxHealth = 5;
    private Color_Change _playerColor;
    private Vector3 _respawnPosition;

    void Start()
    {
        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Color_Change>();
        _sr = GetComponent<SpriteRenderer>();
        _tf = GetComponent<Transform>();
        _respawnPosition = new Vector3(_tf.position.x, _tf.position.y, _tf.position.z);
        respawnEnemy();
    }

    public void applyDamage()
    {
        if (_playerColor.currentColorIndex == _currentColorIndex)
        {
            _health--;
            if (_health < 0)
            {
                respawnEnemy();
            }
            else
            {
                _currentColorIndex = Random.Range(0, 3);
                changeForm();
            }
        }
    }

    private void changeForm()
    {
        switch (_currentColorIndex)
        {
            case 0:
                _sr.sprite = spriteRed[_health];
                break;
            case 1:
                _sr.sprite = spriteGreen[_health];
                break;
            case 2:
                _sr.sprite = spriteBlue[_health];
                break;
            default:
                Destroy(gameObject); // Something went wrong?
                break;
        }
    }

    private void respawnEnemy()
    {
        _tf.position = _respawnPosition;
        _health = Random.Range(0, _maxHealth); // 0, 1, 2, 3, 4
        _currentColorIndex = Random.Range(0, 3); // 0 = red, 1 = green, 2 = blue
        changeForm();
    }
}
