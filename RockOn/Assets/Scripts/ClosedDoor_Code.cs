using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor_Code : MonoBehaviour
{
    // SpriteRenderers of the 4 "key" objects that show the code sequence on the door
    public SpriteRenderer[] keysSR;

    // sprites of the key objects
    public Sprite[] sprites;

    // default grey sprite for the key objects
    public Sprite defaultSprite;

    // the code sequence that opens this door (0, 1, 2 --> Red, Green, Blue)
    private int[] _closedDoorCode;

    // remembers which keys were guessed correctly to skip them
    private bool[] _playerGuess;

    // index of the current key object
    private int _currentIndex;

    // current attack color chosen by the player
    private Player_Color_Change _playerColor;

    // script opening the door
    private ClosedDoor_Open _closedDoorScript;

    // Transform of the Highlighter
    public Transform highlighterTF;

    // true when player guesses the sequence
    private bool _guessed;

    // target for cursor object of this door
    public GameObject targetForCursor;

    void Start()
    {
        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();
        _closedDoorScript = GetComponentInParent<ClosedDoor_Open>();

        // create arrays holding the secret code and player's guesses
        _closedDoorCode = new int[4] { -1, -1, -1, -1 };
        _playerGuess = new bool[4] { false, false, false, false };

        // point to the first key of the code sequence
        _currentIndex = 0;

        _guessed = false;

        updateHighlighterPosition();

        // disable the highlighter until player activates the clef
        highlighterTF.gameObject.SetActive(false);

        // the door is inactive at first, until player activates the clef
        targetForCursor.SetActive(false);

        // make sprites of the key objects darker until player activates the clef
        foreach (SpriteRenderer keySR in keysSR)
        {
            keySR.color = Color.gray;
        }
    }

    // called when player attacks the door with regular attack - he's trying to guess the code
    public void hitDoor()
    {
        // check if current key was guessed correctly
        if (_closedDoorCode[_currentIndex] == _playerColor.currentColorIndex)
        {
            // remember that player guessed this correctly
            _playerGuess[_currentIndex] = true;
        }
        else
        {
            // remember that player guessed this incorrectly
            _playerGuess[_currentIndex] = false;
        }

        // change current key object to match the color of player's attack
        keysSR[_currentIndex].sprite = sprites[_playerColor.currentColorIndex];

        // change current index to point at the next key
        nextKey();
    }

    // checks if player guessed the code correctly
    private void checkGuess()
    {
        // Debug.Log("secret code was: [ " + _closedDoorCode[0] + " " + _closedDoorCode[1] + " " + _closedDoorCode[2] + " " + _closedDoorCode[3] + " ]");
        // Debug.Log("player's guess was: [ " + _playerGuess[0] + " " + _playerGuess[1] + " " + _playerGuess[2] + " " + _playerGuess[3] + " ]");

        // check if player guessed correctly all the keys in sequence
        if (_playerGuess[0] && _playerGuess[1] && _playerGuess[2] && _playerGuess[3])
        {
            // correct guess
            _guessed = true;

            // disable the highlighter object, it's not needed any more
            updateHighlighterPosition();

            // destroy the target for cursor, so the player can't hit the door anymore
            Destroy(targetForCursor, 0.1f);

            // open the door
            _closedDoorScript.openDoor();
        }
        else
        {
            // wrong guess, restart the door
            StartCoroutine(restartDoor());
        }
    }

    // changes current index to the next key
    private void nextKey()
    {
        // increment index
        _currentIndex += 1;

        // if reached end of sequence - go back to beginning
        if (_currentIndex > 3)
        {
            _currentIndex = 0;

            // check if player guessed the sequence correctly
            checkGuess();
        }

        updateHighlighterPosition();
    }

    // restarts the door if player guessed incorrectly
    IEnumerator restartDoor()
    {
        // wait before resetting so all keys are visible
        yield return new WaitForSeconds(0.5f);

        // restart sprites of the key objects to default
        foreach (SpriteRenderer keySR in keysSR)
        {
            keySR.sprite = defaultSprite;
        }

        // restart the player guess array
        _playerGuess = new bool[4] { false, false, false, false };
    }

    public SpriteRenderer[] getKeysSR()
    {
        return keysSR;
    }

    private void updateHighlighterPosition()
    {
        // disable the object when sequence is guessed, otherwise move to next key object
        if (_guessed)
        {
            highlighterTF.gameObject.SetActive(false);
        }
        else
        {
            highlighterTF.position = keysSR[_currentIndex].transform.position;
        }
    }

    public int[] getSecretCode()
    {
        return _closedDoorCode;
    }

    public void activateTheDoor()
    {
        // enable the highlighter
        highlighterTF.gameObject.SetActive(true);

        // enable the target for cursor
        targetForCursor.SetActive(true);

        // make sprites of the key objects normal
        foreach (SpriteRenderer keySR in keysSR)
        {
            keySR.color = Color.white;
        }
    }

    public void setClosedDoorCode(int[] code)
    {
        // only set the code if it's valid length, otherwise make random code
        if(code.Length == 4)
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
                        _closedDoorCode[index] = colorCode;
                        break;
                    default:
                        // random color if value is different
                        _closedDoorCode[index] = Random.Range(0, 3);
                        break;
                }
                index++;
            }
        }
        else
        {
            // generate random code
            for (int i = 0; i < _closedDoorCode.Length; i++)
            {
                _closedDoorCode[i] = Random.Range(0, 3);
            }
        }

    }

}
