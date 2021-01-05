using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private enum ItemType{ MISSILES, HEALTH, LASER };
    private ItemType itemType;
    private string collectInfo;
    bool collisionEntered = false;

    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:
                itemType = ItemType.MISSILES;
                break;
            case 1:
                itemType = ItemType.HEALTH;
                break;
            case 2:
                itemType = ItemType.LASER;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collisionEntered) 
        { 
            if(collision.gameObject.name.StartsWith("Ship"))
            {
                collisionEntered = true;
                Shoot shoot = collision.gameObject.GetComponent<Shoot>();
                ShipDestruction shipDestruction = collision.gameObject.GetComponent<ShipDestruction>();
                switch (itemType)
                {
                    case ItemType.MISSILES:
                        shoot.missileAmount += 5;
                        this.collectInfo = "+5 MISSILES!!";
                        break;
                    case ItemType.HEALTH:
                        shipDestruction.maxHealth = 100;
                        this.collectInfo = "FULL HEALTH!!";
                        break;
                    case ItemType.LASER:
                        shoot.laserAmount += 0;
                        this.collectInfo = "+1 LASER!!";
                        break;
                }

                //StartCoroutine(showCollectInfo(collectInfo));
                //Hide Game Object and don't destroy it. Otherwise the corountine won't work.
                //this.gameObject.GetComponent<Renderer>().enabled = false;
                GameInformation gameInfo = GameObject.Find("Game Information").GetComponent<GameInformation>();
                gameInfo.activate(this.collectInfo);
                Destroy(this.gameObject);
            }
        }
    }


}
