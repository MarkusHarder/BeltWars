using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damageValue = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explosion expl = gameObject.GetComponent<Explosion>();
        expl.StartExplosion();
    }
}
