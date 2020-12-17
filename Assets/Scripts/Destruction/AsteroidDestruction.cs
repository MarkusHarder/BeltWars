using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidDestruction : MonoBehaviour, IEntity
{
    public float health { get; set; }
    public float maxHealth = 20;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            this.ApplyDamage(10);
        }
    }

    private void Awake()
    {
        this.Initialize();
    }


    public void Initialize()
    {
        this.health = this.maxHealth;
    }

    public void ApplyDamage(float points)
    {
        //Destroy(gameObject);
        this.health -= points;
        if(health >= 0)
        {
            Explosion();
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Damage damage = collision.gameObject.GetComponent<Damage>();
        if (damage)
        {
            ApplyDamage(damage.damageValue);
        }

    }

    public void Explosion()
    {
        Explosion expl = gameObject.GetComponent<Explosion>();
        expl.StartExplosion();
    }
}
