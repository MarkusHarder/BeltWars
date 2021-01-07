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
    public int shipNumber = 6;
    public int asteroidDensity = 3;


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
                    int i = Random.Range(0, 100 / eventPropability);
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
        GameInformation gameInfo = GameObject.Find("Game Information").GetComponent<GameInformation>();
        
        if (ShipContainer.checkIfEarthLost())
        {
            gameInfo.activate("MARS WINS!!");
            return true;
        }

        if (ShipContainer.checkIfMarsLost())
        {
            gameInfo.activate("EARTH WINS!!");
            return true;
        }
        return false;
    }
    



}
