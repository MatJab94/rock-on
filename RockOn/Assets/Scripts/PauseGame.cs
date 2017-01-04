using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject PauseUI;
    private bool _paused = false;
    
    void Start()
    {
        PauseUI.SetActive(false);
    }
    
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
