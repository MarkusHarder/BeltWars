﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[AddComponentMenu("")]
public class NetworkManagerBeltWars : NetworkManager
{
    public GameObject startPos1;
    public GameObject startPos2;
    private int count = 1;
    private NetworkConnection cl;


    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform start = numPlayers == 0 ? startPos1.transform : startPos2.transform;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        player.name = "Player" + count++;
        NetworkServer.AddPlayerForConnection(conn, player);
        foreach (var it in NetworkServer.connections)
        {
            if (it.Value != NetworkServer.localConnection)
            {
                cl = it.Value;
            }
        }

        if (numPlayers == 2)
        {
            GlobalVariables.local = false;
            NetworkSceneCreator nsc = new NetworkSceneCreator();
            nsc.createNetworkGameScene(cl);
            NetworkGameController ngc = GameObject.Find("NetworkGameController").GetComponent<NetworkGameController>();
            ngc.conn = conn;
            ngc.elements = nsc.game;
            ngc.start = true;
           


        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        count--;
        base.OnServerDisconnect(conn);
    }

}
