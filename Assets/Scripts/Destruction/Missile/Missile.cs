using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime*10;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explosion expl = gameObject.GetComponent<Explosion>();
        expl.StartExplosion();
    }
}
