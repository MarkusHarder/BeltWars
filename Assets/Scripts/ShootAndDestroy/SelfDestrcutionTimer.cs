using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestrcutionTimer : MonoBehaviour
{
    public float timeToSelfDestruction = 3;

    // Update is called once per frame
    void Update()
    {
        if (timeToSelfDestruction > 0)
        {
            timeToSelfDestruction -= Time.deltaTime;
        }
        else if(timeToSelfDestruction <= 0) 
        {
            Destroy(this.gameObject);
        }
    }
}
