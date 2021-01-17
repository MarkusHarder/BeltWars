using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour
{

    public bool active = false;

    private int segments = 40;
    private float radius = 1.5f;
    
    LineRenderer line;

    private float lineWidth = 0.1f;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.widthMultiplier = lineWidth;
        line.enabled = false;

        line.positionCount = segments + 2;
        line.useWorldSpace = false;
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < (segments + 2); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }

    private void Update()
    {
        if (active)
        {
            StartCoroutine(circleBlink());
            active = false;
        }
    }

    private IEnumerator circleBlink()
    {
        Debug.Log("Hallo");
        line.enabled = true;
        yield return new WaitForSeconds(0.5f);
        line.enabled = false;
        yield return new WaitForSeconds(0.3f);
        line.enabled = true;
        yield return new WaitForSeconds(0.5f);
        line.enabled = false;
        yield return new WaitForSeconds(0.3f);
        line.enabled = true;
        yield return new WaitForSeconds(0.5f);
        line.enabled = false;
    }




}

