using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables 
{
    public static bool local = true;
    public static bool singlePlayer = false;
    public static bool selectedMP = false;
    public static int asteroidDensity = 1;
    public static int numOfShips = 1;
    public static float musicVol = 1;
    public static float effectsVol = 1;
    public static bool musicEnabled = true;
    public static bool effectsEnabled = true;

    public static void updateGameSettings(int density, int ships)
    {
        asteroidDensity = density;
        numOfShips = ships;
    }
}
