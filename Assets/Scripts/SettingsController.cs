using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMPro.TMP_Dropdown displayDropdown;
    private List<Resolution> resolutions;
    private List<Display> displays;
    [SerializeField] private AudioMixer mainMixer;
    private void Start()
    {
        // Get all resolutions
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        // Add all resolutions to the dropdown
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "Hz";
            options.Add(option);
            // Check if the game screen resolution matches to one of the
            // available resolutions and set it as the current resolution value
            // in the dropdown
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Set volume
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("mainVolume", volume);
    }
}
