using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class NetworkShipInformation : NetworkBehaviour
{
    ShipInformation info;
    // Start is called before the first frame update
    [SyncVar]
    public string fraction, currentWeapon, machineGun, missile, laserBeam;
    void Start()
    {
        info = gameObject.GetComponent<ShipInformation>();
    }

    // Update is called once per frame
    [Server]
    void Update()
    {

        infoDisplay();

    }
    [Server]
    private void infoDisplay()
    {
        info.displayInformation();
        fraction = info.fraction;
        currentWeapon = info.currentWeapon;
        machineGun = info.machineGun;
        missile = info.missile;
        laserBeam = info.laserBeam;
        rpcInfo();
    }

    [ClientRpc]
    private void rpcInfo()
    {
        gameObject.GetComponent<Text>().text = fraction + currentWeapon + "\n" + machineGun + missile + laserBeam;
    }
}
