using UnityEngine;
// using UnityEngine.UI; // NO LONGER USED

public class Player_Color_Change : MonoBehaviour
{
    // default color is 0 (red), used by other scripts to determine current color
    [HideInInspector]
    public int currentColorIndex = 0;

    // object that changes colors (the square in GUI)
    // private Image _img; // NO LONGER USED

    // the 3 colors we use
    private Color _red, _green, _blue, _red2, _green2, _blue2;

    // for changing the color of the line when attacking
    private LineRenderer _lr;

    // for changing the color of the cursor
    private Cursor_ColorChange _cursorColor;

    // Use this for initialization
    void Start()
    {
        // initialising colors (values taken from enemies colors)
        _red = new Color(0.55f, 0.27f, 0.23f);
        _red2 = new Color(0.70f, 0.43f, 0.23f);
        _green = new Color(0.39f, 0.60f, 0.32f);
        _green2 = new Color(0.67f, 0.78f, 0.38f);
        _blue = new Color(0.27f, 0.46f, 0.66f);
        _blue2 = new Color(0.34f, 0.70f, 0.80f);

        // initialise _sr and change the object's color to default (red)
        //_img = GameObject.FindGameObjectWithTag("GUI_Attack_Color").GetComponent<Image>();
        //_img.color = _red; // NO LONGER USED

        _lr = GetComponentInChildren<LineRenderer>();

        _cursorColor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor_ColorChange>();
    }

    // Update is called once per frame
    void Update()
    {
        // change colors based on inputs
        // inputs are set in InputManager
        if (Input.GetAxisRaw("Color1") != 0)
        {
            // _img.color = _red; // NO LONGER USED
            currentColorIndex = 0;

            _lr.startColor = _red;
            _lr.endColor = _red2;

            _cursorColor.colorChange(currentColorIndex);
        }
        if (Input.GetAxisRaw("Color2") != 0)
        {
            // _img.color = _green; // NO LONGER USED
            currentColorIndex = 1;

            _lr.startColor = _green;
            _lr.endColor = _green2;

            _cursorColor.colorChange(currentColorIndex);
        }
        if (Input.GetAxisRaw("Color3") != 0)
        {
            // _img.color = _blue; // NO LONGER USED
            currentColorIndex = 2;

            _lr.startColor = _blue;
            _lr.endColor = _blue2;

            _cursorColor.colorChange(currentColorIndex);
        }
        // sr.color changes the object's color
        // currentColorIndex is used by other scripts to easily determine the current color
    }

    public void changeCursorColor()
    {
        //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
