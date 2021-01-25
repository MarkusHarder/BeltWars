using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkSceneCreator : GameSceneCreator
{
    // Start is called before the first frame update


     public void createNetworkGameScene(NetworkConnection conn)
    {
        base.createGameScene();
        foreach(GameObject el in game)
        {
            if (el.name.Contains("Ship_Mars"))
            {
                NetworkServer.Spawn(el.gameObject, conn);
            } else if (el.name.Contains("Ship_Earth"))
            {
                NetworkServer.Spawn(el.gameObject, NetworkServer.localConnection);
            }
            else {
                NetworkServer.Spawn(el.gameObject);
            }
        }
    }
}
