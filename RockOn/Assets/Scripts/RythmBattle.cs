﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmBattle : MonoBehaviour
{

    public float range; // how much time there is before and after the beat, set in Inspector
    public float numOfBeatsInClip; // number of beats in the clip, set in Inspector

    [HideInInspector]
    public bool rythmFlag; // true when it's the right "moment" in the beat, false otherwise

    private float _f; // timer, shows current beat time
    private float _beatLength; // how long is single beat in this clip
    private AudioClip _audioClip; // this gameObject's audio clip
    private AudioSource _audioSource; // this gameObject's audio source
    private float _clipLength; // length of this clip
    private int _numOfBeatsElapsed; // number of beats played so far

    private float _badRythmStart; // the beginning of range in which rythmFlag is false
    private float _badRythmEnd; // the end of range in which rythmFlag is false

    private int _combo; // counts how well the player hits in rythm

    // Use this for initialization
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioClip = _audioSource.clip;
        _clipLength = _audioClip.length;
        _beatLength = _clipLength / numOfBeatsInClip;

        _badRythmStart = range;
        _badRythmEnd = _beatLength - range;

        _combo = 0;

         Debug.Log("Audio length = " + _clipLength);
         Debug.Log("Beat length = " + _beatLength);
         Debug.Log("Bad Rythm Start = " + _badRythmStart);
         Debug.Log("Bad Rythm End = " + _badRythmEnd);

        _numOfBeatsElapsed = 0;
        _f = 0.0f;
        rythmFlag = true;
    }

    // Update is called once per frame
    void Update()
    {

        // allign script with the clip every time all beats were played (track ended)
        if (rythmFlag == false && _numOfBeatsElapsed == numOfBeatsInClip)
        {
            _f = _audioSource.time;
            _numOfBeatsElapsed = 0;
            // Debug.Log("TRACK ENDED");
        }

        // _f goes from 0 to _beatLength and restarts
        if (_f >= _beatLength)
        {
            _f -= _beatLength;
            _numOfBeatsElapsed++;
            Debug.Log("BEAT");
        }

        // every frame add differance in time since last frame
        _f += Time.deltaTime;

        // if flag is true and we're in bad rythm range, turn the flag to false
        if (rythmFlag == true && _f > _badRythmStart && _f < _badRythmEnd)
        {
            rythmFlag = false;
            // Debug.Log("Flag is false");
        }

        // if flag is false and we're in good rythm range, turn the flag to true
        if (rythmFlag == false && (_f < _badRythmStart || _f > _badRythmEnd))
        {
            rythmFlag = true;
            // Debug.Log("Flag is true");
        }
    }

    public void addBonus()
    {
        Debug.Log("C-c-c-combo!!! Combo = " + _combo);
        _combo++;
    }

    public void resetBonus()
    {
        _combo = 0;
        Debug.Log("Bonus restarted! Combo = " + _combo);
    }

}