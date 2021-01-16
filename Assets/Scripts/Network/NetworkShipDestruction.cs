using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class NetworkShipDestruction : NetworkBehaviour
{
    ShipDestruction dest;
    [SyncVar]
    public float syncHealth;
    public float tmpHealth;
    bool control = true;
    bool waiting = false;
    // Start is called before the first frame update
    void Start()
    {
        dest = gameObject.GetComponent<ShipDestruction>();
        syncHealth = dest.health;
        Debug.Log(dest.health + " sync:" + syncHealth);
    }

    // Update is called once per frame
    [Server]
    void Update()
    {
        if(syncHealth != dest.health)
        {
            syncHealth = dest.health;
            rpcSetHP(syncHealth);
        }
    }

    [ClientRpc]
    private void rpcSetHP(float newHP)
    {
        if(syncHealth <= 0)
        {
            dest.health = 0;
            dest.Explosion();
            
        }
        else
        {
            dest.health = syncHealth;
        }
    }




}
