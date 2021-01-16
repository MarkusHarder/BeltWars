using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkItemCollector : NetworkBehaviour
{
    ItemCollector col;
    [SyncVar(hook =nameof(setDrop))]
    ItemCollector.ItemType i;
    int count = 0;

    // Start is called before the first frame update
    
    void Start()
    {
        col = gameObject.GetComponent<ItemCollector>();
        if(isServer)
            col.calcDrop();
    }

    // Update is called once per frame
    [Server]
    void Update()
    {
        if (col.calculated)
        {
            col.calculated = false;
            i = col.getItemType();
            gameObject.GetComponent<ItemCollector>().setItemType(i);

        }
    }

    
    private void setDrop(ItemCollector.ItemType oldI, ItemCollector.ItemType newI)
    {
        Debug.Log("got from SyncVar:" + newI);
        gameObject.GetComponent<ItemCollector>().setItemType(newI);
    }


}
