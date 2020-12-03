using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Class which controls the game
public class GameController : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        GameSceneCreator gameSceneCreator = new GameSceneCreator();
        gameSceneCreator.createGameScene();
    }

}
