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

        // initially player gets half of maximum amount of mana
        _mana = _maxMana / 2;
        updateGUI();
    }

    // add mana (ie. when player gets a combo)
    public void addMana(int amount)
    {
        if (_mana < _maxMana)
        {
            _mana += amount;

            if(_mana > _maxMana)
            {
                _mana = _maxMana;
            }

            updateGUI();
        }
    }

    // take away mana (ie. when player uses AoE attack)
    public void subtractMana()
    {
        if (_mana > 0)
        {
            _mana--;
            updateGUI();
        }
    }

    // public methods for other scripts
    public int getMana()
    {
        return _mana;
    }

    public int getMaxMana()
    {
        return _maxMana;
    }

    public void updateGUI()
    {
        _manaGUI.sprite = sprites[_mana];
    }
}
