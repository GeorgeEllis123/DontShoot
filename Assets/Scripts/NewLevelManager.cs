using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelManager : MonoBehaviour
{
    [SerializeField] private int section = 0;
    [SerializeField] private int levelsInSection = 1;

    [Header("References")]
    [SerializeField] private GameObject playerGun;
    [SerializeField] private GameObject animatedGun;
    [SerializeField] private GameObject animatedWing;
    [SerializeField] private NewTextManager textManager;
    [SerializeField] private NewPatternManager patternManager;
    [SerializeField] private BagBehavior bag;
    [SerializeField] private GameObject timingCircle;

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
        for (int i = 0; i < levelsInSection; i++)
        {
            // Play Dialogue
            textManager.gameObject.SetActive(true);
            if (section == 0 && i == 0)
            {
                textManager.StartMonologue();
            }
            else
            {
                if (section == 0)
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

            // Play Sound
            patternManager.LoadBullets(1);
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

        NextLevel();
    }

    public void GameOver(bool hadBadTiming)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextLevel()
    {
        Debug.Log("next level!");
    }

}
