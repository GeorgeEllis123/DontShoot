using UnityEngine;
using TMPro;
using System.Collections;

public class TextManager : MonoBehaviour
{
    public TMP_Text textbox;
    public float speed = 0.1f;
    public AudioSource pigeonAS;
    public AudioSource clickAS;
    public AudioSource spinAS;
    public int monologueNum = 0;

    public string[] monologue =
    {
        "Hello there, old friend.",
        "You and I are going to play a game.",
        "You are familiar with Russian Roulette, coo-rrect?",
        "I am going to load this gun with both regular bullets and blanks.",
        "You must decide when to skip and when to pull the trigger.",
        "If you spin past a blank, I will shoot you myself, coo-py? Coo-d luck!"
    };

    public string[] messages =
    {

    };

    private bool isTyping = false;

    void Start()
    {
        //PlayMessage(1);
        monologueNum = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines(); 
                textbox.text = monologue[monologueNum - 1]; 
                isTyping = false;
            }
        }
    }

    //use this for custom messages
    public void PrintText(string fullText)
    {
        StartCoroutine(Typewriter(textbox, fullText, speed));
    }

    //use this for sending the monologue messages
    public void PlayMonologue()
    {
        PrintText(monologue[monologueNum]);
        monologueNum++;
    }

    public void DelayMonologue(float timer)
    {
        Invoke("PlayMonologue", timer);
    }

    //use this for sending the dialogue between rounds
    public void PlayMessage(int idx)
    {
        PrintText(messages[idx]);
    }

    IEnumerator Typewriter(TMP_Text textbox, string fullText, float speed)
    {
        isTyping = true;
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
        isTyping = false;
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
