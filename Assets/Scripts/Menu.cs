using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class Menu : MonoBehaviour
{
    //[SerializeField] private Button startButton;
    //[SerializeField] private Button quitButton;
    [SerializeField] private GameObject disclaimer;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI startButtonTxt;
    private string highScoreKey = "HighScoreKey";


    void Start()
    {
        StartCoroutine(Disclaimer());

        int startingLevel = PlayerPrefs.GetInt(highScoreKey, 0);
        if (startingLevel > 0)
        {
            startButtonTxt.text = "Resume";
            restartButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //startButton.onClick.AddListener(() => StartWhenPressed());
        //quitButton.onClick.AddListener(() => QuitGame());
    }

    void StartWhenPressed()
    {
        Debug.Log("Starting");
        int startingLevel = PlayerPrefs.GetInt(highScoreKey, 0);
        if (startingLevel > 0)
            SceneManager.LoadScene(startingLevel);
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You quit the game");
    }

    public void GoToCredits()
    {
        gameObject.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void LeaveCredits()
    {
        creditsScreen.SetActive(false);
        gameObject.SetActive(true);
    }

    IEnumerator Disclaimer()
    {
        yield return new WaitForSecondsRealtime(3f);
        disclaimer.SetActive(false);
        gameObject.SetActive(true);
        int startingLevel = PlayerPrefs.GetInt(highScoreKey, 0);
        if (startingLevel > 0)
        {
            highScore.text = "High Score: \nDay " + startingLevel;
            highScore.gameObject.SetActive(true);
        }
    }
}
