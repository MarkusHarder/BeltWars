using System.Collections;
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
        if (networkManager == null)
            networkManager = FindObjectOfType<NetworkManagerBeltWars>();
        try
        {
            networkManager.StartHost();
            Debug.Log(networkManager.isNetworkActive);
        } catch
        {
            networkManager.StopHost();
        }
    }

    public void StopHost()
    {
        if (networkManager == null)
            return;
        networkManager.StopHost();
        Debug.Log(networkManager.isNetworkActive);
    }
}
