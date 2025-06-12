using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCutScene : MonoBehaviour
{
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject black;
    [SerializeField] private AudioSource walkingSFX;
    [SerializeField] private AudioSource bagSFX;

    public void Play()
    {
        buttons.SetActive(false);
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
        SceneManager.LoadScene(1);
    }
}
