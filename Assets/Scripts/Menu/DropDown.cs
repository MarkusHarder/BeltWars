using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDown : MonoBehaviour
{
    private string asteroidDensity = "low";
    private int numOfShips = 1;

    public void HandleAsteroidDensity(int val)
    {
        if (val == 0)
        {
            this.asteroidDensity = "low";
        } 
        else if (val == 1)
        {
            this.asteroidDensity = "middle";
        }
        else if (val == 2)
        {
            this.asteroidDensity = "high";
        }
    }

    public void HandleNumOfShips(int val)
    {
        if (val == 0)
        {
            this.numOfShips = 1;
        }
        else if (val == 1)
        {
            this.numOfShips = 2;
        }
        else if (val == 2)
        {
            this.numOfShips = 3;
        }
        else if (val == 3)
        {
            this.numOfShips = 4;
        }
        else if (val == 4)
        {
            this.numOfShips = 5;
        }
        else if (val == 5)
        {
            this.numOfShips = 6;
        }
    }

    public string getAsteroidDensity()
    {
        return this.asteroidDensity;
    }

    public int getNumOfShips()
    {
        return this.numOfShips;
    }
}
