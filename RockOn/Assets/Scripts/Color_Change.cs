using UnityEngine;
using System.Collections;

public class Color_Change : MonoBehaviour
{

    public SpriteRenderer sr;

    [HideInInspector]
    public int currentColorIndex = 0;

    private Color _red, _green, _blue;

    // Use this for initialization
    void Start()
    {
        _red = new Color(0.9f, 0.2f, 0.2f, 0.25f);
        _green = new Color(0.2f, 0.9f, 0.2f, 0.25f);
        _blue = new Color(0.2f, 0.2f, 0.9f, 0.25f);

        sr.color = _red;
    }

    // Update is called once per frame
    void Update()
    {
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


    }
}
