using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;﻿

public class LevelManager : MonoBehaviour {

    public Transform MainMenu, OptionsMenu, LoadGameMenu, AboutMenu;


    public void NewGame(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }
    public void LoadGame(string loadLevel)
    {
        SceneManager.LoadScene(loadLevel);
    }

    public void Options(bool clicked)
    {
        if (clicked == true)
        {
            OptionsMenu.gameObject.SetActive(clicked);
            MainMenu.gameObject.SetActive(false);
        }else
        {
            OptionsMenu.gameObject.SetActive(clicked);
            MainMenu.gameObject.SetActive(true);
        }
    }
    public void LoadMenu(bool clicked)
    {
        if (clicked == true)
        {
            LoadGameMenu.gameObject.SetActive(clicked);
            MainMenu.gameObject.SetActive(false);
        }
        else
        {
            LoadGameMenu.gameObject.SetActive(clicked);
            MainMenu.gameObject.SetActive(true);
        }
    }
    public void About(bool clicked)
    {
        if (clicked == true)
        {
            AboutMenu.gameObject.SetActive(clicked);
            MainMenu.gameObject.SetActive(false);
        }
        else
        {
            AboutMenu.gameObject.SetActive(clicked);
            MainMenu.gameObject.SetActive(true);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
