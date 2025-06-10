using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class TextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private AudioSource pigeonSFX;
    [SerializeField] private float characterDelay = 0.04f;
    [SerializeField] private string[] monologueLines;
    [SerializeField] private string[] levelLines;
    [SerializeField] private LevelManager levelManager;

    private Coroutine typewriterCoroutine;
    private bool isTyping;
    private bool isSkipping;
    private bool inMonologue;
    private int currentLine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                isSkipping = true;
            }
            else if (inMonologue)
            {
                PlayNextMonologueLine();
            }
        }
    }

    public void StartMonologue()
    {
        inMonologue = true;
        currentLine = 0;
        PlayLine(monologueLines[currentLine]);
    }

    private void ClearText()
    {
        textBox.text = "";
    }

    public void PlayMessage(int index)
    {
        Invoke("ClearText", 2f);
        if (index-1 >= 0 && index-1 < levelLines.Length)
        {
            inMonologue = false;
            PlayLine(levelLines[index-1]);
        }
    }

    private void PlayLine(string line)
    {
        if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);

        typewriterCoroutine = StartCoroutine(TypeLine(line));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        isSkipping = false;
        textBox.text = "";

        int charCount = 0;

        foreach (char c in line)
        {
            if (isSkipping)
            {
                textBox.text = line;
                break;
            }

            textBox.text += c;

            charCount++;
            if (charCount % 2 == 0 && pigeonSFX != null)
                pigeonSFX.Play();

            yield return new WaitForSeconds(characterDelay);
        }

        isTyping = false;
        isSkipping = false;

        if (inMonologue)
        {
            yield return new WaitForSeconds(1f);
        }
    }

    private void PlayNextMonologueLine()
    {
        currentLine++;

        if (currentLine < monologueLines.Length)
        {
            PlayLine(monologueLines[currentLine]);
        }
        else
        {
            inMonologue = false;
            textBox.text = "";
            levelManager.ExecutePhase();
        }
    }
}
