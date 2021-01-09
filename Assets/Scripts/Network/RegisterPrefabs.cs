using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[ExecuteInEditMode]
public class RegisterPrefabs : MonoBehaviour
{

    public bool update = false;
    public bool clear = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            update = false;
            NetworkManager manager = gameObject.GetComponent<NetworkManager>();
            manager.spawnPrefabs.Clear();
            int c = 0;
            GameObject[] rObjects = Resources.LoadAll<GameObject>("");
            foreach(var obt in rObjects)
            {
                if (obt.GetComponent<NetworkIdentity>() != null)
                {
                    manager.spawnPrefabs.Add(obt);
                }
            }

        }
        if (clear)
        {
            NetworkManager manager = gameObject.GetComponent<NetworkManager>();
            clear = false;
            manager.spawnPrefabs.Clear();
        }
    }
}
