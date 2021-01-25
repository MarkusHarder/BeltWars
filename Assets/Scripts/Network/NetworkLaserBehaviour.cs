using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkLaserBehaviour : NetworkBehaviour
{
    LaserBehaviour laser;
    [SyncVar (hook = nameof(setShip))]
    GameObject ship;
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalVariables.local)
        {
            enabled = false;
        }
        else
        {
            laser = gameObject.GetComponent<LaserBehaviour>();
            if (isServer)
            {
                ship = laser.ship;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void setShip(GameObject oldShip, GameObject newShip)
    {
        laser.ship = newShip;
        new WaitForEndOfFrame();
        laser.initialize();
    }
}
