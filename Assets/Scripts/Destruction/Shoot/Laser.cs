using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject loadedWeapon;
    public Weapontype weapontype;
    public int laserAmount = 100;
    public bool active = true;

    


    public enum Weapontype
    {
        MACHINE_GUN,
        MISSILE,
        LASER
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            loadedWeapon = Resources.Load(ResourcePathConstants.LASER) as GameObject;
            weapontype = Weapontype.LASER;
            Debug.Log("Laser loaded");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (weapontype == Weapontype.LASER && laserAmount > 0)
            {
                StartCoroutine(fireLaser(0));
                laserAmount--;
                active = true;
            }

        }

    }

    IEnumerator fireLaser(float delay)
    {
        yield return new WaitForSeconds(delay);

        float projectilePosX = projectileSpawnPoint.position.x;
        float projectilePosY = projectileSpawnPoint.position.y;
        Vector3 direction = transform.rotation * Vector3.up;

        GameObject weaponToFire = ( GameObject )Instantiate(loadedWeapon, projectileSpawnPoint.position, transform.rotation);
        weaponToFire.transform.parent = gameObject.transform;
        LaserBehaviour laserBeh = weaponToFire.GetComponent<LaserBehaviour>();
        laserBeh.ship = gameObject;
    }
}
