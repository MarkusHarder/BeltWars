using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCheckSelection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject mainMenu, pauseMenu, playMenu, onlineTab, localTab;

    void Start()
    {
        if (GlobalVariables.selectedMP)
        {
            GlobalVariables.selectedMP = false;
            setMenuState();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setMenuState()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        playMenu.SetActive(true);
        onlineTab.SetActive(true);
        localTab.SetActive(false);

    }
}
