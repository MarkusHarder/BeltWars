using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportShipAction : MonoBehaviour
{

    private GameObject item;
    public Vector3 dropPosition;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    private bool posReached = false;
    private bool itemDropped = false;
    private bool targetPosCalc = false;
    private float timer = 1.0f;
    //private float waitTime = 2.0f;

    void Start()
    {
        this.item = Resources.Load(ResourcePathConstants.DROP_ITEM) as GameObject;
        Debug.Log("startPosition: " + startPosition);
        Debug.Log("dropPosition: " + dropPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (dropPosition != null)
        {
            if (!posReached)
            {
                transform.position = Vector2.MoveTowards(transform.position, dropPosition, 4 * Time.deltaTime);

                if (transform.position == dropPosition)
                {
                    this.item = (GameObject)Instantiate(this.item, this.dropPosition, Quaternion.Euler(0, 0, 0));
                    posReached = true;
                }
            }
            else if(posReached && !itemDropped)
            {
                timer -= Time.deltaTime;
                if(timer <= 0) itemDropped = true;
            }
            else if(posReached && itemDropped)
            {
                if(!targetPosCalc)
                {
                    if (startPosition.x == dropPosition.x)
                    {
                        targetPosition = new Vector3(startPosition.x, -(startPosition.y), 0);
                        targetPosCalc = true;
                        Debug.Log("targetPosition: " + targetPosition);
                    }
                    else
                    {
                        targetPosition = new Vector3(-(startPosition.x), startPosition.y, 0);
                        targetPosCalc = true;
                        Debug.Log("targetPosition: " + targetPosition);
                    }
                }
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, 4 * Time.deltaTime);
                if (transform.position == targetPosition) Destroy(this.gameObject);
            }
        }
    }

    public void setDropPosition(Vector2 position)
    {
        dropPosition = position;
    }

    
}
