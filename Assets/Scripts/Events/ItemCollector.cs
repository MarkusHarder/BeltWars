using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ItemCollector : MonoBehaviour
{
    public enum ItemType{ MISSILES, HEALTH, LASER };
    public ItemType itemType;
    private string collectInfo;
    public bool collisionEntered = false;
    public bool calculated = false;

    // Start is called before the first frame update

    void Start()
    {
        if(GlobalVariables.local)
            calcDrop();
    }

    //Only Server should do this!

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collisionEntered) 
        { 
            if(collision.gameObject.name.StartsWith("Ship"))
            {
                collisionEntered = true;
                Shoot shoot = collision.gameObject.GetComponent<Shoot>();
                ShipDestruction shipDestruction = collision.gameObject.GetComponent<ShipDestruction>();

                //StartCoroutine(showCollectInfo(collectInfo));
                //Hide Game Object and don't destroy it. Otherwise the corountine won't work.
                //this.gameObject.GetComponent<Renderer>().enabled = false;
                switch (itemType)
                {
                    case ItemType.MISSILES:
                        shoot.missileAmount += 3;
                        this.collectInfo = "+3 MISSILES!!";
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
                GameObject gameInfo = GameObject.Find("Display Game Information");
                GameInformation gameInfoComponent = gameInfo.GetComponent<GameInformation>();
                gameInfoComponent.activate(this.collectInfo);
                Destroy(this.gameObject);
            }
        }
    }


    public void calcDrop()
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
            calculated = true;
        
    }


    public ItemType getItemType()
    {
        return itemType;
    }


    public void setItemType(ItemType i)
    {
        this.itemType = i;
        Debug.Log("Set item to" + i);
    }
}
