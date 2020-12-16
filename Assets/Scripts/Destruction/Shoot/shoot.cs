using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public int weapon;
    public GameObject mis;
    public GameObject missile1;
    public GameObject maschine_gun;
    public Transform spawnPoint;
    public float speed;
    public Transform ship;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = ship.rotation * Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            mis = missile1;
            weapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            mis = maschine_gun;
            weapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(peng(0.0f));
            if (weapon == 2)
            {
                StartCoroutine(peng(0.2f));
                StartCoroutine(peng(0.4f));
            }
        }
    }


    IEnumerator peng(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject missile = (GameObject)Instantiate(mis, spawnPoint.position, ship.rotation);
        missile.GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }
}

