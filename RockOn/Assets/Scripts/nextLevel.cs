using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour {


    public string nextLevelstring;
    
    public void LoadLevel(string level)
    {
        PlayerPrefs.GetFloat("CurVol");
        SceneManager.LoadScene(level);
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LoadLevel(nextLevelstring);
        }
    }
}
