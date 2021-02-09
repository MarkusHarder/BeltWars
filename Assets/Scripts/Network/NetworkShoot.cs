using System.Collections;
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
        if (GlobalVariables.local)
        {
            enabled = false;
        }
        else
        {
            shoot = gameObject.GetComponent<Shoot>();
            cmdInitWeapon();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isServer)
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
        Debug.Log("Shooting");
        shoot.shoot();
    }

    [Server]
    private void checkShoot()
    {
        active = shoot.active;
    }


    public void evalInput()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            cmdSetWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            cmdSetWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            cmdSetWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Space)) { cmdShoot(); }
    }
}
