﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkShoot : NetworkBehaviour
{
    Shoot shoot;
    [SyncVar]
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        shoot = gameObject.GetComponent<Shoot>();
        cmdInitWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        checkShoot();
        if (active && hasAuthority)
        {
            evalInput();
        }
    }


    [Command]
    public void cmdInitWeapon()
    {
        shoot.initWeapon();
    }


    [Command]
    public void cmdSetWeapon(int n)
    {
        shoot.setWeapon(n);
    }


    [Command]
    public void cmdShoot()
    {
        shoot.shoot();
    }

    [Server]
    private void checkShoot()
    {
        active = shoot.active;
    }


    public void evalInput()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            cmdSetWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            cmdSetWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Space)) { cmdShoot(); }
    }
}