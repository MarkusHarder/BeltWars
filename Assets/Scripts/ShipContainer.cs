using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShipContainer
{
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
}
