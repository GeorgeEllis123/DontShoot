using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class TextManager : MonoBehaviour
{
    public TMP_Text textbox;
    public float speed = .1f;
    public AudioSource pigeonAS;
    public AudioSource clickAS;
    public AudioSource spinAS;

    public string[] messages = {
    "Hello there, old friend.",
    "You may not remember me after all these years...",
    "But our paths have crossed on busy city streets.",
    "You watched as I starved...",
    "But now it is my turn to watch as you die!",
    "Well, it will be, soon enough.",
    "First, I want to play a little game with you.",
    "You are familiar with Russian Roulette, coo-rrect?",
    "I am going to load a pattern of regular bullets and blanks.",
    "You must decide when to skip and when to pull the trigger.",
    "If you spin past a blank, I will shoot you myself, coo-py?",
    "Oh, and do not even try to shoot me",
    "we both know I am faster on the draw anyway. Coo-d luck!"};

    void Start()
    {
        //PlayMessage(1);
    }

    //use this for custom messages
    public void PrintText(string fullText)
    {
        StartCoroutine(Typewriter(textbox, fullText, speed));
    }

    //use this for sending the preset dialogue
    public void PlayMessage(int idx)
    {
        PrintText(messages[idx]);
    }

    IEnumerator Typewriter(TMP_Text textbox, string fullText, float speed)
    {
        string currentText = "";
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i];
            textbox.text = currentText;
            if (pigeonAS)
            {
                pigeonAS.Play();
            }
            yield return new WaitForSeconds(speed);
        }
    }

    public void ClearText()
    {
        textbox.text = "";
    }

    public void ClearTimer(float timer)
    {
        Invoke("ClearText", timer);
    }
}
