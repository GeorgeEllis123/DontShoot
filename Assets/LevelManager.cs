using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject playerGun;
    [SerializeField] private PatternManager patternManager;

    private enum Phase { YourTurn, Pass, TheirTurn }

    private Phase currPhase = Phase.TheirTurn;
    private Phase prevPhase = Phase.Pass;

    private int level = 1;

    public void Start()
    {
        ExecutePhase();
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
                patternManager.LoadBullets(level);
                Debug.Log("Play pattern sound");
                StartCoroutine(ChangePhaseAfterDelay());
                break;
            case Phase.Pass:
                playerGun.SetActive(false);
                Debug.Log("Pass the gun");
                StartCoroutine(ChangePhaseAfterDelay());
                break;
            case Phase.YourTurn:
                level++;
                playerGun.SetActive(true);
                Debug.Log("Stay alive!!!");
                break;
        }
    }

    IEnumerator ChangePhaseAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        ChangePhase();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
