using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Countdown : MonoBehaviour
{
    private Text _counter;

    private SpriteRenderer _sr;
    public Sprite defaultSprite;

    void Start()
    {
        _counter = GetComponentInChildren<Text>();
        _sr = GetComponent<SpriteRenderer>();
        _counter.text = "";
        _sr.sprite = null;

    }

    public void updateCountdown(int time)
    {
        _counter.text = "Time = " + time;
        _sr.sprite = defaultSprite;
    }

    public void turnOffCountdown()
    {
        _counter.text = "";
        _sr.sprite = null;
    }
}
