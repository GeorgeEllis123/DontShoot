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

    private float timeBetweenLoads;

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
            timeBetweenLoads = 0.7f;
            currentPattern = new bool[6];
            currentPattern[0] = false;
            currentPattern[1] = false;
            currentPattern[2] = false;
            currentPattern[3] = false;
            currentPattern[4] = false;
            currentPattern[5] = true;
        }
        else if (level == 2)
        {
            timeBetweenLoads = 0.7f;
            currentPattern = new bool[6];
            currentPattern[0] = true;
            currentPattern[1] = true;
            currentPattern[2] = true;
            currentPattern[3] = true;
            currentPattern[4] = true;
            currentPattern[5] = false;
        }
        else if (level == 3)
        {
            timeBetweenLoads = 0.7f;
            currentPattern = new bool[6];
            currentPattern[0] = false;
            currentPattern[1] = true;
            currentPattern[2] = false;
            currentPattern[3] = true;
            currentPattern[4] = false;
            currentPattern[5] = true;
        }
        else if (level < 8)
        {
            timeBetweenLoads = 0.5f;
            currentPattern = pg.GeneratePlay();
        } else
        {
            timeBetweenLoads = 0.3f;
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
        levelManager.ChangePhase();
    }

    public bool VerifyClick(bool b)
    {
        bool correct = currentPattern[bulletIndex] == b;
        if (!correct)
            levelManager.GameOver();

        bulletIndex++;
        if (bulletIndex >= currentPattern.Length)
        {
            bulletIndex = 0;
            levelManager.ChangePhase();
        }
        return correct;
    }
}
