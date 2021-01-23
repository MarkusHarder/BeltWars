using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public GameObject loadedWeapon;
    public Transform projectileSpawnPoint;
    private float speed;
    public Weapontype weapontype;
    public bool active = false;
    public int missileAmount = 3;
    public int laserAmount = 0;

    public enum Weapontype
    {
        MACHINE_GUN,
        MISSILE,
        LASER
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
        if (active) 
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
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                loadedWeapon = Resources.Load(ResourcePathConstants.LASER) as GameObject;
                weapontype = Weapontype.LASER;
                Debug.Log("Laser loaded");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (weapontype == Weapontype.MISSILE && missileAmount > 0)
                {
                    StartCoroutine(fire(0.0f));
                    missileAmount--;
                    active = false;
                }
                else if (weapontype == Weapontype.MACHINE_GUN)
                {
                    StartCoroutine(fire(0.0f));
                    StartCoroutine(fire(0.1f));
                    StartCoroutine(fire(0.2f));
                    StartCoroutine(fire(0.3f));
                    StartCoroutine(fire(0.4f));
                    StartCoroutine(fire(0.5f));
                    active = false;
                }else if (weapontype == Weapontype.LASER && laserAmount > 0)
                {
                    StartCoroutine(fireLaser());
                    laserAmount--;
                    active = false;
                }
            }   
        }
    }


    IEnumerator fire(float delay)
    {
        yield return new WaitForSeconds(delay);
      
        Vector3 direction = transform.rotation * Vector3.up;

        GameObject weaponToFire = (GameObject) Instantiate(loadedWeapon, projectileSpawnPoint.position, transform.rotation);
        weaponToFire.GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }

    IEnumerator fireLaser()
    {
        yield return new WaitForSeconds(0);

        Vector3 direction = transform.rotation * Vector3.up;

        GameObject weaponToFire = ( GameObject )Instantiate(loadedWeapon, projectileSpawnPoint.position, transform.rotation);

        LaserBehaviour laser = weaponToFire.GetComponent<LaserBehaviour>();
        laser.ship = gameObject;
    }
}

