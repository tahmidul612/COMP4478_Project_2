using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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
