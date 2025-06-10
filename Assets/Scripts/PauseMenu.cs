using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject text;
    public GameObject background;
    public GameObject[] buttons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.SetActive(false); 
        background.SetActive(false);
        foreach (GameObject b in buttons)
        {
            b.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (Time.timeScale > 0f)
        {
            text.SetActive(true);
            background.SetActive(true);
            foreach (GameObject b in buttons)
            {
                b.SetActive(true);
            }
            Time.timeScale = 0f;
        } else
        {
            text.SetActive(false);
            background.SetActive(false);
            foreach (GameObject b in buttons)
            {
                b.SetActive(false);
            }
            Time.timeScale = 1f;
        }
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
