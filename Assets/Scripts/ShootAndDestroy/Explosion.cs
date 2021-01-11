using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartExplosion()
    {
        if (gameObject.name.StartsWith("Ship"))
        {
            ShipContainer.removeShipFromList(gameObject);
        }

        Destroy(gameObject);

        GameObject explosion = Resources.Load(ResourcePathConstants.explosion) as GameObject;
        GameObject.Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
    }


}
