using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Mirror;

//Class which controls the game
public class GameController : MonoBehaviour
{
    public float timer = 0;
    public float roundTime = 20;
    public int eventPropability = 100;
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

    virtual protected void Update()
    {
        if (!checkGameOver())
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else if (!eventIsRunning)
            {
                Debug.Log(eventPropability);
                if (eventAllowed)
                {
                    int i = Random.Range(0, 100);
                    if (i < eventPropability )
                    {
                        ShipContainer.deactivateAllShips();
                        eventIsRunning = true;
                        eventAllowed = false;
                        EventSupportShip sShip = new EventSupportShip();
                        sShip.initiateEvent();
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
        GameObject gameI = GameObject.Find("Game Information");
        if (gameI == null)
            gameI = GameObject.Find("Network Game Information");
        GameInformation gameInfo = gameI.GetComponent<GameInformation>();

        
        

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
