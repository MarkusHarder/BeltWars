using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartSingleplayer()
    {
        GlobalVariables.singlePlayer = true;
        SceneManager.LoadScene("MainScene");
    }

    public void StartLocalMultiplayer()
    {
        GlobalVariables.singlePlayer = false;
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }


}
