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
    public float borderDistance = 0.5f;



    public void createGameScene()
    {


        this.createBackground();
        this.createBackgroundPlanet();
        this.spawnAsteroids();
        this.spawnShips();
        ShipContainer.printShips();
    }

    public void spawnAsteroids()
    {
        int asteroidAmount = 8 * asteroidDensity;
        for (int i = 0; i < asteroidAmount; i++)
        {
            spawnAsteroidRandom(i);
        }
    }

    public void spawnShips()
    {
        for(int i = 0; i < shipAmount; i++)
        {
            spawnShipRandom(ResourcePathConstants.SHIP_EARTH, i);
            spawnShipRandom(ResourcePathConstants.SHIP_MARS, i);

        }
    }

    public void createBackground()
    {
        int i = Random.Range(0, ResourcePathConstants.BACKGROUND_SCENES.Length-1);
        GameObject backgroundScene = Resources.Load(ResourcePathConstants.BACKGROUND_SCENES[i]) as GameObject;

        if (backgroundScene == null) Debug.Log("Background scene resource is null!");
        
        Instantiate(backgroundScene, new Vector3(0, 0, 0), Quaternion.identity);
    }


    public void createBackgroundPlanet()
    {
        int i = Random.Range(0, ResourcePathConstants.PLANETS.Length - 1);
        GameObject planet = Resources.Load(ResourcePathConstants.PLANETS[i]) as GameObject;

        if (planet == null) Debug.Log("Planet resource is null!");

        CameraMeasurements camera = new CameraMeasurements();

        float x = Random.Range(camera.getHorizontalMin() + borderDistance, camera.getHorizontalMax() - borderDistance);
        float y = Random.Range(camera.getVerticalMin() + borderDistance, camera.getVerticalMax() - borderDistance);

        Vector3 spawnLocation = new Vector3(x, y, 0);
        float rotation = Random.Range(0, 360);

        GameObject clone = (GameObject) Instantiate(planet, spawnLocation, Quaternion.Euler(0, 0, rotation));
        float scale =  Random.Range(0.05f , 0.15f);

        clone.transform.localScale = new Vector3(scale, scale, 0);
        clone.name = "Planet";

        //Applying shadow to planet
        i = Random.Range(0, ResourcePathConstants.PLANET_SHADOWS.Length - 1);
        GameObject shadow = Resources.Load(ResourcePathConstants.PLANET_SHADOWS[i]) as GameObject;

        if (shadow == null) Debug.Log("Planet resource is null!");

        spawnLocation = new Vector3(x, y, 0);
        rotation = Random.Range(0, 360);

        GameObject clone2 = (GameObject) Instantiate(shadow, spawnLocation, Quaternion.Euler(0, 0, rotation));

        clone2.transform.localScale = new Vector3(scale, scale, 0);
        clone2.name = "Shadow";
    }


    private void spawnShipRandom(string path, int index)
    {
        GameObject ship = Resources.Load(path) as GameObject;
        CameraMeasurements camera = new CameraMeasurements();

        if (ship == null) Debug.Log("Ship resource is null!");

        bool collision;
        Vector3 spawnLocation;

        do
        {
            float x = Random.Range(camera.getHorizontalMin() + borderDistance, camera.getHorizontalMax() - borderDistance);
            float y = Random.Range(camera.getVerticalMin() + borderDistance, camera.getVerticalMax() - borderDistance);

            spawnLocation = new Vector3(x, y, 0);

            collision = checkCollision(spawnLocation, ship);
        } while (collision);

        float rotation = Random.Range(0, 360);
        GameObject clone = (GameObject) Instantiate(ship, spawnLocation, Quaternion.Euler(0, 0, rotation));

        if (path.Equals(ResourcePathConstants.SHIP_EARTH))
        {
            clone.name = "Ship_Earth_" + (index + 1);
            ShipContainer.earth.Add(clone);
        }
        else
        {
            clone.name = "Ship_Mars_" + (index + 1);
            ShipContainer.mars.Add(clone);
        }
    }

    private void spawnAsteroidRandom(int index) 
    {
        int i = Random.Range(0, ResourcePathConstants.ASTEROIDS.Length - 1);
        GameObject asteroid = Resources.Load(ResourcePathConstants.ASTEROIDS[i]) as GameObject;

        if (asteroid == null) Debug.Log("Asteroid resource is null!");

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
        GameObject clone = (GameObject) Instantiate(asteroid, spawnLocation, Quaternion.Euler(0, 0, rotation));

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
