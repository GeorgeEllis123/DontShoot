using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        startButton.onClick.AddListener(() => startWhenPressed());
        quitButton.onClick.AddListener(() => startWhenPressed());
    }

    void startWhenPressed()
    {
        Debug.Log("Starting");
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }

    void quitGame()
    {
        Debug.Log("You quit the game"); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
