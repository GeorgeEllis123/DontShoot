using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject playerGun;
    [SerializeField] private GameObject animatedGun;
    [SerializeField] private GameObject animatedWing;
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject bangScreen;
    [SerializeField] private GameObject toolTip;
    [SerializeField] private Animator bagAnimation;
    [SerializeField] private TextManager textManager;
    [SerializeField] private PatternManager patternManager;
    [SerializeField] private AudioSource bangSFX;
    [SerializeField] private AudioSource bagSFX;


    private enum Phase { YourTurn, Pass, TheirTurn }

    private Phase currPhase = Phase.TheirTurn;
    private Phase prevPhase = Phase.Pass;

    private int level = 1;
    public bool isGameover;

    public void Start()
    {
        bagAnimation.SetTrigger("BagOff");
        bagSFX.Play();
        StartCoroutine(StartMonologue());
    }

    public IEnumerator StartMonologue()
    {
        yield return new WaitForSeconds(1f);
        textManager.StartMonologue();
    }

    public void ChangePhase()
    {
        if (isGameover)
        {
            return;
        }

        switch (currPhase)
        {
            case Phase.TheirTurn:
                prevPhase = Phase.TheirTurn;
                currPhase = Phase.Pass;
                ExecutePhase();
                break;
            case Phase.Pass:
                if (prevPhase == Phase.TheirTurn)
                {
                    currPhase = Phase.YourTurn;
                    ExecutePhase();
                }
                else
                {
                    currPhase = Phase.TheirTurn;
                    StartCoroutine(WaitUntilNoDialogue());
                }
                prevPhase = Phase.Pass;
                break;
            case Phase.YourTurn:
                prevPhase = Phase.YourTurn;
                currPhase = Phase.Pass;
                ExecutePhase();
                break;
        }
        
    }

    public void ExecutePhase()
    {
        if (isGameover)
        {
            return;
        }
        
        if (prevPhase == Phase.YourTurn)
        {
            playerGun.GetComponent<Animator>().SetTrigger("PutDown");
        }

        switch (currPhase)
        {
            case Phase.TheirTurn:
                table.SetActive(true);
                animatedGun.SetActive(false);
                animatedWing.SetActive(false);
                patternManager.LoadBullets(level);
                break;
            case Phase.Pass:
                table.SetActive(true);
                animatedGun.SetActive(true);
                animatedWing.SetActive(true);
                animatedWing.GetComponent<Animator>().SetTrigger("Slide");
                animatedGun.GetComponent<GunMovement>().Toss(prevPhase == Phase.TheirTurn ? -1 : 1);
                if (prevPhase == Phase.YourTurn)
                {
                    textManager.PlayMessage(level - 1);
                }
                playerGun.SetActive(false);
                StartCoroutine(PassGunDelay());
                break;
            case Phase.YourTurn:
                level++;
                //table.SetActive(false);
                animatedGun.SetActive(false);
                animatedWing.SetActive(false);
                playerGun.SetActive(true);
                break;
        }
    }

    IEnumerator WaitUntilNoDialogue()
    {
        yield return new WaitUntil(() => textManager.isTyping == false);
        ExecutePhase();
    }

    IEnumerator PassGunDelay()
    {
        yield return new WaitForSeconds(1.2f);
        ChangePhase();
    }

    IEnumerator ReloadSceneDelay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ExtraLongReloadSceneDelay()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver(bool badTiming)
    {
        if (!isGameover)
        {
            isGameover = true;
            bangSFX.Play();
            bangScreen.SetActive(true);
            playerGun.SetActive(false);
            if (badTiming && level == 2)
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

    public int GetLevel()
    {
        return level;
    }
}
