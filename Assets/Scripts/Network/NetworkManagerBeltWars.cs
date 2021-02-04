using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using Mirror;

[AddComponentMenu("")]
public class NetworkManagerBeltWars : NetworkManager
{
    public GameObject startPos1;
    public GameObject startPos2;
    private int count = 1;
    private NetworkConnection cl;
    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public static event Action OnGameStarted;
    [SerializeField] private GameObject menuObject;


    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientConnected?.Invoke();
    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        OnClientDisconnected?.Invoke();
    }
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
            OnGameStarted?.Invoke();
            menuObject.SetActive(false);
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
