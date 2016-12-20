using UnityEngine;

public class Player_Mana : MonoBehaviour
{

    // arrays that hold sprites of the mana, set in Inspector
    public Sprite[] sprites;

    private SpriteRenderer _manaGUI;

    // the sprite of the mana bar
    private Sprite _currentSprite;
    private int _mana;

    // maximum allowed mana
    private const int _maxMana = 8;

    void Start()
    {
        _manaGUI = GameObject.FindGameObjectWithTag("GUI_Mana").GetComponent<SpriteRenderer>();
        _mana = 7;
        _manaGUI.sprite = sprites[_mana];
    }

    // add mana (ie. when player gets a combo)
    public void addMana()
    {
        if (_mana < _maxMana)
        {
            _mana++;
            _manaGUI.sprite = sprites[_mana];
        }
    }

    // take away mana (ie. when player uses AoE attack)
    public void subtractMana()
    {
        if (_mana > 0)
        {
            _mana--;
            _manaGUI.sprite = sprites[_mana];
        }
    }

    // public method for other scripts to get current value of _mana
    public int getMana()
    {
        return _mana;
    }
}
