using UnityEngine;
using System.Collections;

public class Color_Change : MonoBehaviour
{
    // object changes colors, set in Inspector
    public SpriteRenderer sr;

    // default color is 0 (red), used by other scripts to determine current color
    [HideInInspector]
    public int currentColorIndex = 0;

    // the 3 colors we use
    private Color _red, _green, _blue;

    // Use this for initialization
    void Start()
    {
        // initialising colors (Red, Green, Blue, Alpha)
        _red = new Color(0.9f, 0.2f, 0.2f, 0.25f);
        _green = new Color(0.2f, 0.9f, 0.2f, 0.25f);
        _blue = new Color(0.2f, 0.2f, 0.9f, 0.25f);

        // change the object's color to default (red)
        sr.color = _red;
    }

    // Update is called once per frame
    void Update()
    {
        // change colors based on inputs
        // inputs are set in InputManager
        if (Input.GetAxis("Color1") > 0)
        {
            sr.color = _red;
            currentColorIndex = 0;
        }
        if (Input.GetAxis("Color2") > 0)
        {
            sr.color = _green;
            currentColorIndex = 1;
        }
        if (Input.GetAxis("Color3") > 0)
        {
            sr.color = _blue;
            currentColorIndex = 2;
        }
        // sr.color changes the object's color
        // currentColorIndex is used by other scripts to easily determine the current color
    }
}
