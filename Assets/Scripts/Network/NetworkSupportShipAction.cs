using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkSupportShipAction : NetworkBehaviour
{
    SupportShipAction supAction;
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalVariables.local)
        {
            enabled = false;
        }
        else
        {
            supAction = gameObject.GetComponent<SupportShipAction>();
        }
    }

    // Update is called once per frame
    [Server]
    void Update()
    {
        supAction.moveShip();
    }
}
