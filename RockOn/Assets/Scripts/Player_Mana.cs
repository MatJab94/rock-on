using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mana : MonoBehaviour {

    // arrays that hold sprites of the mana, set in Inspector
    public Sprite[] sprites;

    private SpriteRenderer _manaSR;

    // the sprite of the mana bar
    private Sprite _currentSprite;
    private int _mana;

    // maximum allowed mana
    private const int _maxMana = 8;

    void Start()
    {
        _manaSR = gameObject.GetComponent<SpriteRenderer>();
        _mana=7;
        _manaSR.sprite = sprites[_mana];
    }

    public void addMana()
    {
        if (_mana < _maxMana)
        {
            _mana++;
            _manaSR.sprite = sprites[_mana];
        }
    }

    public void subtractMana()
    {
        if (_mana > 0)
        {
            _mana--;
            _manaSR.sprite = sprites[_mana];
        }
    }

    public int getMana()
    {
        return _mana;
    }
}
