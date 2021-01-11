using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float rotSpeed = 180f;

    float shipBoundaryRadius = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        // Rotate the ship.

        // Grab our rotation quaternion
        Quaternion rot = transform.rotation;

       // Grab the Z euler angle
        float z = rot.eulerAngles.z;

        // Change the Z angle based on input
        z -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

        // Recreate the quaternion
        rot = Quaternion.Euler(0, 0, z);

        // Feed the quaternion into our rotation
        transform.rotation = rot;

        // Move the ship.
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(0, Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime, 0);

        pos += rot * velocity;

        // Screen boundaries
        if (pos.y + shipBoundaryRadius > Camera.main.orthographicSize)
        {
            pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
        }
        if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize)
        {
            pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
        }

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        // Screen boundaries
        if (pos.x + shipBoundaryRadius > widthOrtho)
        {
            pos.x = widthOrtho - shipBoundaryRadius;
        }
        if (pos.x - shipBoundaryRadius < -widthOrtho)
        {
            pos.x = -widthOrtho + shipBoundaryRadius;
        }

        transform.position = pos;

        if (velocity.magnitude >= 0.01)
        {
                        
                FindObjectOfType<AudioManager>().Play("ShipEngine");
        }
        else
        {
            FindObjectOfType<AudioManager>().Stop("ShipEngine");
        }

        // Wenn Waffe ausgewählt wurde, wird durch die Space Taste, der zugehörige Sound abgespielt
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<AudioManager>().Play("RocketLauncher");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            FindObjectOfType<AudioManager>().Play("Laser");
        }




    }




}
