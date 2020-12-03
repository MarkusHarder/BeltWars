using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSupportShip : MonoBehaviour
{

    double probability = 0.2;
    private GameObject ship;
    private GameObject item;
    private Vector2 dropLocation;
    private Vector2 startLocation;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hallo");
        this.initiateEvent();
    }

    // Update is called once per frame
    void Update()
    {
        this.moveShipToTarget();
    }


    public void initiateEvent()
    {
        this.ship = Resources.Load(ResourcePathConstants.SUPPORT_SHIP) as GameObject;
        this.item = Resources.Load(ResourcePathConstants.DROP_ITEM) as GameObject;

        this.calculateDropLocation();

        this.placeShip();

    }


    private void calculateDropLocation()
    {
        CameraMeasurements camera = new CameraMeasurements();
        bool collision;
        float borderDistance = 0.75f;

        do
        {
            float x = Random.Range(camera.getHorizontalMin() + borderDistance, camera.getHorizontalMax() - borderDistance);
            float y = Random.Range(camera.getVerticalMin() + borderDistance, camera.getVerticalMax() - borderDistance);

            this.dropLocation = new Vector3(x, y, 0);
            Debug.Log("dropLocation: x=" + x + " y=" + y);

            collision = checkItemCollision();
        } while (collision);

        this.item = (GameObject) Instantiate(this.item, this.dropLocation, Quaternion.Euler(0, 0, 0));
    }


    private void placeShip()
    {
        float outOfViewRange = 3f;
        float x = this.dropLocation.x;
        float y = this.dropLocation.y;
        CameraMeasurements camera = new CameraMeasurements();

        float xb1 = camera.getHorizontalMax() + outOfViewRange;
        float xb2 = camera.getHorizontalMin() - outOfViewRange;
        float yb1 = camera.getVerticalMax() + outOfViewRange;
        float yb2 = camera.getVerticalMin() - outOfViewRange;

        Vector2[] possiblePositions = {
            new Vector2(x, yb1),
            new Vector2(x, yb2),
            new Vector2(xb1, y),
            new Vector2(xb2, y)};

        this.startLocation = possiblePositions[Random.Range(0, 3)];

        Debug.Log("startLocation: x=" + startLocation.x + " y=" + startLocation.y);

        float rotation; 

        if (this.startLocation.x == xb1) {
            rotation = 90;
        } else if (this.startLocation.x == xb2) {
            rotation = 270;
        } else if (this.startLocation.y == yb1) {
            rotation = 180;
        }else{
            rotation = 0;
        }

        this.ship = (GameObject) Instantiate(this.ship, this.startLocation, Quaternion.Euler(0, 0, rotation));
        this.ship.name = "Support_Ship";
    }

    private bool checkItemCollision()
    {
        float radius = this.item.GetComponent<CircleCollider2D>().radius;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.dropLocation, radius, LayerMask.GetMask("Default"));

        if (colliders.Length > 0) return true;
        return false;
    }


    private void moveShipToTarget()
    {
        this.ship.transform.position = Vector2.MoveTowards(transform.position, this.dropLocation, 3 * Time.deltaTime);
    }
}
