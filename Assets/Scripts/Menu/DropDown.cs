using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDown : MonoBehaviour
{
    public void HandleAsteroidDensity(int val)
    {
        if (val == 0)
        {
            GlobalVariables.asteroidDensity = 1;
        } 
        else if (val == 1)
        {
            GlobalVariables.asteroidDensity = 3;
        }
        else if (val == 2)
        {
            GlobalVariables.asteroidDensity = 5;
        }
    }

    public void HandleNumOfShips(int val)
    {
        if (val == 0)
        {
            GlobalVariables.numOfShips = 1;
        }
        else if (val == 1)
        {
            GlobalVariables.numOfShips = 2;
        }
        else if (val == 2)
        {
            GlobalVariables.numOfShips = 3;
        }
        else if (val == 3)
        {
            GlobalVariables.numOfShips = 4;
        }
        else if (val == 4)
        {
            GlobalVariables.numOfShips = 5;
        }
        else if (val == 5)
        {
            GlobalVariables.numOfShips = 6;
        }
    }
}
