using System.Collections;
using UnityEngine;

public class NewPatternManager : MonoBehaviour
{
    [SerializeField] private float timeBetweenLoads = 0.5f;

    private NewLevelManager levelManager;
    private bool[] currentPattern;
    private NewPatternGenerator pg;
    [SerializeField] private NewInputManager inputManager;


    // barrel animation
    public BarrelAnimation barrelRotator;

    private int bulletIndex = 0;

    private AudioSource[] audiosources;
    private AudioSource bulletSound;
    private AudioSource blankSound;

    private bool playSoundDone = false;
    private bool playPatternDone = false;

    void Awake()
    {
        levelManager = FindAnyObjectByType<NewLevelManager>();
        pg = gameObject.GetComponent<NewPatternGenerator>();
        audiosources = gameObject.GetComponents<AudioSource>();
        bulletSound = audiosources[0];
        blankSound = audiosources[1];
    }

    public void LoadBullets(int lot)
    {
        currentPattern = pg.GetPattern(lot);
        // reset current number of inputs each time a pattern is loaded
        inputManager.numInputs = 0;
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
        playSoundDone = true;
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
            playPatternDone = true;
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

    public bool GetPlaySoundDone()
    {
        return playSoundDone;
    }

    public bool GetPlayPatternDone()
    {
        return playPatternDone;
    }

    public void ResetBools()
    {
        playPatternDone = false;
        playSoundDone = false;
    }

}
