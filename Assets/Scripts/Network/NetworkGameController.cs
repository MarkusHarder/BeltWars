using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkGameController : GameController
{
    public bool start = false;
    public bool networkObj = true;
    public NetworkConnection conn;
    bool turn = true;
    public List<GameObject> elements;
    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.local = false;
        
    }

    // Update is called once per frame
    [Server]
    override protected void Update()
    {
        if(!start) { return; }
        base.Update();

    }

    public void setAuth()
    {
        foreach (GameObject el in elements)
        {
            if (el.name.Contains("Ship") || el.name.Contains("Asteroid")){
                el.GetComponent<NetworkIdentity>().RemoveClientAuthority();
                if (turn)
                    el.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
                else
                    el.GetComponent<NetworkIdentity>().AssignClientAuthority(NetworkServer.localConnection);
            }

        }

        turn = !turn;

    }
}
