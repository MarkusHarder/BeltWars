using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSupportShip : MonoBehaviour
{

    double probability = 0.2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void runEvent()
    {
        GameObject ship = Resources.Load(ResourcePathConstants.SUPPORT_SHIP) as GameObject;
        GameObject item = Resources.Load(ResourcePathConstants.DROP_ITEM) as GameObject;

        Vector3 dropLocation = getDropLocation(item);

        placeShip(ship);
    }


    private Vector3 getDropLocation(GameObject item)
    {
        CameraMeasurements camera = new CameraMeasurements();
        bool collision;
        Vector3 dropLocation;
        float borderDistance = 0.75f;

        do
        {
            float x = Random.Range(camera.getHorizontalMin() + borderDistance, camera.getHorizontalMax() - borderDistance);
            float y = Random.Range(camera.getVerticalMin() + borderDistance, camera.getVerticalMax() - borderDistance);

            dropLocation = new Vector3(x, y, 0);

            collision = checkCollision(spawnLocation, item);
        } while (collision);

        return dropLocation;
    }


    private void placeShip(GameObject ship, float x, float y)
    {
        float outOfViewRange = 3f;

        float xb1 = camera.getHorizontalMax() + outOfViewRange;
        float xb2 = -(camera.getHorizontalMax() + outOfViewRange);
        float yb1 = camera.getHorizontalMin() - outOfViewRange;
        float yb2 = -(camera.getHorizontalMin() - outOfViewRange);

        Vector3[] possiblePositions = {
            new Vector3(x, yb1),
            new Vector3(x, yb2),
            new Vector3(xb1, y),
            new Vector3(xb2, y)};

        Vector3 spawnLocation = possiblePositions[Random.Range(0, 3)];
        float rotation; 

        if (finalPosition.x == xb1) {
            rotation = 270;
        } else if (finalPosition.x == xb2) {
            rotation = 90;
        } else if (finalPosition.y == yb1) {
            rotation = 180;
        }else{
            rotation = 0;
        }

        GameObject clone = (GameObject) Instantiate(ship, spawnLocation, Quaternion.Euler(0, 0, rotation));
        clone.name = "Support_Ship";
    }

    private bool checkCollision(Vector3 spawnLocation, GameObject gameObject)
    {
        float radius = gameObject.GetComponent<CircleCollider2D>().radius;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnLocation, radius, LayerMask.GetMask("Default"));

        if (colliders.Length > 0) return true;
        return false;
    }
}
