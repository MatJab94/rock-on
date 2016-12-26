using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    //  private Image _screenFader;
    // private Color _fadeColor;

    public GameObject PauseUI;
    private bool _paused = false;

    // Use this for initialization
    void Start()
    {

        PauseUI.SetActive(false);

        // TO-DO: Screen fading
        //  _screenFader = GameObject.FindGameObjectWithTag("GUI_Screen_Fader").GetComponent<Image>();
        //   _fadeColor = Color.black;
        //  _fadeColor.a = 0.0f;
        //   _screenFader.color = _fadeColor;
    }

    // Update is called once per frame
    void Update()
    {       
        if (Input.GetButtonDown("Pause"))
        {
            _paused = !_paused;
        }
        if (_paused)
        {
            PauseUI.SetActive(true);
            AudioListener.pause = true;
            Time.timeScale = 0;
        }
        else if (!_paused)
        {
            PauseUI.SetActive(false);
            AudioListener.pause = false;
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        _paused = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
