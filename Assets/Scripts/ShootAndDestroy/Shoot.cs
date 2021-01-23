using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Shoot : MonoBehaviour
{
    public GameObject loadedWeapon;
    public Transform projectileSpawnPoint;
    private float speed;
    public Weapontype weapontype;
    public bool active = false;
    //Needs to be adjusted via NetworkShoot
    public int missileAmount = 3;
    //this too
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
        initWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.local)
        {
            if (active)
            {
                evalInput();
            }
        }
    }


    IEnumerator fire(float delay)
    {
        yield return new WaitForSeconds(delay);
      
        Vector3 direction = transform.rotation * Vector3.up;

        GameObject weaponToFire = (GameObject) Instantiate(loadedWeapon, projectileSpawnPoint.position, transform.rotation);
        if(!GlobalVariables.local)
            NetworkServer.Spawn(weaponToFire);
        weaponToFire.GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }



    public void shoot()
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
        }
        else if (weapontype == Weapontype.LASER && laserAmount > 0)
        {
            StartCoroutine(fireLaser());
            laserAmount--;
            active = false;
        }
    }


    public void setWeapon(int n)
    {

        if(n == 0)
        {
            loadedWeapon = Resources.Load(ResourcePathConstants.MACHINE_GUN) as GameObject;
            weapontype = Weapontype.MACHINE_GUN;
            speed = 1;
            Debug.Log("Machine Gun loaded");
        }else if(n == 1){
            loadedWeapon = Resources.Load(ResourcePathConstants.MISSILE) as GameObject;
            weapontype = Weapontype.MISSILE;
            speed = 5;
            Debug.Log("Rocket Launcher loaded");
        }

    }

    public void initWeapon()
    {
        GameObject wep = Resources.Load(ResourcePathConstants.MACHINE_GUN) as GameObject;
        loadedWeapon = wep;
        weapontype = Weapontype.MACHINE_GUN;
        speed = 1;
        Debug.Log("Machine Gun loaded");
    }



    public void evalInput()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            setWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            setWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Space)) { shoot(); }
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

