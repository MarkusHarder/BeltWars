using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject quitPopUpUI;

    private GameObject activeShip;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            FindObjectOfType<AudioManager>().Stop("engine1");
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        ShipContainer.setShipActive(activeShip);
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        quitPopUpUI.SetActive(false);
        GameIsPaused = false;
    }

    void Pause()
    {
        activeShip = ShipContainer.getActiveShip();
        ShipContainer.deactivateAllShips();
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
