using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RythmAnimation : MonoBehaviour
{

    private SpriteRenderer _sr;
    private RythmBattle _rb;
    private float scale;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GameObject.FindGameObjectWithTag("RythmBattle").GetComponent<RythmBattle>();
        if (gameObject.tag.Equals("GUI_Health") || gameObject.tag.Equals("GUI_Mana"))
        {
            scale = 0.5f;
        }
        else
        {
            scale = 1.0f;
        }
    }

    void FixedUpdate()
    {
        if (_rb.getRythmFlag())
        {
            _sr.color = Color.white;
        }
        else
        {
            float color = _sr.color.r - Time.deltaTime * scale;
            _sr.color = new Color(color, color, color);
        }
    }
}
