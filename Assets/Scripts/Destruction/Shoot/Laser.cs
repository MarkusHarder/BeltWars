using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public BoxCollider2D bc;
    public Transform firePoint;
    public GameObject startVFX;
    public GameObject collideVFX;
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

    }
    void EnableLaser()
    {


        lineRenderer.enabled = true;
        StartCoroutine(boom(1.9f));

        for (int i = 0; i < particles.Count; i++)
            particles[i].Play();
    }

    void UpdateLaser()
    {
              
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude);

        if(hit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
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
    }

    IEnumerator boom(float delay)
    {
        yield return new WaitForSeconds(delay);
        bc.enabled = true;
    }
}
