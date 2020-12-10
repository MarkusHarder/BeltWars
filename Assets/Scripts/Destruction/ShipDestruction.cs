using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipDestruction : MonoBehaviour, IEntity
{
    public float health { get; set; }
    public float maxHealth = 100;
    public string bar;
    public Text shipHP;
    public Text damage;
   // public GameObject ship;
    // Start is called before the first frame update
    void Start()
    {
        this.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.ApplyDamage(10);
        }
    }



    public void Initialize()
    {
        this.maxHealth = 100;
        this.health = this.maxHealth;
        this.bar = this.health + "/" + this.maxHealth;
        this.shipHP = gameObject.transform.Find("Canvas").Find("Ship HP").GetComponent<Text>();
        this.damage = gameObject.transform.Find("Canvas").Find("Damage").GetComponent<Text>();
        this.shipHP.text = this.bar;
        this.damage.text = "";
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Missile d = collision.gameObject.GetComponent<Missile>();
        if (d)
        {
            this.ApplyDamage(d.damage);
        }

    }

    public void ApplyDamage(float points)
    {
        
        this.health -= points;
        this.bar = this.health + "/" + this.maxHealth;
        this.shipHP.text = this.bar;
        StartCoroutine(hideDamage(points));
        if (this.health <= 0)
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

    IEnumerator hideDamage(float points)
    {
        this.damage.text = "-" + points;
        yield return new WaitForSeconds(2);
        this.damage.text = "";
    }
}
