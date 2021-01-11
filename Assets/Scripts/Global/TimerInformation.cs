using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerInformation : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        displayInformation();
    }

    public void displayInformation()
    {
      int timeLeft = (int) GameObject.Find("NetworkGameController").GetComponent<NetworkGameController>().timer;
      gameObject.GetComponent<Text>().text = "" + timeLeft + " ";
    }
}
