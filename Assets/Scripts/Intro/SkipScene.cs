using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipScene : MonoBehaviour
{
    public void LoadNextScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") || Input.GetKey("escape") || Input.GetKey("enter"))
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Intro_1")
            {
                this.LoadNextScene("Intro_2");
            }
            else if (currentScene == "Intro_2")
            {
                this.LoadNextScene("Intro_3");
            }
            else if (currentScene == "Intro_3")
            {
                this.LoadNextScene("Menu");
            }
        }
    }
}
