using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.1f;  // Delay in seconds which waits to print the next letter
    private string fullText;
    private string currentText = "";

    // Start is called before the first frame update
    void Start()
    {
        fullText = this.GetComponent<Text>().text;
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for(int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
