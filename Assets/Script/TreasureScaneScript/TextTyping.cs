using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTyping : MonoBehaviour
{
    public TMP_Text text;
    string dialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogue = "����� ������";
        StartCoroutine(Typing(dialogue));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Typing(string EveName)
    { 
        text.text = null;
        for (int i = 0; i < EveName.Length; i++)
        {
            text.text += EveName[i];
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

}
