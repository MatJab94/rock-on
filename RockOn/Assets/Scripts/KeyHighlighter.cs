using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHighlighter : MonoBehaviour
{
    // this objects sprite renderer for "animation"
    private SpriteRenderer _sr;

    // current color for the sprite
    private Color _currentColor;

    // how fast the animation is, animate between min and max values
    private float _highlightSpeed, _min, _max;

    // should it get darker or brighter
    private bool _darken;

    // Use this for initialization
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        
        _highlightSpeed = 0.01f;
        _min = 0.3f;
        _max = 0.7f;
        
        _currentColor = new Color(_max, _max, _max);
        _sr.color = _currentColor;

        _darken = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_darken)
        {
            // make the color darker
            _currentColor.r -= _highlightSpeed;
            _currentColor.g -= _highlightSpeed;
            _currentColor.b -= _highlightSpeed;
        }
        else
        {
            // make the color brighter
            _currentColor.r += _highlightSpeed;
            _currentColor.g += _highlightSpeed;
            _currentColor.b += _highlightSpeed;
        }

        // change sprite's color
        _sr.color = _currentColor;

        // check if color is between min and max, otherwise change the flag
        if (_sr.color.r < _min)
        {
            _darken = false;
        }
        if (_sr.color.r > _max)
        {
            _darken = true;
        }
    }
}
