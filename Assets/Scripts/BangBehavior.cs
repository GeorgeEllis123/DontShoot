using UnityEngine;
using TMPro;

public class BangBehavior : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI highScoreText;
    private string highScoreKey = "HighScoreKey";
    private int highScore;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        highScore = PlayerPrefs.GetInt(highScoreKey, 1);
    }
    private void OnEnable()
    {
        audioSource.Play();
        highScoreText.text = "High Score:\n" + highScore + " days";
        highScoreText.gameObject.SetActive(true);
    }
}
