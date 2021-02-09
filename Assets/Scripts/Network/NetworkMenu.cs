﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private NetworkManagerBeltWars networkManager = null;

    private void Start()
    {
        GlobalVariables.selectedMP = true;
    }
    public void HostLobby()
    {
        networkManager.StartHost();
        Debug.Log(networkManager.isNetworkActive);
    }

    public void StopHost()
    {
        networkManager.StopHost();
        Debug.Log(networkManager.isNetworkActive);
    }
}