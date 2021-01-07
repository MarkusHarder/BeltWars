using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShipContainer
{
    private static GameObject currentShip;
    private static int currentEarthShipIndex = 0;
    private static int currentMarsShipIndex = 0;

    public static ArrayList marsShips = new ArrayList();
    public static ArrayList earthShips = new ArrayList();

    public static void printShips()
    {
        Debug.Log("Printing ships nowl!");

        foreach (object marsship in marsShips)
        {
            Debug.Log(((GameObject)marsship).name);
        }

        foreach (object earthship in earthShips)
        {
            Debug.Log(((GameObject)earthship).name);
        }
    }

    

    public static void activateNextShip()
    {
        deactivateAllShips();

        if(currentShip == null)
        {
            activateFirstShip();
        }
        else if (currentShip.name.StartsWith("Ship_Earth"))
        {
            incrementMarsShipIndex();
            GameObject ship = ( GameObject ) marsShips[currentMarsShipIndex];
            setShipActive(ship);
        }
        else if(currentShip.name.StartsWith("Ship_Mars"))
        {
            incrementEarthShipIndex();
            GameObject ship;
            ship = ( GameObject )earthShips[currentEarthShipIndex];
            setShipActive(ship);
        }
    }

    private static void activateFirstShip()
    {
        GameObject ship;
        int i = UnityEngine.Random.Range(0, 1);
        if(i == 0)
        {
            ship = ( GameObject )marsShips[0];
            setShipActive(ship);
        }
        else
        {
            ship = ( GameObject )earthShips[0];
            setShipActive(ship);
        }
    }

    private static void setShipActive(GameObject ship) 
    {
        if(ship != null) {
            ship.GetComponent<ProtoMovement>().active = true;
            ship.GetComponent<Shoot>().active = true;
            currentShip = ship;
        }
    }

    private static void setShipInactive(GameObject ship)
    {
        if(ship != null) {
            ship.GetComponent<ProtoMovement>().active = false;
            ship.GetComponent<Shoot>().active = false;
        }
    }

    private static void incrementEarthShipIndex()
    {
        if (currentEarthShipIndex + 1 > earthShips.Count - 1)
        {
            currentEarthShipIndex = 0;
        }
        else
        {
            currentEarthShipIndex++;
        }
    }

    private static void incrementMarsShipIndex()
    {
        if (currentMarsShipIndex + 1 > marsShips.Count - 1)
        {
            currentMarsShipIndex = 0;
        }
        else
        {
            currentMarsShipIndex++;
        }
    }

    public static void deactivateAllShips()
    {
        foreach(GameObject ship in marsShips)
        {
            setShipInactive(ship);
        }
        foreach (GameObject ship in earthShips)
        {
            setShipInactive(ship);
        }
    }

    public static bool checkIfMarsLost()
    {
        if (marsShips.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool checkIfEarthLost()
    {
        if(earthShips.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public static GameObject getActiveShip()
    {
        foreach(GameObject ship in marsShips)
        {
            if (ship && ship.GetComponent<ProtoMovement>().active)
            {
                return ship;
            }
        }
        foreach (GameObject ship in earthShips)
        {
            if (ship && ship.GetComponent<ProtoMovement>().active)
            {
                return ship;
            }
        }
        return null;
    }


    public static void removeShipFromList(GameObject ship)
    {
        if (ship.name.StartsWith("Ship_Mars"))
        {
            marsShips.Remove(ship);
        }
        else if (ship.name.StartsWith("Ship_Earth"))
        {
            earthShips.Remove(ship);
        }
    }

}
