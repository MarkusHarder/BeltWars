using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public bool ZoomActive;
    public Vector3[] Target;
    public Camera Cam;
    public float Speed;
    public int ZoomInCamSize;
    public int ZoomOutCamSize;

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;
    }

    public void LateUpdate()
    {
        if (ZoomActive)
        {
            Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, ZoomInCamSize, Speed);
            Cam.transform.position = Vector3.Lerp(Cam.transform.position, Target[1], Speed);
        }
        else
        {
            Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, ZoomOutCamSize, Speed);
            Cam.transform.position = Vector3.Lerp(Cam.transform.position, Target[0], Speed);
        }
    }
}
