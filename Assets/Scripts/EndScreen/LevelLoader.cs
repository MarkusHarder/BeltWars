using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
 
    public void LoadEndScreenLoss()
    {
        StartCoroutine(LoadLevel("YouLoose"));
    }

    public void LoadEndScreenWin()
    {
        StartCoroutine(LoadLevel("YouWin"));
    }

    IEnumerator LoadLevel(string name)
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        SceneManager.LoadScene(name);
    }
}
