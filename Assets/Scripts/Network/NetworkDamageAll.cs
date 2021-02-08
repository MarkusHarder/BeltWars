using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkDamageAll : MonoBehaviour
{
    public bool exec = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (exec)
        {
            exec = false;
            dealdamage();
        }
        
    }

    private void dealdamage()
    {
        ShipDestruction dest;
        AsteroidDestruction aDest;
        foreach(GameObject el in gameObject.GetComponent<NetworkGameController>().elements)
        {
            dest = el.GetComponent<ShipDestruction>();


            if (dest)
            {
                dest.ApplyDamage(10);
            }
            else
            {
                aDest = el.GetComponent<AsteroidDestruction>();
                if (aDest)
                {
                    aDest.ApplyDamage(10);
                }
            }
        }
    }
}
