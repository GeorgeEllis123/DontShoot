using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioDistractionManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> sounds;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, sounds.Count);
        audioSource.clip = sounds[randomIndex];
        audioSource.Play();
    }

    public void PlayRandomSoundWithDelay(float delay)
    {
        StartCoroutine(PlayAfterDelay(delay));
    }

    private IEnumerator PlayAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayRandomSound();
    }
}
