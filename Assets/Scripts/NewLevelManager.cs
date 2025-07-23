using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelManager : MonoBehaviour
{
    [Header("General Level Details")]
    public int day = 0;
    public int levelsInDay = 1;
    public bool useAdlibsToday = true;
    [SerializeField] private int[] patternLotPerLevel; // see NewPatternManager for what patterns are in each lot
    [SerializeField] private bool dayHasMonologueFirst = false;
    [SerializeField] private bool audioDistractionWhilePlaying = false;
    [SerializeField] private bool audioDistractionWhileListening = false;
    private int lastAdlib = -1;

    [Header("References")]
    [SerializeField] private GameObject playerGun;
    [SerializeField] private GameObject animatedGun;
    [SerializeField] private GameObject animatedWing;
    [SerializeField] private NewTextManager textManager;
    [SerializeField] private NewPatternManager patternManager;
    [SerializeField] private AudioDistractionManager audioDistractionManager;
    [SerializeField] private BagBehavior bag;
    [SerializeField] private GameObject timingCircle;
    [SerializeField] private GameObject bangScreen;
    [SerializeField] private GameObject toolTip;
    [SerializeField] private Pigeon_Visitor pigeon_Visitor_Left;
    [SerializeField] private Pigeon_Visitor pigeon_Visitor_Right;

    [Header("Player Data")]
    [SerializeField] private float playerPlayPatternDoneWaitTime = 0.45f; 
    private int highScore;
    private string highScoreKey = "HighScoreKey";

    private bool isGameover = false;
    private int currentDay; 

    void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        currentDay = highScore;
        //Unlock ach at beginning of day 1 & 9
        //if (day == 1)
        //    AchievementManager.UnlockAchievement("ACH_COODNAPPED");

        //if (day == 9)
        //    AchievementManager.UnlockAchievement("ACH_NO_MORE_COOS");

        StartCoroutine("Play");
    }

    private IEnumerator Play()
    {
        //// Start Up Stuff ////
        bag.TakeOff();
        yield return new WaitForSeconds(1f);

        //// Level Stuff ////
        for (int i = 0; i < levelsInDay; i++)
        {
            // Play Dialogue
            textManager.gameObject.SetActive(true);

            if (dayHasMonologueFirst && i == 0)
            {
                textManager.StartMonologue();
            }
            else
            {
                if (dayHasMonologueFirst)
                {
                    // -1 to account for the monologue
                    if (useAdlibsToday && (i - 1) >= textManager.levelLines.Length)
                    {
                        textManager.PlayAdlib(GetRandomAdlib());
                    }
                    else
                    {
                        textManager.PlayMessage(i - 1);
                    }
                }
                else
                {
                    if (useAdlibsToday && i >= textManager.levelLines.Length)
                    {
                        textManager.PlayAdlib(GetRandomAdlib());
                    }
                    else
                    {
                        textManager.PlayMessage(i);
                    }
                }
            }

            // Wait till Dialogue is done
            yield return new WaitUntil(() => textManager.GetDone());

            yield return new WaitForSeconds(0.5f);

            // Hide Gun
            animatedGun.SetActive(false);
            animatedWing.SetActive(false);
            
            yield return new WaitForSeconds(0.2f);

            // Play Sound
            patternManager.LoadBullets(patternLotPerLevel[i]);
            if (audioDistractionWhileListening)
                audioDistractionManager.PlayRandomSoundWithDelay(Random.Range(1f, 3f));
            // Left Pigeon Visitor Enters, Right Pigeon Visitor Exits while loading
            if (day >= 7)
            {
                if (i == pigeon_Visitor_Left.enterLevel)
                {
                    pigeon_Visitor_Left.PigeonEnter();
                }

                if (i == pigeon_Visitor_Right.exitLevel)
                {
                    pigeon_Visitor_Right.PigeonExit();
                }
            }
            yield return new WaitUntil(() => patternManager.GetPlaySoundDone());
            yield return new WaitForSeconds(0.25f);

            // Pass the Gun To Player
            animatedGun.SetActive(true);
            animatedWing.SetActive(true);
            animatedWing.GetComponent<Animator>().SetTrigger("Slide");
            animatedGun.GetComponent<GunMovement>().Toss(-1);
            yield return new WaitForSeconds(1.2f);

            // Player Picks Up the Gun
            animatedGun.SetActive(false);
            animatedWing.SetActive(false);
            playerGun.SetActive(true);
            if (audioDistractionWhilePlaying)
                audioDistractionManager.PlayRandomSoundWithDelay(Random.Range(1f, 3f));

            // Right Pigeon Visitor Enters, Left Pigeon Visitor Exits while playing
            if (day >= 7)
            {
                if (i == pigeon_Visitor_Right.enterLevel)
                {
                    pigeon_Visitor_Right.PigeonEnter();
                }

                if (i == pigeon_Visitor_Left.exitLevel)
                {
                    pigeon_Visitor_Left.PigeonExit();
                }
            }

            // Wait till Player Done Playing
            yield return new WaitUntil(() => patternManager.GetPlayPatternDone());
            yield return new WaitForSeconds(playerPlayPatternDoneWaitTime);

            // Put down gun
            timingCircle.SetActive(false);
            yield return StartCoroutine(playerGun.GetComponent<PickUp>().HandlePutdown());
            timingCircle.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            // Pass the Gun To Pigeon
            animatedGun.SetActive(true);
            animatedWing.SetActive(true);
            animatedWing.GetComponent<Animator>().SetTrigger("Slide");
            animatedGun.GetComponent<GunMovement>().Toss(1);

            // Resets all the variables
            patternManager.ResetBools();
            textManager.ResetBools();
        }

        // Play last line
        textManager.PlayLastLine();
        yield return new WaitUntil(() => textManager.GetDone());

        // Put bag back on and load next level
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("NextLevel");
    }

    public void GameOver(bool hadBadTiming)
    {
        if (!isGameover)
        {
            isGameover = true;
            if (audioDistractionManager != null)
                audioDistractionManager.gameObject.SetActive(false);
            bangScreen.SetActive(true);
            playerGun.SetActive(false);
            if (hadBadTiming && day == 1)
            {

                toolTip.SetActive(true);
                StartCoroutine(ExtraLongReloadSceneDelay());
            }
            else
            {
                StartCoroutine(ReloadSceneDelay());
            }
        }
    }

    private int GetRandomAdlib()
    {
        int randomAdlib = Random.Range(0, textManager.adlibList.Length);
        int attempts = 0;
        while (randomAdlib == lastAdlib)
        {
            attempts++;
            if(attempts > 100)
            {
                break;
            }
            randomAdlib = Random.Range(0, textManager.adlibList.Length);
        }
        lastAdlib = randomAdlib;
        return randomAdlib;
    }

    private IEnumerator NextLevel()
    {
        currentDay++; 
        // update high score each time a new level is reached
        highScore = SceneManager.GetActiveScene().buildIndex + 1;
        // if high score is somehow set outside scene count range, set it to the final level
        if(highScore >= SceneManager.sceneCountInBuildSettings)
        {
            highScore = SceneManager.sceneCountInBuildSettings - 1;
        }
        PlayerPrefs.SetInt(highScoreKey, highScore);
        PlayerPrefs.Save();

        int lvlIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //AchievementManager.CheckForAchievement(currentDay, lvlIndex);

        if (SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings - 1)
        {
            bangScreen.SetActive(true);
            playerGun.SetActive(false);
            Debug.Log("GG");
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            bag.PutOn();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator ReloadSceneDelay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator ExtraLongReloadSceneDelay()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
