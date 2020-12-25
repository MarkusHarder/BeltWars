using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMeasurements : MonoBehaviour
{

    public Camera camera = Camera.main;

    public float getHorizontalMin()
    {
        float halfHeight = this.camera.orthographicSize;
        float halfWidth = this.camera.aspect * halfHeight;

        return -halfWidth;
    }

    public float getHorizontalMax()
    {
        float halfHeight = this.camera.orthographicSize;
        float halfWidth = this.camera.aspect * halfHeight;

        return halfWidth;
    }

    public float getVerticalMin()
    {
        float halfHeight = this.camera.orthographicSize;

        return -halfHeight;
    }

    public float getVerticalMax()
    {
        float halfHeight = this.camera.orthographicSize;

        return halfHeight;
    }

}
