using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public BoxCollider2D bc;
    public Transform firePoint;
    public GameObject startVFX;
    public Vector2 direction;

    private List<ParticleSystem> particles = new List<ParticleSystem>();

    // Start is called before the first frame update
    void Start()
    {
        FillLists();
        DisableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            EnableLaser();
            UpdateLaser();
            StartCoroutine(peng(2f));
        }

        //if (Input.GetButton("Fire1"))
        //{
        //    UpdateLaser();
        //}
        //if (Input.GetButtonUp("Fire1"))
        //{
        //    DisableLaser();
        //}
    }
    void EnableLaser()
    {


        lineRenderer.enabled = true;
        bc.enabled = true;

        for (int i = 0; i < particles.Count; i++)
            particles[i].Play();
    }

    void UpdateLaser()
    {
      
        lineRenderer.SetPosition(-5, firePoint.position);
        
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude);
    }

    void DisableLaser()
    {
        lineRenderer.enabled = false;
        bc.enabled = false;
        for (int i = 0; i < particles.Count; i++)
            particles[i].Stop();
    }

    void FillLists()
    {
        for(int i=0; i<startVFX.transform.childCount; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
                particles.Add(ps);
        }
    }

    IEnumerator peng(float delay)
    {
        yield return new WaitForSeconds(delay);
        DisableLaser();
        // GameObject missile = (GameObject)Instantiate(mis, spawnPoint.position, ship.rotation);
        //missile.GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }
}
