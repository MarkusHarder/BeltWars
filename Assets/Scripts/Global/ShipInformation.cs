using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipInformation : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        displayInformation();
    }


    public void displayInformation()
    {
        GameObject ship = ShipContainer.getActiveShip();

        if (ship)
        {

            string machineGun = " 1. Machine Gun: Infinite" + "\n";
            string missile = " 2. Missiles:" + ship.GetComponent<Shoot>().missileAmount + "\n";
            string laserBeam = " 3. LaserBeam: " + ship.GetComponent<Shoot>().laserAmount + "\n";


            string currentWeapon = " Loaded: ";

            if (ship.GetComponent<Shoot>().weapontype == Shoot.Weapontype.MACHINE_GUN)
            {
                currentWeapon += "MACHINE GUN";
            }
            else if (ship.GetComponent<Shoot>().weapontype == Shoot.Weapontype.MISSILE)
            {
                currentWeapon += "MISSILE LAUNCHER";
            }
            else if (ship.GetComponent<Shoot>().weapontype == Shoot.Weapontype.LASER)
            {
                currentWeapon += "LASERBEAM";
            }

            this.gameObject.GetComponent<Text>().text = currentWeapon + "\n" + machineGun + missile + laserBeam;
        }
       
        
        
    }
}
