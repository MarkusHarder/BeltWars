using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShipContainer
{
    private static GameObject currentShip;
    private static int nextEarthShipIndex = 0;
    private static int nextMarsShipIndex = 0;

    public static ArrayList mars = new ArrayList();
    public static ArrayList earth = new ArrayList();

    public static void printShips()
    {
        Debug.Log("Printing ships nowl!");

        foreach (object marsship in mars)
        {
            Debug.Log(((GameObject)marsship).name);
        }

        foreach (object earthship in earth)
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
        else
        {
            if (currentShip.name.StartsWith("Ship_Earth"))
            {
                GameObject ship;
                do
                {
                  if(nextMarsShipIndex >= mars.Count - 1)
                    {
                        nextMarsShipIndex = 0;
                    }
                  ship = ( GameObject )mars[nextMarsShipIndex++];
                } while (ship == null);
     
                setShipActive(ship);
            }else if(currentShip.name.StartsWith("Ship_Mars"))
            {
                GameObject ship;
                do
                {
                    if (nextEarthShipIndex >= earth.Count - 1)
                    {
                        nextEarthShipIndex = 0;
                    }
                    ship = ( GameObject )earth[nextEarthShipIndex++];
                } while (ship == null);
                
                setShipActive(ship);
            }
        }
    }

    private static void activateFirstShip()
    {
        GameObject ship;
        int i = UnityEngine.Random.Range(0, 1);
        if(i == 0)
        {
            ship = ( GameObject )mars[0];
            setShipActive(ship);
            nextMarsShipIndex++;
        }
        else
        {
            ship = ( GameObject )earth[0];
            setShipActive(ship);
            nextEarthShipIndex++;
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

    public static void deactivateAllShips()
    {
        foreach(GameObject ship in mars)
        {
            setShipInactive(ship);
        }
        foreach (GameObject ship in earth)
        {
            setShipInactive(ship);
        }
    }

    public static bool checkIfMarsLost()
    {
        
        foreach(GameObject ship in mars)
        {
            if(ship != null) return false;
        }
        return true;
    }

    public static bool checkIfEarthLost()
    {

        foreach (GameObject ship in earth)
        {
            if (ship != null) return false;
        }
        return true;
    }


    public static GameObject getActiveShip()
    {
        foreach(GameObject ship in mars)
        {
            if (ship && ship.GetComponent<ProtoMovement>().active)
            {
                return ship;
            }
        }
        foreach (GameObject ship in earth)
        {
            if (ship && ship.GetComponent<ProtoMovement>().active)
            {
                return ship;
            }
        }
        return null;
    }

}
