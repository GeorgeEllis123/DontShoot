using System.Collections;
using UnityEngine;
using TMPro;

public class NewTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private TextMeshProUGUI skipReminder;
    [SerializeField] private AudioSource pigeonSFX;
    [SerializeField] private float characterDelay = 0.04f;
    [SerializeField] private string[] monologueLines;
    [SerializeField] private string[] levelLines;
    [SerializeField] private string lastLine;

    private Coroutine typewriterCoroutine;
    private bool isTyping;
    private bool isSkipping;
    private bool inMonologue;
    private int currentLine;
    private bool done;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleSpacePress();
        }
    }

    private void HandleSpacePress()
    {
        skipReminder.gameObject.SetActive(false);

        if (isTyping)
        {
            isSkipping = true;
        }
        else
        {
            if (inMonologue)
            {
                PlayNextMonologueLine();
            }
            else
            {
                textBox.text = "";
                done = true;
            }
        }
    }

    public void StartMonologue()
    {
        inMonologue = true;
        currentLine = 0;
        done = false;
        PlayLine(monologueLines[currentLine]);
        skipReminder.gameObject.SetActive(true);
    }

    public void PlayMessage(int index = 0)
    {
        inMonologue = false;
        PlayLine(levelLines[index]);
    }

    public void PlayLastLine()
    {
        inMonologue = false;
        PlayLine(lastLine);
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

        //if (inMonologue)
        //{
           // yield return new WaitForSeconds(1f);
        //}
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
            done = true;
        }
    }

    public bool GetDone()
    {
        return done;
    }

    public void ResetBools()
    {
        done = false;
    }
}
