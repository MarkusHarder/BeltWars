using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using UnityEngine.SceneManagement;

public class NetworkJoinMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerBeltWars networkManager = null;
    [SerializeField] private GameObject networkMenu = null;

    [Header("UI")]
    [SerializeField] private TMP_InputField ipAddressInputField = null;
    [SerializeField] private Button joinButton = null;
    [SerializeField] private GameObject cancelButton = null;


    private void OnEnable()
    {
        NetworkManagerBeltWars.OnClientConnected += HandleClientConnected;
        NetworkManagerBeltWars.OnClientDisconnected += HandleClientDisconnected;
        NetworkManagerBeltWars.OnGameStarted += HandleGameStart;
    }

    private void OnDisable()
    {
        NetworkManagerBeltWars.OnClientConnected -= HandleClientConnected;
        NetworkManagerBeltWars.OnClientDisconnected -= HandleClientDisconnected;
        NetworkManagerBeltWars.OnGameStarted -= HandleGameStart;
    }

    public void JoinLobby()
    {
        try
        {
            string ipAddress = ipAddressInputField.text;
            if (string.IsNullOrEmpty(ipAddress))
            {
                return;
            }
            networkManager.networkAddress = ipAddress;
            networkManager.StartClient();
            joinButton.interactable = false;
        } catch
        {
            ipAddressInputField.text = "Enter a valid IP!";
            networkManager.networkAddress = "";
            networkManager.StopClient();
            joinButton.interactable = true;
            cancelButton.SetActive(false);
        }
    }

    private void HandleClientConnected()
    {
        joinButton.interactable = true;
        gameObject.SetActive(false);
        networkMenu.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }

    private void HandleGameStart()
    {
    }


    public void CancelLobby()
    {
        networkManager.StopClient();
        joinButton.interactable = true;
    }
    public void QuitLobby()
    {
        networkManager.StopClient();
        joinButton.interactable = true;
        SceneManager.LoadScene("Menu");
    }
}
