using UnityEngine;
using TMPro;
using System.Collections;

public class TextManager : MonoBehaviour
{
    [Header("Associations")]
    public TMP_Text textbox;
    public LevelManager levelManager;

    [Header("Timing")]
    public float typeSpeed = 0.1f;
    public float linePause = 2f;

    [Header("Audio")]
    public AudioSource pigeonAS;

    [Header("Dialogue")]
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

    private bool isPlayingMonologue = false;
    private bool isTyping = false;
    private bool isPausing = false;
    private int currentMonologueIndex = 0;
    private Coroutine monologueCoroutine;
    private Coroutine typewriterCoroutine;
    private Coroutine pauseCoroutine;

    void Start()
    {
        currentMonologueIndex = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleSpaceInput();
        }
    }

    private void HandleSpaceInput()
    {
        if (!isPlayingMonologue)
        {
            return;
        }

        if (isTyping)
        {
            SkipCurrentTyping();
        }
        else if (isPausing)
        {
            SkipPause();
        }
    }

    private void SkipCurrentTyping()
    {
        if(typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        textbox.text = monologue[currentMonologueIndex];
        isTyping = false;
        StartPause();
    }

    private void SkipPause()
    {
        if(pauseCoroutine != null)
        {
            StopCoroutine(pauseCoroutine);
        }
        isPausing = false;

        ContinueMonologue();
    }

    public void StartMonologue()
    {
        if (isPlayingMonologue)
        {
            return;
        }

        currentMonologueIndex = 0;
        isPlayingMonologue = true;
        monologueCoroutine = StartCoroutine(PlayMonologueSequence());
    }

    private IEnumerator PlayMonologueSequence()
    {
        while(currentMonologueIndex < monologue.Length && isPlayingMonologue)
        {
            textbox.text = "";

            yield return StartCoroutine(TypeSentence(monologue[currentMonologueIndex]));

            if(currentMonologueIndex < monologue.Length - 1)
            {
                yield return StartCoroutine(PauseBetweenSentences());
            }

            currentMonologueIndex++;
        }

        isPlayingMonologue = false;
        OnMonologueComplete();
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        typewriterCoroutine = StartCoroutine(Typewriter(sentence));
        yield return typewriterCoroutine;
        isTyping = false;
    }

    private IEnumerator PauseBetweenSentences()
    {
        isPausing = true;
        pauseCoroutine = StartCoroutine(WaitForSeconds(linePause));
        yield return pauseCoroutine;
        isPausing = false;
    }

    private IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private void StartPause()
    {
        if(currentMonologueIndex < monologue.Length - 1)
        {
            pauseCoroutine = StartCoroutine(PauseBetweenSentences());
        }
        else
        {
            isPlayingMonologue = false;
            OnMonologueComplete();
        }
    }

    private void ContinueMonologue()
    {
        currentMonologueIndex++;
        if(currentMonologueIndex < monologue.Length)
        {
            StartCoroutine(TypeSentence(monologue[currentMonologueIndex]));
        }
        else
        {
            isPlayingMonologue = false;
            OnMonologueComplete();
        }
    }

    private IEnumerator Typewriter(string fullText)
    {
        string currentText = "";
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i];
            textbox.text = currentText;

            if (pigeonAS && !pigeonAS.isPlaying)
            {
                pigeonAS.Play();
            }

            yield return new WaitForSeconds(typeSpeed);
        }
    }

    private void OnMonologueComplete()
    {
        ClearText();
        levelManager.ExecutePhase();
    }

    public void StopMonologue()
    {
        if(monologueCoroutine != null)
        {
            StopCoroutine(monologueCoroutine);
        }
        if(typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }
        if(pauseCoroutine != null)
        {
            StopCoroutine(pauseCoroutine);
        }

        isPlayingMonologue = false;
        isTyping = false;
        isPausing = false;
    }




    //Non monologue stuff


    //use this for custom messages
    public void PrintText(string fullText)
    {
        StartCoroutine(Typewriter(fullText));
    }

    //use this for sending the dialogue between rounds
    public void PlayMessage(int idx)
    {
        if(idx < messages.Length)
        {
            PrintText(messages[idx]);
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
