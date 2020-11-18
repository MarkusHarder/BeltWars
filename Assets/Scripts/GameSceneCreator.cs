using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Summary description for Class1
/// </summary>
public  class GameSceneCreator : MonoBehaviour
{
    
    public void createGameScene()
    {
        this.createBackground();
        this.placeAsteroids();
        this.placeShips();
    }

    public void placeAsteroids()
    {

    }

    public void placeShips()
    {

    }

    public void createBackground()
    {

        //int i = new Random().Next(0, ResourcePathConstants.BACKGROUNDSCENES.Length);
        int i = Random.Range(0, ResourcePathConstants.BACKGROUNDSCENES.Length-1);
        GameObject backgroundScene = Resources.Load(ResourcePathConstants.BACKGROUNDSCENES[i]) as GameObject;
        if (backgroundScene == null)
        {
            Debug.Log("Resource is null!");
        }
        Instantiate(backgroundScene, new Vector3(0, 0, 0), Quaternion.identity);
    }

}
