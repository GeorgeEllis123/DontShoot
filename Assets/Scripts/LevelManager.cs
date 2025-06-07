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
    [SerializeField] private TextManager textManager;
    [SerializeField] private PatternManager patternManager;
    [SerializeField] private AudioSource bangSFX;

    private enum Phase { YourTurn, Pass, TheirTurn }

    private Phase currPhase = Phase.TheirTurn;
    private Phase prevPhase = Phase.Pass;

    private int level = 1;
    private bool isGameover;

    public void Start()
    {
        StartMonologue();
    }

    public void StartMonologue()
    {
        //implement intro monologue
        textManager.DelayMonologue(0f);
        textManager.ClearTimer(4f);
        textManager.DelayMonologue(4f);
        textManager.ClearTimer(9f);
        textManager.DelayMonologue(9f);
        textManager.ClearTimer(15f);
        textManager.DelayMonologue(15f);
        textManager.ClearTimer(23f);
        textManager.DelayMonologue(23f);
        textManager.ClearTimer(30f);
        textManager.DelayMonologue(30f);
        textManager.ClearTimer(38f);

        Invoke("ExecutePhase", 38);
    }

    public void ChangePhase()
    {
        switch(currPhase)
        {
            case Phase.TheirTurn:
                prevPhase = Phase.TheirTurn;
                currPhase = Phase.Pass;
                break;
            case Phase.Pass:
                if (prevPhase == Phase.TheirTurn)
                {
                    currPhase = Phase.YourTurn;
                }
                else
                {
                    currPhase = Phase.TheirTurn;
                }
                prevPhase = Phase.Pass;
                break;
            case Phase.YourTurn:
                prevPhase = Phase.YourTurn;
                currPhase = Phase.Pass;
                break;
        }

        ExecutePhase();
    }

    private void ExecutePhase()
    {
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
                if(prevPhase == Phase.YourTurn)
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

    IEnumerator PassGunDelay()
    {
        yield return new WaitForSeconds(1.2f);
        ChangePhase();
    }

    IEnumerator ReloadSceneDelay()
    {
        yield return new WaitForSeconds(2f);
        bangScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        if (!isGameover)
        {
            isGameover = true;
            bangSFX.Play();
            bangScreen.SetActive(true);
            StartCoroutine(ReloadSceneDelay());
        }
    }
}
