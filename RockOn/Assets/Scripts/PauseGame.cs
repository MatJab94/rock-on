using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    private Image _screenFader;
    private Color _fadeColor;
    private bool _paused;

    // Use this for initialization
    void Start()
    {
        // TO-DO: Screen fading
        _screenFader = GameObject.FindGameObjectWithTag("GUI_Screen_Fader").GetComponent<Image>();
        _fadeColor = Color.black;
        _fadeColor.a = 0.0f;
        _screenFader.color = _fadeColor;

        _paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _paused = !_paused;
        }
        if (_paused)
        {

            Time.timeScale = 0;
        }
        else if (!_paused)
        {
            Time.timeScale = 1;
        }

    }
}
