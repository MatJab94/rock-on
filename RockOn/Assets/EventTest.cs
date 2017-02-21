using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnEnable()
    {
        RythmBattle.OnGoodRythm += good;
        RythmBattle.OnBadRythm += bad;
    }

    private void OnDisable()
    {
        RythmBattle.OnGoodRythm -= good;
        RythmBattle.OnBadRythm -= bad;
    }

    void good()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    void bad()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }
}
