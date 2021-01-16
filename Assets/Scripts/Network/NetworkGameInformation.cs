using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class NetworkGameInformation : NetworkBehaviour
{
    GameInformation gi;
    [SyncVar(hook = nameof(updatePickup))]
    private string text;
    // Start is called before the first frame update
    void Start()
    {
        gi = gameObject.GetComponent<GameInformation>();
    }

    
    
    // Update is called once per frame
    [Server]
    void Update()
    {
        if (gi.executed)
        {
            gi.executed = false;
            text = gi.gameInfo;
        }
    }


    private void updatePickup(string oldT, string newT)
    {
        gi.startDisplay(newT);
    }
}
