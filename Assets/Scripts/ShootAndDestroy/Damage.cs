using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damageValue = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameObject.name.Equals("LaserBeam")) //The Laser should not explode
        {
            Explosion expl = gameObject.GetComponent<Explosion>();
            expl.StartExplosion();
        }
    }
}
