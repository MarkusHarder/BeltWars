﻿using System.Collections;
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
        Destroy(gameObject);
        GameObject explosionInstance = GameObject.Instantiate(Resources.Load("Prefabs/Destruction/Explosion") as GameObject, gameObject.transform.position, Quaternion.identity);
    }
}
