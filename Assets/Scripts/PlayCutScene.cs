using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCutScene : MonoBehaviour
{
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject black;
    [SerializeField] private AudioSource walkingSFX;

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
        black.SetActive(true);
        yield return new WaitForSeconds(1f);
        Debug.Log("test4");
        SceneManager.LoadScene(1);
    }
}
