using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


/**
 * handle shooting/health in a similar way
 * how to solve regular game?
 */
public class NetworkMovement : NetworkBehaviour
{
    ProtoMovement mov;
    Circle circ;
    NetworkGameController ngc;
    [SyncVar(hook = nameof(toggleCirc))]
    public bool active;
    private bool running = false;
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalVariables.local)
        {
            enabled = false;
        }
        else
        {
            ngc = GameObject.FindObjectOfType<NetworkGameController>();
            mov = gameObject.GetComponent<ProtoMovement>();
            circ = gameObject.GetComponent<Circle>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isServer)
            checkMov();
        if (active && hasAuthority)
        {
            
            mov.keepObjectInCameraView();
            mov.moveShip();
        }
    }



    [Server]
    private void checkMov()
    {
        active = mov.active;
    }

    private void toggleCirc(bool activeO, bool activeN)
    {
        Debug.Log("Togglebecher " + activeN);
        if (activeN)
        {
            if(isServer)
                ngc.setAuth();
            
        }
        circ.active = activeN;

    }

}
