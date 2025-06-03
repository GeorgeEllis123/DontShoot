using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class TextManager : MonoBehaviour
{
    public TMP_Text textbox;
    public float speed = .1f;
    public AudioSource myAS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //textbox = FindAnyObjectByType<TextMeshProUGUI>();
        myAS = GetComponent<AudioSource>();
        PrintText("This is a test message");
    }

    public void PrintText(string fullText)
    {
        StartCoroutine(Typewriter(textbox, fullText, speed));
        Invoke("ClearText", 10f);
    }

    IEnumerator Typewriter(TMP_Text textbox, string fullText, float speed)
    {
        string currentText = "";
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i];
            textbox.text = currentText;
            myAS.Play();
            yield return new WaitForSeconds(speed);
        }
    }

    public void ClearText()
    {
        textbox.text = "";
    }
}
