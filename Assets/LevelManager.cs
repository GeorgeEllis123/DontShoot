using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject playerGun;

    private enum Phase { YourTurn, Pass, TheirTurn }

    private Phase currPhase = Phase.TheirTurn;
    private Phase prevPhase = Phase.Pass;

    private int level;

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
                    currPhase = Phase.Pass;
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
                Debug.Log("Play pattern sound");
                break;
            case Phase.Pass:
                playerGun.SetActive(false);
                Debug.Log("Pass the gun");
                break;
            case Phase.YourTurn:
                playerGun.SetActive(true);
                Debug.Log("Stay alive!!!");
                break;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
