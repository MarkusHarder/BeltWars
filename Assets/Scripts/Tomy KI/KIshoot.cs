using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KIshoot : MonoBehaviour
{
    public GameObject loadedWeapon;
    public Transform projectileSpawnPoint;
    private float speed;
    public Weapontype weapontype;
    public bool active = false;
    public int missileAmount = 3;
    public int laserAmount = 0;


    public Transform player;
    private bool arrive;
    public float stoppingDistance;
    private bool hitAsteroid = false;
    


    public enum Weapontype
    {
        MACHINE_GUN,
        MISSILE,
        LASER
     }
    //Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<EnemyAI>().FindClosestEnemy();

        //loadedWeapon = Resources.Load(ResourcePathConstants.MISSILE) as GameObject;
        //weapontype = Weapontype.MISSILE;
        //speed = 5;
        //arrive = true; // Ship arrived
        //Debug.Log("Rocket Launcher loaded");
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            player = FindObjectOfType<EnemyAI>().FindClosestEnemy();

            if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
            {
                if (GetComponent<EnemyAI>().active == false)
                {
                    loadedWeapon = Resources.Load(ResourcePathConstants.MACHINE_GUN) as GameObject;
                    weapontype = Weapontype.MACHINE_GUN;
                    speed = 1;
                    arrive = true; // Ship arrived
                    Debug.Log("Machine Gun loaded");
                }
            }

            if ((Vector2.Distance(transform.position, player.position) >= stoppingDistance && missileAmount > 0) || hitAsteroid == true && missileAmount > 0)
            {

                if (GetComponent<EnemyAI>().active == false)
                {
                    loadedWeapon = Resources.Load(ResourcePathConstants.MISSILE) as GameObject;
                    weapontype = Weapontype.MISSILE;
                    speed = 5;
                    arrive = true; // Ship arrived
                    Debug.Log("Rocket Launcher loaded");
                }

            }

            if ((Vector2.Distance(transform.position, player.position) >= stoppingDistance && missileAmount == 0 ) || hitAsteroid == true && missileAmount == 0 )
            {
                if (GetComponent<EnemyAI>().active == false)
                {
                    loadedWeapon = Resources.Load(ResourcePathConstants.MACHINE_GUN) as GameObject;
                    weapontype = Weapontype.MACHINE_GUN;
                    speed = 1;
                    arrive = true; // Ship arrived
                    Debug.Log("Machine Gun loaded");
                }
            }






                if (arrive)
            {
                if (weapontype == Weapontype.MISSILE && missileAmount > 0)
                {
                    StartCoroutine(fire(0.0f));
                    missileAmount--;
                    arrive = false;
                    active = false;
 
                    hitAsteroid = false;
                }
                 if (weapontype == Weapontype.MACHINE_GUN)
                {
                    StartCoroutine(fire(0.0f));
                    StartCoroutine(fire(0.1f));
                    StartCoroutine(fire(0.2f));
                    StartCoroutine(fire(0.3f));
                    StartCoroutine(fire(0.4f));
                    StartCoroutine(fire(0.5f));
                    active = false;
                    arrive = false;

                    hitAsteroid = false;
                }


            
            }
            
        }
    }


    IEnumerator fire(float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 direction = transform.rotation * Vector3.up;

        GameObject weaponToFire = (GameObject)Instantiate(loadedWeapon, projectileSpawnPoint.position, transform.rotation);
        weaponToFire.GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Asteroid"))
        {
            hitAsteroid = true;
            Debug.Log("Hindernis" + hitAsteroid);
        }

    }

}

