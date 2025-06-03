using System.Collections;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public bool[] currentPattern;

    private PatternGenerator pg;

    //add two AudioSources to pattern manager object; first is bullet, second is blank
    private AudioSource bulletSound;
    private AudioSource blankSound;

    public float timeBetweenLoads = 0.2f;

    private int bulletIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pg = gameObject.GetComponent<PatternGenerator>();
        AudioSource[] audiosources = gameObject.GetComponents<AudioSource>();
        bulletSound = audiosources[0];
        blankSound = audiosources[1];

        LoadBullets(1);
        //VerifyClick(0, true);
        //VerifyClick(1, true);
        //VerifyClick(2, true);
        //VerifyClick(3, true);
        //VerifyClick(4, true);
        //VerifyClick(5, true);
    }

    private void LoadBullets(int level)
    {
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
        /*
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
        */
    }

    public bool VerifyClick(bool b)
    {
        bool correct = currentPattern[bulletIndex] == b;
        if (correct)
        {
            Debug.Log("correct!");
        } else
        {
            Debug.Log("incorrect...");
        }
        
        if (bulletIndex >= currentPattern.Length)
        {
            bulletIndex = 0;
            Debug.Log("Change scenes");
        }
        else
        {
            bulletIndex++;
        }
        return correct;
    }

    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
    }
}
