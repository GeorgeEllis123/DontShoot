using System.Collections;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    private LevelManager levelManager;

    public bool[] currentPattern;

    private PatternGenerator pg;

    //add two AudioSources to pattern manager object; first is bullet, second is blank
    //public AudioClip bulletSound;
    //public AudioClip blankSound;

    // barrel animation
    public BarrelAnimation barrelRotator;

    private float timeBetweenLoads;

    private int bulletIndex = 0;

    AudioSource[] audiosources;
    AudioSource bulletSound;
    AudioSource blankSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        pg = gameObject.GetComponent<PatternGenerator>();
        audiosources = gameObject.GetComponents<AudioSource>();
        bulletSound = audiosources[0];
        blankSound = audiosources[1];
    }

    public void LoadBullets(int level)
    {
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
        else if (level < 4)
        {
            timeBetweenLoads = 0.7f;
            currentPattern = pg.GeneratePlay();
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
                bulletSound.Play();
                yield return new WaitForSeconds(timeBetweenLoads);
            }
            //if blank
            else
            {
                blankSound.Play();
                yield return new WaitForSeconds(timeBetweenLoads);
            }
        }
        levelManager.ChangePhase();
    }

    public bool VerifyClick(bool b)
    {
        bool correct = currentPattern[bulletIndex] == b;
        if (!correct)
            levelManager.GameOver(false);

        bulletIndex++;
        barrelRotator.RotateMinus60();

        if (bulletIndex >= currentPattern.Length)
        {
            bulletIndex = 0;
            StartCoroutine(ChangePhaseWithDelay());
            barrelRotator.ResetRotation();
        }
        return correct;
    }

    public void GetShot(bool badTiming)
    {
        levelManager.GameOver(badTiming);
    }

    public bool GetNextBullet()
    {
        return currentPattern[bulletIndex];
    }

    IEnumerator ChangePhaseWithDelay()
    {
        yield return new WaitForSeconds(0.3f);
        levelManager.ChangePhase();
    }
}
