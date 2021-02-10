using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIBehaviour: MonoBehaviour
{
    public bool active = false;

    public float moveSpeed;
    public float rotationSpeed;
    
    public Weapontype weapontype;

    public int missileAmount = 2;
    public int laserAmount = 0;


    private Vector3 finalPosition = new Vector3();

    public Transform spawnPointProjectile;
    public GameObject loadedWeapon;

    private Transform enemyToAttack;
    private CameraMeasurements gameCamera;
    
    private bool alreadyShot = false;
    private bool finalPositionDetermined = false;
    private bool roundFinished = false;

    private float timer = 3f;

    private float projectileSpeed;

    public enum Weapontype
    {
        MACHINE_GUN,
        MISSILE,
        LASER
    }


    // Update is called once per frame
    void Update()
    {
        keepObjectInCameraView();

        if (active && !roundFinished) {

            if(!alreadyShot)
            {
                chooseWeapon();
                enemyToAttack = findEnemyToAttack();
                RotateTowards(enemyToAttack.position);

                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    return;
                }

                MoveTowards(enemyToAttack.position);

                Debug.DrawRay(spawnPointProjectile.position, spawnPointProjectile.TransformDirection(Vector2.up) *10f, Color.red);
                RaycastHit2D hit = Physics2D.Raycast(spawnPointProjectile.position, spawnPointProjectile.TransformDirection(Vector2.up), Mathf.Infinity);

                if (hit)
                {
                    if (hit.collider.name.StartsWith("Ship_Earth"))
                    {
                        FindObjectOfType<AudioManager>().Stop("engine1");
                        shoot();
                        alreadyShot = true;
                        timer = 1f;
                    }
                }
            }

            if (alreadyShot)
            {
                //Wait beore mving again, else machingun fire is spreading
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    return;
                }
                moveToFinalPosition();
            }
        }
    }

    private void moveToFinalPosition()
    {
        if (!finalPositionDetermined)
        {
            //Chose Drop Item if exists or random Position
            CameraMeasurements camera = new CameraMeasurements();
            float x = Random.Range(camera.getHorizontalMin() + 0.5f, camera.getHorizontalMax() - 0.5f);
            float y = Random.Range(camera.getVerticalMin() + 0.5f, camera.getVerticalMax() - 0.5f);
            Transform dropItem = dropItemPosition();
            finalPosition = dropItem ? dropItem.position : new Vector3(x, y, 0);
            finalPositionDetermined = true;
        }

        MoveTowards(finalPosition);
        RotateTowards(finalPosition);

        if(transform.position == finalPosition)
        {
            FindObjectOfType<AudioManager>().Stop("engine1");
            finalPositionDetermined = false;
            roundFinished = true;
            timer = 3f;
            alreadyShot = false;
        }
    }

    private Transform dropItemPosition()
    {
        float distanceToClosestItem = Mathf.Infinity;
        Transform closestItem = null;

        GameObject[] allItems = GameObject.FindGameObjectsWithTag("Item");

        foreach (GameObject item in allItems)
        {
            float distanceToItem = (item.transform.position - this.transform.position).sqrMagnitude;

            //Find closest item
            if (distanceToItem < distanceToClosestItem)
            {
                distanceToClosestItem = distanceToItem;
                closestItem = item.transform;
            }
        }
        return closestItem;
    }


    private void chooseWeapon()
    {
        if (laserAmount > 0)
        {
            loadedWeapon = Resources.Load(ResourcePathConstants.LASER) as GameObject;
            weapontype = Weapontype.LASER;
        }
        else if (missileAmount > 0)
        {
            loadedWeapon = Resources.Load(ResourcePathConstants.MISSILE) as GameObject;
            weapontype = Weapontype.MISSILE;
            projectileSpeed = 5;
        }
        else
        {
            loadedWeapon = Resources.Load(ResourcePathConstants.MACHINE_GUN) as GameObject;
            weapontype = Weapontype.MACHINE_GUN;
            projectileSpeed = 1;
        }
    }

    public void shoot()
    {
        if (weapontype == Weapontype.MISSILE)
        {
            FindObjectOfType<AudioManager>().Play("rocket");
            StartCoroutine(fire(0.0f));
            missileAmount--;
        }
        else if (weapontype == Weapontype.MACHINE_GUN)
        {
            FindObjectOfType<AudioManager>().Play("mgun");
            StartCoroutine(fire(0.0f));
            StartCoroutine(fire(0.1f));
            StartCoroutine(fire(0.2f));
            StartCoroutine(fire(0.3f));
            StartCoroutine(fire(0.4f));
            StartCoroutine(fire(0.5f));
        }
        else if (weapontype == Weapontype.LASER)
        {
            FindObjectOfType<AudioManager>().Play("laser");
            StartCoroutine(fireLaser());
            laserAmount--;
        }
    }

    IEnumerator fire(float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 direction = transform.rotation * Vector3.up;

        GameObject weaponToFire = ( GameObject )Instantiate(loadedWeapon, spawnPointProjectile.position, transform.rotation);
        weaponToFire.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed);
    }


    IEnumerator fireLaser()
    {
        yield return new WaitForSeconds(0);

        Vector3 direction = transform.rotation * Vector3.up;

        GameObject weaponToFire = ( GameObject )Instantiate(loadedWeapon, spawnPointProjectile.position, transform.rotation);

        LaserBehaviour laser = weaponToFire.GetComponent<LaserBehaviour>();
        laser.ship = gameObject;
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveTowards(Vector2 target)
    {
        FindObjectOfType<AudioManager>().Play("engine1");
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    public Transform findEnemyToAttack()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        float maxDistanceToAttackDamagedEnemies = 10f;
        Transform closestEnemy = null;
        Transform mostDamagedEnemyNearby = null;
        float minHealth = 100;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            ShipDestruction sd = currentEnemy.GetComponent<ShipDestruction>();
            float health = sd.health;

            //Find closest Enemy
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy.transform;
            }

            //Find most Damaged Enemy within a Radius of 10f
            if (health < minHealth && distanceToEnemy < maxDistanceToAttackDamagedEnemies)
            {
                minHealth = health;
                mostDamagedEnemyNearby = currentEnemy.transform;
            }
        }

        return mostDamagedEnemyNearby ? mostDamagedEnemyNearby : closestEnemy;
    }

    private void keepObjectInCameraView()
    {
        gameCamera = new CameraMeasurements();
        if ((transform.position.x >= gameCamera.getHorizontalMax()))
        {
            transform.position = new Vector3(gameCamera.getHorizontalMax(), transform.position.y, 0);
        }

        if ((transform.position.x <= gameCamera.getHorizontalMin()))
        {
            transform.position = new Vector3(gameCamera.getHorizontalMin(), transform.position.y, 0);
        }

        if ((transform.position.y >= gameCamera.getVerticalMax()))
        {
            transform.position = new Vector3(transform.position.x, gameCamera.getVerticalMax(), 0);
        }

        if ((transform.position.y <= gameCamera.getVerticalMin()))
        {
            transform.position = new Vector3(transform.position.x, gameCamera.getVerticalMin(), 0);
        }
    }

    public void setActive()
    {
        active = true;
        timer = 3f;
        finalPositionDetermined = false;
        roundFinished = false;
        alreadyShot = false;

        //Increase mass to make it possible to move Asteroids more easy
        gameObject.GetComponent<Rigidbody2D>().mass = 1000;
    }

    public void setInActive()
    {
        active = false;
        //Reset mass to usual behaviour
        gameObject.GetComponent<Rigidbody2D>().mass = 30;
    }
}
