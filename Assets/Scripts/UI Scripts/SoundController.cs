// Tahmidul Islam @tahmidul612
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// This script is attached to the volume slider in the options menu
// It sets the volume of the game and saves it to PlayerPrefs
public class SoundController : MonoBehaviour
{
    private AudioMixer mainMixer;
    private Slider volumeSlider;
    void Start()
    {
        mainMixer = Resources.Load<AudioMixer>("MainMixer");
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = PlayerPrefs.GetFloat("mainVolume", 0.75f);
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(volumeSlider.value); });
    }
    private void SetVolume(float volume)
    {
        mainMixer.SetFloat("mainVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("mainVolume", volume);
    }
}
