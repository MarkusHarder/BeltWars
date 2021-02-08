using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerInformation : MonoBehaviour
{
    public int timeLeft;

    // Update is called once per frame
    private void Start()
    {
        getTime();
    }
    void Update()
    {
        if (GlobalVariables.local)
            getTime();
    }


    public void getTime()
    {
       
        GameController controller = GameObject.FindObjectOfType<GameController>();
        if (controller == null)
            controller = FindObjectOfType<NetworkGameController>();
        timeLeft = (int)controller.timer;
        displayInformation();
      
    }

    public void displayInformation()
    {
        gameObject.GetComponent<Text>().text = "" + timeLeft + " ";
    }
}
