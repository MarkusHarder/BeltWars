using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkGameController : GameController
{
    public bool start = false;
    public bool networkObj = true;
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
}
