using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    public float moveSpeed;
    public float stoppingDistance;


    private Transform closestPlayer;
    private CameraMeasurements gameCamera;

    public float offset;
    public float rotationSpeed;
    private bool isMoving = false;

    float timer = 3f;
    public bool active = false;
    private bool hitAsteroid = false;





    void Start()
    {
        closestPlayer = FindClosestEnemy();

    }

    // Update is called once per frame
    void Update()
    {
        keepObjectInCameraView();

        if (active) {

            closestPlayer = FindClosestEnemy();


            if (Vector2.Distance(transform.position, closestPlayer.position) > stoppingDistance)
            {
                
                RotateTowards(closestPlayer.position);


                if (isMoving)
                {

                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                        return;
                    }


                    MoveTowards(closestPlayer.position);

                }


            }
             if (Vector2.Distance(transform.position, closestPlayer.position) < stoppingDistance)
            {
                
                RotateTowards(closestPlayer.position);
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    return;
                }
                timer = 3f;


                active = false;
            }
            if (Vector2.Distance(transform.position, closestPlayer.position) == stoppingDistance || hitAsteroid == true )
            {                
                transform.position = this.transform.position;
                active = false;
                timer = 3f;
                hitAsteroid = false;
                

            }





        }



    }

    private void RotateTowards(Vector2 target)
    {
        Vector3 direction = closestPlayer.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        
        isMoving = true;
    }

    private void MoveTowards(Vector2 target)
    {
        
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

    }

    public Transform FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Transform closestEnemy = null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy.transform;
            }
        }

        return closestEnemy;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Asteroid"))
        {
            hitAsteroid = true;

            Debug.Log("Hindernis" + hitAsteroid);

        }


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





}
