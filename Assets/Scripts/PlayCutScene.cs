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
        Debug.Log("test1");
    }

    IEnumerator Kidnap()
    {
        Debug.Log("test2");
        yield return new WaitForSeconds(3f);
        Debug.Log("test3");
        walkingSFX.Stop();
        bagSFX.Play();
        yield return new WaitForSeconds(0.1f);
        black.SetActive(true);
        yield return new WaitForSeconds(1f);
        Debug.Log("test4");
        SceneManager.LoadScene(1);
    }
}
