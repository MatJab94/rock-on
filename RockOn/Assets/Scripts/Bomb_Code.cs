using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Code : MonoBehaviour
{
    // SpriteRenderer of the "key" object that shows the correct color on the bomb
    public SpriteRenderer keySR;

    // sprites of the key object
    public Sprite[] sprites;

    // default grey sprite for the key object
    public Sprite defaultSprite;

    // the color that detonates this bomb (0, 1, 2 --> R, G, B)
    private int _bombCode;

    // current attack color chosen by the player
    private Player_Color_Change _playerColor;

    // main script of the bomb
    private Bomb_BlowUp _bombScript;

    // SpriteRenderer of the bomb
    private SpriteRenderer _bombSR;

    // Transform of the Highlighter
    public Transform highlighterTF;

    // number of tries
    private int _numOfTries;

    // number of tries at which the bomb damages the player
    private int _maxNumOfTries;

    // rythm battle script
    private RythmBattle _rythmBattle;

    // can bomb be detonated?
    private bool canDetonate;

    // beat counter, bomb can only be blown up when == 0
    int beatCounter;

    void Start()
    {
        _playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Color_Change>();
        _bombScript = GetComponentInParent<Bomb_BlowUp>();
        _bombSR = GetComponentInParent<SpriteRenderer>();
        _rythmBattle = GameObject.FindGameObjectWithTag("RythmBattle").GetComponent<RythmBattle>();

        _bombCode = Random.Range(0, 3);

        _numOfTries = 0;
        _maxNumOfTries = 3;

        beatCounter = 0;
        canDetonate = false;
        _bombSR.color = Color.gray;
    }

    private void Update()
    {
        beatCounter = _rythmBattle.getNumOfBeatsElapsed() % 4;

        if (!getRythm() && canDetonate && beatCounter != 0)
        {
            canDetonate = false;
            _bombSR.color = Color.gray;
            keySR.sprite = defaultSprite;
        }

        if (getRythm() && !canDetonate && beatCounter == 0)
        {
            canDetonate = true;
            _bombSR.color = Color.white;
            keySR.sprite = sprites[_bombCode];
        }
    }

    // called when player attacks the chest with regular attack - he's trying to guess the code
    public void hitBomb()
    {
        _numOfTries++;

        // check if bomb was detonated correctly
        if (canDetonate && _playerColor.currentColorIndex == _bombCode)
        {
            Debug.Log("BOOOOOOOOOOM");
        }
    }

    public SpriteRenderer getKeySR()
    {
        return keySR;
    }

    // getter to easily get the flag
    private bool getRythm()
    {
        return _rythmBattle.getRythmFlag();
    }
}
