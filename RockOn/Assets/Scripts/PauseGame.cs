using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{

    public Transform OptionsMenu;

    public GameObject PauseUI;
    private bool _paused = false;

    // Use this for initialization
    void Start()
    {
        PauseUI.SetActive(false);
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
    public void Options(bool clicked)
    {
        if (clicked == true)
        {
            OptionsMenu.gameObject.SetActive(clicked);
            //  PauseUI.gameObject.SetActive(false);
        }
        else
        {
            OptionsMenu.gameObject.SetActive(clicked);
          //  PauseUI.gameObject.SetActive(true);
        }
    }


    public void MainMenu(string menu)
    {
        SceneManager.LoadScene(menu);
    }
}
