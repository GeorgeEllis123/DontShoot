using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class TextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private TextMeshProUGUI buttonPrompt;
    [SerializeField] private TextMeshProUGUI skipreminder;
    [SerializeField] private AudioSource pigeonSFX;
    [SerializeField] private float characterDelay = 0.04f;
    [SerializeField] private string[] monologueLines;
    [SerializeField] private string[] levelLines;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PatternManager patternManager;

    private Coroutine typewriterCoroutine;
    private bool isTyping;
    private bool isSkipping;
    private bool inMonologue;
    private int currentLine;

    private void Update()
    {
        Debug.Log("Target Ready:");
        Debug.Log(inputManager.TargetReady());
        Debug.Log("Get Level:");
        Debug.Log(levelManager.GetLevel());


        if(inputManager.TargetReady() && levelManager.GetLevel() == 2)
        {
            if (!patternManager.GetNextBullet())
            {
                buttonPrompt.text = "Left Click / S!";
            }
            else
            {
                buttonPrompt.text = "Right Click / D!";
            }
        }
        else
        {
            buttonPrompt.text = "";
        }

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
        if (index-1 >= 0 && index-1 < levelLines.Length)
        {
            inMonologue = false;
            Invoke("ClearText", levelLines[index - 1].Length * characterDelay + 0.5f);
            PlayLine(levelLines[index-1]);
        } else
        {
            Invoke("ClearText", levelLines[index - 1].Length * characterDelay + 0.5f);
            PlayLine(levelLines[levelLines.Length - 1]);
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
            if (!pigeonSFX.isPlaying)
            {
                pigeonSFX.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
                pigeonSFX.Play();
            }

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
