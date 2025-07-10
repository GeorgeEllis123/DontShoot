using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCutScene : MonoBehaviour
{
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject black;
    [SerializeField] private GameObject highScoreText;
    [SerializeField] private AudioSource walkingSFX;
    [SerializeField] private AudioSource bagSFX;
    private string highScoreKey = "HighScoreKey";

    public void Play()
    {
        buttons.SetActive(false);
        highScoreText.SetActive(false);

        StartCoroutine(Kidnap());
    }

    IEnumerator Kidnap()
    {
        yield return new WaitForSeconds(3f);
        walkingSFX.Stop();
        bagSFX.Play();
        yield return new WaitForSeconds(0.1f);
        black.SetActive(true);
        yield return new WaitForSeconds(1f);
        // attempt to access high score level, defaults to 1
        int startingLevel = PlayerPrefs.GetInt(highScoreKey, 1);
        SceneManager.LoadScene(startingLevel);
    }
}
