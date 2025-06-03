using System.Collections;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public bool[] currentPattern;

    private PatternGenerator pg;

    //add two AudioSources to pattern manager object; first is bullet, second is blank
    private AudioSource bulletSound;
    private AudioSource blankSound;

    public float timeBetweenLoads = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pg = gameObject.GetComponent<PatternGenerator>();
        AudioSource[] audiosources = gameObject.GetComponents<AudioSource>();
        bulletSound = audiosources[0];
        blankSound = audiosources[1];

        LoadBullets(1);
        VerifyClick(0, true);
        VerifyClick(1, true);
        VerifyClick(2, true);
        VerifyClick(3, true);
        VerifyClick(4, true);
        VerifyClick(5, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadBullets(int level)
    {
        if (level < 8)
        {
            currentPattern = pg.GeneratePlay();
        } else
        {
            currentPattern = pg.GenerateChallenge();
        }
        for (int i = 0; i < currentPattern.Length; i++)
        {
            //if bullet
            if (currentPattern[i])
            {
                bulletSound.Play();
                Debug.Log("bullet sound");
                Wait(timeBetweenLoads);
            } 
            //if blank
            else
            {
                blankSound.Play();
                Debug.Log("blank sound");
                Wait(timeBetweenLoads);
            }
        }
    }

    public bool VerifyClick(int i, bool b)
    {
        if (currentPattern[i] == b)
        {
            Debug.Log("correct!");
        } else
        {
            Debug.Log("incorrect...");
        }
        return currentPattern[i] == b;
    }

    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
    }
}
