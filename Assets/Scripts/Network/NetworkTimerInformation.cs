using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

/**
 * Ship Information the same way
 */
public class NetworkTimerInformation : NetworkBehaviour
{
    TimerInformation tInfo;
    [SyncVar]
    int timeLeft;

    private void Start()
    {
            tInfo = gameObject.GetComponent<TimerInformation>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.local)
        {
            enabled = false;
        }
        else
        {
            getTime();
            displayInformation();
        }


    }

    public void displayInformation()
    {
       
      gameObject.GetComponent<Text>().text = "" + timeLeft + " ";
    }

    [Server]
    void getTime()
    {
        tInfo.getTime();
        timeLeft = tInfo.timeLeft;
    }
}
