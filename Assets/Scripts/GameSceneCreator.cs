using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Summary description for Class1
/// </summary>
public  class GameSceneCreator : MonoBehaviour
{

    public int shipAmount = 6;
    public int asteroidDensity = 3;
    public double borderDistance = 0.5;


    
    public void createGameScene()
    {
        this.createBackground();
        this.placeAsteroids();
        this.placeShips();
    }

    public void placeAsteroids()
    {
        int asteroidAmount = 8 * asteroidDensity;
        for (int i = 0; i < asteroidAmount; i++)
        {
            placeAsteroidRandom(i);
        }
    }

    public void placeShips()
    {
        for(int i = 0; i < shipAmount; i++)
        {
            placeShipRandom(ResourcePathConstants.SHIP_EARTH, i);
            placeShipRandom(ResourcePathConstants.SHIP_MARS, i);

        }
    }

    public void createBackground()
    {
        int i = Random.Range(0, ResourcePathConstants.BACKGROUND_SCENES.Length-1);
        GameObject backgroundScene = Resources.Load(ResourcePathConstants.BACKGROUND_SCENES[i]) as GameObject;
        if (backgroundScene == null)
        {
            Debug.Log("BAckground scene resource is null!");
        }
        Instantiate(backgroundScene, new Vector3(0, 0, 0), Quaternion.identity);
    }


    private void placeShipRandom(string path, int index)
    {
        GameObject ship = Resources.Load(path) as GameObject;
        CameraMeasurements camera = new CameraMeasurements();

        if (ship == null)
        {
            Debug.Log("Ship resource is null!");
        }

        bool collision;
        Vector3 spawnLocation;

        do
        {
            float x = Random.Range(camera.getHorizontalMin() + (float) borderDistance, camera.getHorizontalMax() - (float) borderDistance);
            float y = Random.Range(camera.getVerticalMin() + (float) borderDistance, camera.getVerticalMax() - (float) borderDistance);

            spawnLocation = new Vector3(x, y, 0);

            collision = checkCollision(spawnLocation, ship);
        } while (collision);

        float rotation = Random.Range(0, 360);
        GameObject clone = (GameObject) Instantiate(ship, spawnLocation, Quaternion.Euler(0, 0, rotation));

        if (path.Equals(ResourcePathConstants.SHIP_EARTH))
        {
            clone.name = "Ship_Earth_" + (index + 1);
        }
        else
        {
            clone.name = "Ship_Mars_" + (index + 1);
        }
    }

    private void placeAsteroidRandom(int index) 
    {
        int i = Random.Range(0, ResourcePathConstants.ASTEROIDS.Length - 1);
        GameObject asteroid = Resources.Load(ResourcePathConstants.ASTEROIDS[i]) as GameObject;

        if (asteroid == null)Debug.Log("Asteroid resource is null!");

        bool collision;
        Vector3 spawnLocation;
        CameraMeasurements camera = new CameraMeasurements();

        do {
            float x = Random.Range(camera.getHorizontalMin(), camera.getHorizontalMax());
            float y = Random.Range(camera.getVerticalMin(), camera.getVerticalMax());

            spawnLocation = new Vector3(x, y, 0);
            collision = checkCollision(spawnLocation, asteroid);

        } while (collision);

        float rotation = Random.Range(0, 360);
        GameObject clone = (GameObject)Instantiate(asteroid, spawnLocation, Quaternion.Euler(0, 0, rotation));

        clone.name = "Asteroid_" + (index + 1);
    }


    private bool checkCollision(Vector3 spawnLocation, GameObject gameObject) 
    {
        float radius = gameObject.GetComponent<CircleCollider2D>().radius;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnLocation, radius, LayerMask.GetMask("Default"));

        if (colliders.Length > 0) return true;
        return false;
    }






}
