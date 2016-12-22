using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_ColorChange : MonoBehaviour {

    // stuff for changing cursor's color
    public Texture2D[] cursorTexture;
    private CursorMode _cursorMode;
    private Vector2 _hotSpot;

    // Use this for initialization
    void Start () {
        _cursorMode = CursorMode.Auto;
        _hotSpot = Vector2.zero;
    }

    public void colorChange(int colorIndex)
    {
        Cursor.SetCursor(cursorTexture[colorIndex], _hotSpot, _cursorMode);
    }

}
