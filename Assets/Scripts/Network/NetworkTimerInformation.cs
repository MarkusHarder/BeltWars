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
    [SyncVar (hook = nameof (displayInformation))]
    int timeLeft;

    private void Start()
    {
            tInfo = gameObject.GetComponent<TimerInformation>();
    }
    // Update is called once per frame
    [Server]
    void Update()
    {
        getTime();
    }

    public void displayInformation(int oldT, int newT)
    {

        tInfo.timeLeft = newT;
        tInfo.displayInformation();
    }

    [Server]
    void getTime()
    {
        tInfo.getTime();
        timeLeft = tInfo.timeLeft;
    }
}
