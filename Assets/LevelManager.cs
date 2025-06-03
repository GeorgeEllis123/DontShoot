using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private enum Phase { YourTurn, Pass, TheirTurn }

    private Phase currPhase = Phase.TheirTurn;
    private Phase prevPhase = Phase.Pass;

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
        Debug.Log("Current Phase" + currPhase.ToString());
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
