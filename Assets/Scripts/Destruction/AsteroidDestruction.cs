using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidDestruction : MonoBehaviour, IEntity
{
    public float health { get; set; }
    public float maxHealth = 1;
    public GameObject asteroid;
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
        Explosion();
    }

    public void Explosion()
    {
        Destroy(gameObject);
        GameObject explosionInstance = GameObject.Instantiate(Resources.Load("Prefabs/Explosion") as GameObject, gameObject.transform.position, Quaternion.identity);
    }
}
