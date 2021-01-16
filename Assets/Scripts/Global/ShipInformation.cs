using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipInformation : MonoBehaviour
{
    public string fraction, currentWeapon, machineGun, missile, laserBeam;


    // Update is called once per frame
    void Update()
    {
        if(GlobalVariables.local)
            displayInformation();
    }


    public void displayInformation()
    {
        GameObject ship = ShipContainer.getActiveShip();

        if (ship)
        {
            fraction = "";
            if (ship.name.StartsWith("Ship_Mars"))
            {
                fraction = " MARS" + "\n";
            }
            else
            {
                fraction = " EARTH" + "\n";
            }

            machineGun = " 1. Machine Gun: Infinite" + "\n";
            missile = " 2. Missiles:" + ship.GetComponent<Shoot>().missileAmount + "\n";
            laserBeam = " 3. LaserBeam: " + ship.GetComponent<Shoot>().laserAmount + "\n";


            currentWeapon = " Loaded: ";

            if (ship.GetComponent<Shoot>().weapontype == Shoot.Weapontype.MACHINE_GUN)
            {
                currentWeapon += "MACHINE GUN";
            }
            else if (ship.GetComponent<Shoot>().weapontype == Shoot.Weapontype.MISSILE)
            {
                currentWeapon += "MISSILE LAUNCHER";
            }
            else if (ship.GetComponent<Shoot>().weapontype == Shoot.Weapontype.LASERBEAM)
            {
                currentWeapon += "LASERBEAM";
            }

            updateText();
        }
       
        
        
    }

    public void updateText()
    {
        gameObject.GetComponent<Text>().text = fraction + currentWeapon + "\n" + machineGun + missile + laserBeam;
    }
}
