using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Transform player; // get the player transform, or w/e object you want to limit in a circle
     Vector3 circleCenter; // this is a location that is set to the middle of my world, it will be the center of your circle.



    private float objectWidth;
    private float objectHeigth;

    private void Start()
    {
        circleCenter = player.position;


    }
    void Update()
    {

        float radius = 5f; // this is the range you want the player to move without restriction
        float dist = Vector3.Distance(player.position, circleCenter); // the distance from player current position to the circleCenter

        if (dist > radius)
        {
            Vector3 fromOrigintoObject = player.position - circleCenter;
            fromOrigintoObject *= radius / dist;
            player.position = circleCenter + fromOrigintoObject;
            transform.position = player.position;
        }


    }
}
