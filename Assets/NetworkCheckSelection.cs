using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCheckSelection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject mainMenu, playMenu, MultiplayerTab, localMultiplayerTab, singleplayerTab;

    void Start()
    {
        if (GlobalVariables.selectedMP)
        {
            GlobalVariables.selectedMP = false;
            GlobalVariables.local = true;
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
        playMenu.SetActive(true);
        MultiplayerTab.SetActive(true);
        localMultiplayerTab.SetActive(false);
        singleplayerTab.SetActive(false);
    }
}
