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
        networkManager.StopHost();
        Debug.Log(networkManager.isNetworkActive);
    }
}
