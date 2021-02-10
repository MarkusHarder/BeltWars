using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SupportShipAction : NetworkBehaviour
{

    private GameObject item;
    public Vector3 dropPosition;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    private bool posReached = false;
    private bool itemDropped = false;
    private bool targetPosCalc = false;
    private float timer = 1.0f;
    private bool soundPlayed = false;
 

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
        if (GlobalVariables.local)
        {
            moveShip();
        }
    }

    public void setDropPosition(Vector2 position)
    {
        dropPosition = position;
    }

    public void moveShip()
    {

        if (dropPosition != null)
        {
            if (!posReached)
            {
                FindObjectOfType<AudioManager>().Play("engine3");
                transform.position = Vector2.MoveTowards(transform.position, dropPosition, 4 * Time.deltaTime);

                if (transform.position == dropPosition)
                {
                    this.item = (GameObject)Instantiate(this.item, this.dropPosition, Quaternion.Euler(0, 0, 0));
                    if (!GlobalVariables.local)
                        NetworkServer.Spawn(item);
                    posReached = true;
                    FindObjectOfType<AudioManager>().Stop("engine3");
                }
            }
            else if (posReached && !itemDropped)
            {
                timer -= Time.deltaTime;
                if (timer <= 0) itemDropped = true;
                if (!soundPlayed)
                {
                    FindObjectOfType<AudioManager>().Play("drop_item");
                    soundPlayed = true;
                }
            }
            else if (posReached && itemDropped)
            {
                FindObjectOfType<AudioManager>().Play("engine3");
                if (!targetPosCalc)
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
                if (transform.position == targetPosition)
                {
                    GameObject gameController = GameObject.Find("Game Controller");
                    if (gameController == null)
                        gameController = GameObject.Find("NetworkGameController");
                    gameController.GetComponent<GameController>().eventIsRunning = false;
                    Destroy(this.gameObject);
                    FindObjectOfType<AudioManager>().Stop("engine3");
                }
            }
        }

    }
}
