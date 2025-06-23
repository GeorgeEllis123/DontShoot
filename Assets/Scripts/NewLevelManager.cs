using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelManager : MonoBehaviour
{
    [Header("General Level Details")]
    public int pigeon_Visitor_Level = 0;
    [SerializeField] private int day = 0;
    [SerializeField] private int levelsInDay = 1;
    [SerializeField] private int[] patternLotPerLevel; // see NewPatternManager for what patterns are in each lot
    [SerializeField] private bool dayHasMonologueFirst = false;
    [SerializeField] private bool audioDistractionWhilePlaying = false;
    [SerializeField] private bool audioDistractionWhileListening = false;

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
    [SerializeField] private Pigeon_Visitor pigeon_Visitor;

    private bool isGameover = false;

    void Start()
    {
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
                    textManager.PlayMessage(i - 1);
                }
                else
                {
                    textManager.PlayMessage(i);
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
            // Pigeon Visitor Enters
            if (day >= 7)
            {
                if(i == pigeon_Visitor_Level)
                {
                    pigeon_Visitor.PigeonEnter();
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

            // Wait till Player Done Playing
            yield return new WaitUntil(() => patternManager.GetPlayPatternDone());

            // Put down gun
            timingCircle.SetActive(false);
            // TODO: Add a put down animation here!!!
            yield return new WaitForSeconds(0.5f);
            playerGun.SetActive(false);
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

    private IEnumerator NextLevel()
    {
        
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
