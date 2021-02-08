using System.Collections;
using System.Collections.Generic;
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
        string ipAddress = ipAddressInputField.text;
        if (string.IsNullOrEmpty(ipAddress))
        {
            return;
        }
        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();
        joinButton.interactable = false;
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


    public void QuitLobby()
    {
        networkManager.StopClient();
        joinButton.interactable = true;
        SceneManager.LoadScene("Menu");
    }
}
