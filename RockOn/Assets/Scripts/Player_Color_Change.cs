using UnityEngine;

public class Player_Color_Change : MonoBehaviour
{
    // default color is 0 (red), used by other scripts to determine current color
    [HideInInspector]
    public int currentColorIndex = 0;

    // the 3 colors we use
    private Color _red, _green, _blue, _red2, _green2, _blue2;
    private Color[] _primary;
    private Color[] _secondary;

    // for changing the color of the line when attacking
    private LineRenderer _lr;

    // for changing the color of the cursor
    private Cursor_ColorChange _cursorColor;

    // This object's Animator component, to animate when walking
    private Animator _anim;

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

        // for scroll wheel color changing
        _primary = new Color[] { _red, _green, _blue };
        _secondary = new Color[] { _red2, _green2, _blue2 };

        _lr = GetComponentInChildren<LineRenderer>();

        _anim = GetComponent<Animator>();
        _anim.SetInteger("color", 0);

        _cursorColor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor_ColorChange>();
    }

    // Update is called once per frame
    void Update()
    {
        // change colors based on inputs
        if (Input.GetAxisRaw("Color1") != 0)
        {
            currentColorIndex = 0;

            _anim.SetInteger("color", 0);

            _lr.startColor = _red;
            _lr.endColor = _red2;

            _cursorColor.colorChange(currentColorIndex);
        }
        if (Input.GetAxisRaw("Color2") != 0)
        {
            currentColorIndex = 1;

            _anim.SetInteger("color", 1);

            _lr.startColor = _green;
            _lr.endColor = _green2;

            _cursorColor.colorChange(currentColorIndex);
        }
        if (Input.GetAxisRaw("Color3") != 0)
        {
            currentColorIndex = 2;

            _anim.SetInteger("color", 2);

            _lr.startColor = _blue;
            _lr.endColor = _blue2;

            _cursorColor.colorChange(currentColorIndex);
        }
        if (Input.GetAxis("MouseWheel") > 0.0f || Input.GetButtonDown("ChangeColorNext"))
        {
            currentColorIndex = (currentColorIndex + 1) % 3;

            _anim.SetInteger("color", currentColorIndex);

            _lr.startColor = _primary[currentColorIndex];
            _lr.endColor = _secondary[currentColorIndex];

            _cursorColor.colorChange(currentColorIndex);
        }
        if (Input.GetAxis("MouseWheel") < 0.0f || Input.GetButtonDown("ChangeColorPrevious"))
        {
            currentColorIndex = (currentColorIndex + 2) % 3;

            _anim.SetInteger("color", currentColorIndex);

            _lr.startColor = _primary[currentColorIndex];
            _lr.endColor = _secondary[currentColorIndex];

            _cursorColor.colorChange(currentColorIndex);
        }
    }
}
