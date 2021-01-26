using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class NetworkShipDestruction : NetworkBehaviour
{
    ShipDestruction dest;
    [SyncVar(hook =(nameof(setHP)))]
    public float syncHealth;
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalVariables.local)
        {
            enabled = false;
        }
        else
        {
            dest = gameObject.GetComponent<ShipDestruction>();
            syncHealth = dest.health;
            Debug.Log(dest.health + " sync:" + syncHealth);
        }
    }

    // Update is called once per frame
    [Server]
    void Update()
    {
        if(syncHealth != dest.health)
        {
            syncHealth = dest.health;
        }
    }

    private void setHP(float oldHP, float newHP)
    {
        if(syncHealth <= 0)
        {
            dest.health = 0;
            dest.Explosion();
            if (isServer)
            {
                NetworkServer.Spawn(dest.debrisInstance);
            }
            
        }
        else
        {
            dest.health = syncHealth;
        }
    }




}
