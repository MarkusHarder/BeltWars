using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkGameController : GameController
{
    public bool start = false;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    override protected void Update()
    {
        if(!start) { return; }
        base.Update();
    }
}
