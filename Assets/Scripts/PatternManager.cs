using System;
using System.Collections;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    private LevelManager levelManager;

    public bool[] currentPattern;

    private PatternGenerator pg;

    //add two AudioSources to pattern manager object; first is bullet, second is blank
    public AudioClip bulletSound;
    public AudioClip blankSound;

    public float timeBetweenLoads = 0.2f;

    private int bulletIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        pg = gameObject.GetComponent<PatternGenerator>();
        AudioSource[] audiosources = gameObject.GetComponents<AudioSource>();
    }

    public void LoadBullets(int level)
    {
        Debug.Log(level);
        if (level == 1)
        {
            currentPattern = new bool[6];
            currentPattern[0] = false;
            currentPattern[1] = false;
            currentPattern[2] = false;
            currentPattern[3] = false;
            currentPattern[4] = false;
            currentPattern[5] = true;
        }
        else if (level < 8)
        {
            currentPattern = pg.GeneratePlay();
        } else
        {
            currentPattern = pg.GenerateChallenge();
        }

        StartCoroutine(PlayPattern());
    }

    IEnumerator PlayPattern()
    {
        for (int i = 0; i < currentPattern.Length; i++)
        {
            //if bullet
            if (currentPattern[i])
            {
                AudioSource.PlayClipAtPoint(bulletSound, Vector3.zero);
                yield return new WaitForSeconds(timeBetweenLoads);
            }
            //if blank
            else
            {
                AudioSource.PlayClipAtPoint(blankSound, Vector3.zero);
                yield return new WaitForSeconds(timeBetweenLoads);
            }
        }
    }

    public bool VerifyClick(bool b)
    {
        bool correct = currentPattern[bulletIndex] == b;
        if (correct)
        {
            Debug.Log("correct!");
        } else
        {
            levelManager.GameOver();
        }

        bulletIndex++;
        if (bulletIndex >= currentPattern.Length)
        {
            bulletIndex = 0;
            levelManager.ChangePhase();
        }
        return correct;
    }
}
