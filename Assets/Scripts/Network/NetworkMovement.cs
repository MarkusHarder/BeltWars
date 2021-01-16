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

    [SyncVar]
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        mov = gameObject.GetComponent<ProtoMovement>();
    }

    // Update is called once per frame
    void Update()
    {
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
}
