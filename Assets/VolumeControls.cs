using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControls : MonoBehaviour
{

    public Slider backgroundVolumeSlider;
    public Slider sfxVolumeSlider;

    public AudioMixer backgroundMixer;
    public AudioMixer sfxMixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustBackgroundVolume()
    {
        backgroundMixer.SetFloat("volume", backgroundVolumeSlider.value);
    }

    public void AdjustSFXVolume()
    {
        sfxMixer.SetFloat("volume", sfxVolumeSlider.value);
    }
}
