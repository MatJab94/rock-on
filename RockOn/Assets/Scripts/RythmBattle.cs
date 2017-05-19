using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RythmBattle : MonoBehaviour
{
    // stuff for firing events, notifying objects about rythm
    public delegate void goodRythm();
    public static event goodRythm OnGoodRythm;
    public delegate void badRythm();
    public static event badRythm OnBadRythm;
    public delegate void Beat();
    public static event Beat OnBeat;
    [HideInInspector]
    public bool rythmFlag; // true when it's the right "moment" in the beat, false otherwise
    public float range; // how much time there is before and after the beat, set in Inspector
    public float numOfBeatsInClip; // number of beats in the clip, set in Inspector
    private float _f; // timer, shows current beat time
    private float _beatLength; // how long is single beat in this clip
    private AudioClip _audioClip; // this gameObject's audio clip
    private AudioSource _audioSource; // this gameObject's audio source
    public AudioSource _audioSourceMetronome; // Metronome's audio source
    private float _clipLength; // length of this clip
    private int _numOfBeatsElapsed; // number of beats played so far
    private float _badRythmStart; // the beginning of range in which rythmFlag is false
    private float _badRythmEnd; // the end of range in which rythmFlag is false
    private Player_Mana _playerMana; // player mana script for adding bonuses
    private Text _message; //combo message - when combo is achieved
    private int _badrhythmcounter;
    private bool _isBonusAdded;
    private string[] _reprimandStrings;
    private string _reprimand;

    // Use this for initialization
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioClip = _audioSource.clip;
        _clipLength = _audioClip.length;
        _beatLength = _clipLength / numOfBeatsInClip;
        _badRythmStart = range;
        _badRythmEnd = _beatLength - range;
        _playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Mana>();
        _badrhythmcounter = 0;
        _isBonusAdded = false;
        _reprimandStrings = new string[4] { "Ain't got no rhythm!", "Not quite my tempo! ", "such a bad timing!", "Play it in RHYTHM!" };
        _message = GameObject.FindWithTag("GUI_message").GetComponent<Text>();
        _numOfBeatsElapsed = 0;
        _f = 0.0f;
        rythmFlag = true;
        // Debug.Log("Audio length = " + _clipLength);
        // Debug.Log("Beat length = " + _beatLength);
        // Debug.Log("Bad Rythm Start = " + _badRythmStart);
        // Debug.Log("Bad Rythm End = " + _badRythmEnd);
        _audioSource.Play();
        _audioSourceMetronome.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // every frame add difference in time since last frame
        _f += Time.deltaTime;

        // synchronise the script with the clip every time all beats were played (track ended)
        if (rythmFlag == false && _numOfBeatsElapsed == numOfBeatsInClip)
        {
            _f = _audioSource.time;
            _numOfBeatsElapsed = 0;
        }

        // _f goes from 0 to _beatLength and restarts
        if (_f >= _beatLength)
        {
            _f -= _beatLength;
            _numOfBeatsElapsed++;
            // fire the event and notify objects about beat
            if (OnBeat != null) OnBeat();
        }

        // if flag is true and we're in bad rythm range, turn the flag to false
        if (rythmFlag == true && _f > _badRythmStart && _f < _badRythmEnd)
        {
            // fire the event and notify objects about rythm
            if (OnBadRythm != null) OnBadRythm();

            rythmFlag = false;
        }

        // if flag is false and we're in good rythm range, turn the flag to true
        if (rythmFlag == false && (_f < _badRythmStart || _f > _badRythmEnd))
        {
            // fire the event and notify objects about rythm
            if (OnGoodRythm != null) OnGoodRythm();

            rythmFlag = true;
        }
    }

    public void addBonus()
    {
        _isBonusAdded = true;
        _playerMana.addMana(1);
        
        if (_playerMana.getMana() >= 5)
        {
            StartCoroutine(ShowMessage("Combo!", 1)); //combo message               
        }
    }

    public void resetBonus()
    {
        // _combo = 0;
        //Debug.Log("Bonus restarted! Combo = " + _combo);

        //Combo counter / display
        //  _textCombo.GetComponent<Text>().text = "Combo = " + _combo.ToString();
    }

    public void addReprimand() // if you fail in Rhythm battle
    {
        _badrhythmcounter++;
        _reprimand = _reprimandStrings[Random.Range(0, 4)];
        if (_isBonusAdded == true)
        {
            _badrhythmcounter = 0;
        }
        if (_badrhythmcounter >= 4)
        {
            addSpecialReprimand();
            _badrhythmcounter = 0;
        }
        else
        {
            StartCoroutine(ShowMessage(_reprimand, 1.5f)); //Reprimand message
        }
        _isBonusAdded = false;
    }
    public void addPraise() // praise message - if enemy is just killed
    {
        StartCoroutine(ShowMessage("YEAH!", 1));
    }
    public void addSpecialReprimand()
    {
        StartCoroutine(ShowMessage("YOU SUCK!", 1));
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        _message.text = message;
        _message.enabled = true;
        yield return new WaitForSeconds(delay);
        _message.enabled = false;

    }

    public bool getRythmFlag()
    {
        return rythmFlag;
    }

    public int getNumOfBeatsElapsed()
    {
        return _numOfBeatsElapsed;
    }

}
