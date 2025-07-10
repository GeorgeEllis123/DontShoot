using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NewTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private TextMeshProUGUI skipReminder;
    [SerializeField] private AudioSource pigeonSFX;
    [SerializeField] private float characterDelay = 0.04f;
    [SerializeField] private string[] monologueLines;
    [SerializeField] private string[] levelLines;
    [SerializeField] private string lastLine;
    [SerializeField] private int maxCharsPerSegment = 100;

    private Coroutine typewriterCoroutine;
    private bool isTyping;
    private bool isSkipping;
    private bool inMonologue;
    private int currentLine;
    private bool done;
    private bool waitingForSegmentInput = false;
    private bool continueToNextSegment = false;

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
        else if (waitingForSegmentInput)
        {
            continueToNextSegment = true;
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

        string[] segments = LineSplit(line, maxCharsPerSegment);

        if(segments.Length > 1)
        {
            typewriterCoroutine = StartCoroutine(PlaySegments(segments));
        }
        else
        {
            typewriterCoroutine = StartCoroutine(TypeLine(line));
        }
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

    private IEnumerator PlaySegments(string[] segments)
    {
        for(int i = 0; i < segments.Length; i++)
        {
            yield return StartCoroutine(TypeLine(segments[i]));

            if(i < segments.Length - 1)
            {
                waitingForSegmentInput = true;
                continueToNextSegment = false;

                yield return new WaitUntil(() => continueToNextSegment);

                waitingForSegmentInput = false;
                textBox.text = "";
            }
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

    public string[] LineSplit(string line, int maxChars = 60)
    {
        if(line.Length < maxChars)
        {
            return new string[] { line };
        }

        List<string> segments = new List<string>();
        string remaining = line;

        while(remaining.Length > maxChars)
        {
            int splitIndex = -1;

            for(int i = maxChars; i >= maxChars / 2; i--)
            {
                if(i < remaining.Length && ".,;:!?".Contains(remaining[i]))
                {
                    splitIndex = i + 1;
                    break;
                }
            }

            if(splitIndex == -1)
            {
                for(int i = maxChars; i >= maxChars / 2; i--)
                {
                    if(i < remaining.Length && remaining[i] == ' ')
                    {
                        splitIndex = i;
                        break;
                    }
                }
            }

            if(splitIndex == -1)
            {
                splitIndex = maxChars;
            }

            segments.Add(remaining.Substring(0, splitIndex).Trim());
            remaining = remaining.Substring(splitIndex).Trim();
        }

        if(remaining.Length > 0)
        {
            segments.Add(remaining);
        }

        return segments.ToArray();
    }
}
