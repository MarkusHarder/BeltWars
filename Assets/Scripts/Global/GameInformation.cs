using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInformation : MonoBehaviour
{
    public string gameInfo;
    public bool executed = false;

    public void activate(string info)
    {
 
        gameInfo = info;
        //if (GlobalVariables.local)
            StartCoroutine(showInfo(gameInfo));
        executed = true;
    }

    private IEnumerator showInfo(string info)
    {
        this.gameObject.GetComponent<Text>().text = info;
        yield return new WaitForSeconds(2);
        this.gameObject.GetComponent<Text>().text = "";

    }

    public void startDisplay(string info) 
    { 
        StartCoroutine(showInfo(info));
    }
}
