using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class NetworkEndSceneHandler : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadSceneOnClient(string name)
    {
        if (isServer)
        {
            rpcSetScene(name);
        }
    }



    [ClientRpc]
    public void rpcSetScene(string name)
    {
        GlobalVariables.local = true;
        SceneManager.LoadScene(name);
    }
}
