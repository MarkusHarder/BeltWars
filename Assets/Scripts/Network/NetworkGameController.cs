using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkGameController : GameController
{
    public bool start = false;
    public bool networkObj = true;
    public NetworkConnection conn;
    bool turn = true;
    public List<GameObject> elements;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.local = false;
        shipNumber = GlobalVariables.numOfShips;
        asteroidDensity = GlobalVariables.asteroidDensity;

    }

    // Update is called once per frame
    [Server]
    override protected void Update()
    {
        if(!start) { return; }
        base.Update();

    }

    public IEnumerator setAuth()
    {
        if (NetworkServer.localClientActive)
        {
            FindObjectOfType<NetworkAudioHandler>().cmdStopServerSound("engine1");
            FindObjectOfType<AudioManager>().Stop("engine1");
            List<GameObject> destObj = new List<GameObject>();
            Debug.Log("AuthCount" + ++count);
            foreach (GameObject el in elements)
            {
                if (el == null)
                {
                    destObj.Add(el);
                }
                else
                {
                    Rigidbody2D tmpR = el.GetComponent<Rigidbody2D>();
                    NetworkIdentity tmpNI = el.GetComponent<NetworkIdentity>();
                    tmpNI.RemoveClientAuthority();
                    if (el.name.Contains("Asteroid") ||el.name.Contains("Ship"))
                    {
                        tmpR.angularVelocity = 0f;
                        tmpR.velocity = Vector2.zero;
                        tmpNI.RemoveClientAuthority();
                        if (turn)
                            tmpNI.AssignClientAuthority(conn);
                        else
                            tmpNI.AssignClientAuthority(NetworkServer.localConnection);

                    }
                }
            }




            turn = !turn;
            StartCoroutine(remnoveNull(destObj));
            yield return null;
        }
    }

    IEnumerator remnoveNull(List<GameObject> remEls)
    {
        foreach (GameObject unit in remEls)
        {
            elements.Remove(unit);
        }
        yield return new WaitForEndOfFrame();
    }

}


