using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipDestruction : MonoBehaviour, IEntity
{
    public float health { get; set; }
    public float maxHealth = 10;
    public string bar;
    public Text shipHP;
   // public GameObject ship;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        bar = this.health + "/" + this.maxHealth;
        shipHP = GameObject.Find("Canvas/Ship HP").GetComponent<Text>();
        this.shipHP.text = this.bar;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Missile d = collision.gameObject.GetComponent<Missile>();
        if (d)
        {
            ApplyDamage(d.damage);
        }

    }

    public void ApplyDamage(float points)
    {
        
        health -= points;
        bar = this.health + "/" + this.maxHealth;
        this.shipHP.text = this.bar;
        if (health <= 0)
        {
            //Destroy(gameObject);
            Explosion();
        }
    }

    public void Explosion()
    {
        Explosion expl = gameObject.GetComponent<Explosion>();
        expl.StartExplosion();
        GameObject debrisInstance = GameObject.Instantiate(Resources.Load("Prefabs/Destruction/Debris") as GameObject, gameObject.transform.position, Quaternion.identity);
    }
}
