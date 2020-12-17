using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMissile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            this.shoot(10);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Vector3 pos = gameObject.transform.position;
            pos.y += 5;
            GameObject ship = GameObject.Instantiate(Resources.Load(ResourcePathConstants.ship) as GameObject, pos, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 pos = gameObject.transform.position;
            pos.y += 5;
            GameObject.Instantiate(Resources.Load(ResourcePathConstants.asteroid) as GameObject as GameObject, pos, Quaternion.identity);
        }
    }

    void shoot(float dmg)
    {
        Vector3 pos = gameObject.transform.position;
        pos.y += 2;
        GameObject missile = GameObject.Instantiate(Resources.Load(ResourcePathConstants.missile) as GameObject as GameObject, pos, Quaternion.identity);
    }
}
