using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkAudioHandler : NetworkBehaviour
{
    private bool gameMusic = false;
    [SerializeField]
    public AudioManager audioManager;
    // Start is called before the first frame update
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Start()
    {
        if (!gameMusic && isClientOnly)
        {
            gameMusic = true;
            audioManager.Play("ingame_music");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void handlePlayClientSound(string name)
    {
        if (name.Equals("click_sound"))
        {
            audioManager.networkPlay(name);
            return;
        }
        if (isServer)
            rpcPlayClientSound(name);
        else if (name.Equals("engine1"))
        {
            cmdPlayServerSound(name);
        }
    }
    public void handleStopClientSound(string name)
    {
        if (name.Contains("music"))
        {
            audioManager.networkStop(name);
            return;
        }
        if (isServer)
            rpcStopClientSound(name);
        else if (name.Equals("engine1"))
        {
            cmdStopServerSound(name);
        }
    }

    [ClientRpc]
    public void rpcPlayClientSound(string name)
    {
        audioManager.networkPlay(name);
    }

    [ClientRpc]
    public void rpcStopClientSound(string name)
    {
        Debug.Log("I should stop playing sound!");
        audioManager.networkStop(name);
    }

    [Command]
    public void cmdPlayServerSound(string name)
    {
        rpcPlayClientSound(name);
    }

    [Command]
    public void cmdStopServerSound(string name)
    {
        rpcStopClientSound(name);
    }

}
