using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_Code : MonoBehaviour
{
    // SpriteRenderers of the 3 "key" objects that show the code sequence on the chest
    public SpriteRenderer[] keysSR;

    // sprites of the key objects
    public Sprite[] sprites;

    // default grey sprite for the key objects
    public Sprite defaultSprite;

    // the code sequence that opens this chest (0, 1, 2 --> Red, Green, Blue)
    private int[] _chestCode;

    // remembers which keys were guessed correctly to skip them
    private bool[] _playerGuess;

    // true if player guessed entire code sequence
    private bool _guessed;

    // index of the current key object
    private int _currentIndex;

    // current attack color chosen by the player
    private Player_Color_Change _playerColor;

    // script opening the chest
    private Chest_Open _chestScript;

    // SpriteRenderer of the chest for "animations" showing if guess was correct
    private SpriteRenderer _chestSR;

    // Transform of the Highlighter
    public Transform highlighterTF;

    // number of tries
    private int _numOfTries;

    // number of tries at which we open the chest with a bad treasure
    private int _maxNumOfTries;

    void Start()
    {
        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();
        _chestScript = GetComponentInParent<Chest_Open>();
        _chestSR = GetComponentInParent<SpriteRenderer>();

        // create arrays holding the secret code and player's guesses
        _chestCode = new int[3] { -1, -1, -1 };
        _playerGuess = new bool[3] { false, false, false };
        _guessed = false;

        // point to the first key of the code sequence
        _currentIndex = 0;

        _numOfTries = 0;
        _maxNumOfTries = _chestScript.getNumOfTries();

        updateHighlighterPosition();
    }

    // called when player attacks the chest with regular attack - he's trying to guess the code
    public void hitChest()
    {
        _numOfTries++;

        // check if current key was guessed correctly
        if (_chestCode[_currentIndex] == _playerColor.currentColorIndex)
        {
            // remember that player guessed this correctly
            _playerGuess[_currentIndex] = true;

            // change current key object to match the color of player's attack
            keysSR[_currentIndex].sprite = sprites[_playerColor.currentColorIndex];

            // play correct guess "animation"
            StartCoroutine(correctGuess());
        }
        else
        {
            // remember that player guessed this incorrectly
            _playerGuess[_currentIndex] = false;

            // change current key object back to default
            keysSR[_currentIndex].sprite = defaultSprite;

            // play wrong guess "animation"
            StartCoroutine(wrongGuess());

            // if player tried too many times, open the chest with a bad treasure
            if (_numOfTries > _maxNumOfTries)
            {
                // change the flag to prevent infinite looping
                _guessed = true;

                // open the chest
                _chestScript.openChest(false);
            }
        }

        // change current index to point at the next key
        nextKey();
    }

    // checks if player guessed the code correctly
    private void checkGuess()
    {
        //Debug.Log("secret code was: [ " + _chestCode[0] + " " + _chestCode[1] + " " + _chestCode[2] + " ]");
        //Debug.Log("player's guess was: [ " + _playerGuess[0] + " " + _playerGuess[1] + " " + _playerGuess[2] + " ]");

        // check if player guessed correctly all the keys in sequence
        if (_playerGuess[0] && _playerGuess[1] && _playerGuess[2])
        {
            // player guessed, change the flag
            _guessed = true;

            // open the chest if correct
            _chestScript.openChest(true);
        }
    }

    // changes current index to the next key
    private void nextKey()
    {
        // increment index, if it's too big it goes back to 0
        _currentIndex = (_currentIndex + 1) % 3;

        // if player already guessed this key - skip it
        if (_playerGuess[_currentIndex])
        {
            // first check if player guessed entire sequence
            checkGuess();

            // if not, increment the index again
            if (!_guessed)
            {
                nextKey();
            }
        }
        else
        {
            updateHighlighterPosition();
        }
    }

    // when players guessed correctly
    IEnumerator correctGuess()
    {
        // make the chest tranparent
        Color c = _chestSR.color;
        for (float f = 1.0f; f >= 0.5f; f -= 0.02f)
        {
            c.a = f;
            keysSR[0].color = c;
            keysSR[1].color = c;
            keysSR[2].color = c;
            _chestSR.color = c;
            yield return null;
        }
        // make the chest color normal again
        for (float f = 0.5f; f <= 1.0f; f += 0.02f)
        {
            c.a = f;
            keysSR[0].color = c;
            keysSR[1].color = c;
            keysSR[2].color = c;
            _chestSR.color = c;
            yield return null;
        }

        // restart to default color at the end
        keysSR[0].color = Color.white;
        keysSR[1].color = Color.white;
        keysSR[2].color = Color.white;
        _chestSR.color = Color.white;
    }

    // when players guessed incorrectly
    IEnumerator wrongGuess()
    {
        // make the chest color dark
        Color c = _chestSR.color;
        for (float f = 1.0f; f >= 0.5f; f -= 0.02f)
        {
            c.r = f;
            c.g = f;
            c.b = f;
            keysSR[0].color = c;
            keysSR[1].color = c;
            keysSR[2].color = c;
            _chestSR.color = c;
            yield return null;
        }
        // make the chest color normal again
        for (float f = 0.5f; f <= 1.0f; f += 0.02f)
        {
            c.r = f;
            c.g = f;
            c.b = f;
            keysSR[0].color = c;
            keysSR[1].color = c;
            keysSR[2].color = c;
            _chestSR.color = c;
            yield return null;
        }

        // restart to default color at the end
        keysSR[0].color = Color.white;
        keysSR[1].color = Color.white;
        keysSR[2].color = Color.white;
        _chestSR.color = Color.white;
    }

    public SpriteRenderer[] getKeysSR()
    {
        return keysSR;
    }

    private void updateHighlighterPosition()
    {
        highlighterTF.position = keysSR[_currentIndex].transform.position;
    }

    public void setChestCode(int[] code)
    {
        // only set the code if it's valid length, otherwise make random code
        if (code.Length == 3)
        {
            int index = 0;
            // look through entire array and set each value
            foreach (int colorCode in code)
            {
                switch (colorCode)
                {
                    case 0:
                    case 1:
                    case 2:
                        // color based on code if value is 1, 2 or 3
                        _chestCode[index] = colorCode;
                        break;
                    default:
                        // random color if value is different
                        _chestCode[index] = Random.Range(0, 3);
                        break;
                }
                index++;
            }
        }
        else
        {
            // generate random code
            for (int i = 0; i < _chestCode.Length; i++)
            {
                _chestCode[i] = Random.Range(0, 3);
            }
        }

    }
}
