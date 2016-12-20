using UnityEngine;
using UnityEngine.UI;

public class Player_Color_Change : MonoBehaviour
{
    // default color is 0 (red), used by other scripts to determine current color
    [HideInInspector]
    public int currentColorIndex = 0;

    // object that changes colors (the square in GUI)
    private Image _img;

    // the 3 colors we use
    private Color _red, _green, _blue;

    // Use this for initialization
    void Start()
    {
        // initialising colors (Red, Green, Blue, Alpha)
        _red = new Color(0.9f, 0.2f, 0.2f);
        _green = new Color(0.2f, 0.9f, 0.2f);
        _blue = new Color(0.2f, 0.2f, 0.9f);

        // initialise _sr and change the object's color to default (red)
        _img = GameObject.FindGameObjectWithTag("GUI_Attack_Color").GetComponent<Image>();
        _img.color = _red;
    }

    // Update is called once per frame
    void Update()
    {
        // change colors based on inputs
        // inputs are set in InputManager
        if (Input.GetAxis("Color1") > 0)
        {
            _img.color = _red;
            currentColorIndex = 0;
        }
        if (Input.GetAxis("Color2") > 0)
        {
            _img.color = _green;
            currentColorIndex = 1;
        }
        if (Input.GetAxis("Color3") > 0)
        {
            _img.color = _blue;
            currentColorIndex = 2;
        }
        // sr.color changes the object's color
        // currentColorIndex is used by other scripts to easily determine the current color
    }

    // returns a new color that is the same as the current (ie. for highlighting the AoE collider)
    public Color getCurrentColor()
    {
        if (currentColorIndex == 0)
        {
            return new Color(_red.r, _red.g, _red.b);
        }
        else
        {
            if (currentColorIndex == 1)
            {
                return new Color(_green.r, _green.g, _green.b);
            }
            else
            {
                return new Color(_blue.r, _blue.g, _blue.b);
            }
        }

    }
}
