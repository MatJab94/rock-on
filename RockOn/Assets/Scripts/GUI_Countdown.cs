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

    public void startCountdown(float time)
    {
        _counter.text = "Time = " + (int)time;
        _sr.sprite = defaultSprite;
        StartCoroutine(countdown(time));
    }

    IEnumerator countdown(float time)
    {
        time++;
        while (time+1 >= 0)
        {
            Debug.Log("current time: " + time);
            _counter.text = "Time = " + (int)time;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            time -= Time.fixedDeltaTime;
        }

        // hide the icon and counter
        _counter.text = "";
        _sr.sprite = null;
    }
}
