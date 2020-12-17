using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    private GameObject loadedWeapon;
    public Transform projectileSpawnPoint;
    private float speed;
    private Weapontype weapontype;

    private enum Weapontype
    {
        MACHINE_GUN,
        MISSILE,
        LASERBEAM
     }
    // Start is called before the first frame update
    void Start()
    {
        
        loadedWeapon = Resources.Load(ResourcePathConstants.MACHINE_GUN) as GameObject;
        weapontype =  Weapontype.MACHINE_GUN;
        speed = 1;
        Debug.Log("Machine Gun loaded");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            loadedWeapon = Resources.Load(ResourcePathConstants.MACHINE_GUN) as GameObject;
            weapontype = Weapontype.MACHINE_GUN;
            speed = 1;
            Debug.Log("Machine Gun loaded");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            loadedWeapon = Resources.Load(ResourcePathConstants.MISSILE) as GameObject;
            weapontype = Weapontype.MISSILE;
            speed = 5;
            Debug.Log("Rocket Launcher loaded");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (weapontype == Weapontype.MISSILE || weapontype == Weapontype.MACHINE_GUN)
            {
                StartCoroutine(fire(0.0f));
                if (weapontype == Weapontype.MACHINE_GUN)
                {
                    StartCoroutine(fire(0.1f));
                    StartCoroutine(fire(0.2f));
                    StartCoroutine(fire(0.3f));
                }
            }
        }
    }


    IEnumerator fire(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        float projectilePosX = projectileSpawnPoint.position.x;
        float projectilePosY = projectileSpawnPoint.position.y;
        Vector3 direction = transform.rotation * Vector3.up;

        GameObject weaponToFire = (GameObject) Instantiate(loadedWeapon, projectileSpawnPoint.position, transform.rotation);
        weaponToFire.GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }

}

