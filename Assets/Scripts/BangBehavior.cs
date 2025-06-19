using UnityEngine;

public class BangBehavior : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        audioSource.Play();
    }
}
