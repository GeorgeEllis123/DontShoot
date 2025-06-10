using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    //[SerializeField] private Button startButton;
    //[SerializeField] private Button quitButton;

    void Start()
    {
        gameObject.SetActive(true);
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
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }

    //cannibalized this script, this method is the only important part now
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You quit the game");
    }
}
