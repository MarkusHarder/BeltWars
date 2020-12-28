using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Class which controls the game
public class GameController : MonoBehaviour
{
    public float timer = 0;
    public float roundTime = 15;
    public int eventPropability = 10;
    public bool eventIsRunning = false;
    public bool eventAllowed = true;


    // Start is called before the first frame update
    void Start()
    {
        GameSceneCreator gameSceneCreator = new GameSceneCreator();
        gameSceneCreator.createGameScene();
    }

    private void Update()
    {
        if (!checkGameOver())
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else if (!eventIsRunning)
            {
                if (eventAllowed)
                {
                    int i = UnityEngine.Random.Range(0, 100 / eventPropability);
                    if (i == 0)
                    {
                        ShipContainer.deactivateAllShips();
                        eventIsRunning = true;
                        eventAllowed = false;
                        new EventSupportShip().initiateEvent();
                    }
                }

                if (!eventIsRunning)
                {
                    ShipContainer.activateNextShip();
                    eventAllowed = true;
                    timer += roundTime;
                }
            }
        }
    }

    public bool checkGameOver() 
    {
        if (ShipContainer.checkIfEarthLost())
        {
            Debug.Log("Earth lost");
            return true;
        }

        if (ShipContainer.checkIfMarsLost())
        {
            Debug.Log("Mars lost");
            return true;
        }
        return false;
    }



}
