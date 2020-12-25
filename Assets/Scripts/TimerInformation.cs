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
      int timeLeft = (int) GameObject.Find("Game Controller").GetComponent<GameController>().timer;
      this.gameObject.GetComponent<Text>().text = "" + timeLeft + " ";
    }
}
