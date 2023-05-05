using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("MainGameScene");
    }
    public void QuitButtonPressed()
    {
        Application.Quit();
    }

}
