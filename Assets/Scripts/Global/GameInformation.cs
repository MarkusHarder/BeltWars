using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInformation : MonoBehaviour
{

    public void activate(string info)
    {
        StartCoroutine(showInfo(info));
    }

    IEnumerator showInfo(string info)
    {
        this.gameObject.GetComponent<Text>().text = info;
        yield return new WaitForSeconds(2);
        this.gameObject.GetComponent<Text>().text = "";

    }
}
