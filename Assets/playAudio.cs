using UnityEngine;

public class playAudio : MonoBehaviour
{
    private AudioSource audioSource;

    // [SerializeField] private AudioClip clip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
       audioSource.Play();
    }
}
