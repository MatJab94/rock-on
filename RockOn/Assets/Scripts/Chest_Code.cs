using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_Code : MonoBehaviour
{
    // 3 SpriteRenderers of the code elements that open the chest
    public SpriteRenderer[] keys;

    // sprites of the code elements
    public Sprite[] sprites;

    // grey sprite for the code elements
    public Sprite defaultSprite;

    // code elements values (0, 1, 2 = R, G, B)
    private int[] _chestCode = { 0, 0, 0 };

    // index of the current code element
    private int _currentIndex;

    // current attack color chosen by the player
    private Player_Color_Change _playerColor;

    // Use this for initialization
    void Start()
    {
        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();

        _currentIndex = 0;

        // randomly choose a code for the chest
        for (int i = 0; i < _chestCode.Length; i++)
        {
            _chestCode[i] = Random.Range(0, 300) % 3; // 0 = red, 1 = green, 2 = blue
        }

    }

    public void checkCurrentKey()
    {
        keys[_currentIndex].sprite = sprites[_playerColor.currentColorIndex];
        nextKey();
    }

    private void checkCode()
    {
        for (int i = 0; i < _chestCode.Length; i++)
        {
            keys[i].sprite = defaultSprite;
        }
    }

    private void nextKey()
    {
        _currentIndex += 1;
        if(_currentIndex >= 3)
        {
            _currentIndex = 0;
            checkCode();
        }
    }
}
