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
        setupResolutions();
        setupDisplays();
    }

    private void setupDisplays()
    {
        displays = new List<Display>();
        displays.AddRange(Display.displays);
        displayDropdown.ClearOptions();
        for (int i=0; i<displays.Count; i++)
        {
            displayDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData("Display " + (i+1).ToString()));
        }
        displayDropdown.onValueChanged.AddListener(delegate { SetDisplay(displayDropdown.value); });
        displayDropdown.RefreshShownValue();
    }

    private void setupResolutions()
    {
        // populate Resolution[] list with 3 resolutions (360p, 720p and 1080p)
        int currentRefreshRate = Screen.currentResolution.refreshRate;
        Resolution currentResolution = new Resolution
            {
                width = Screen.width,
                height = Screen.height,
                refreshRate = currentRefreshRate
            };
        resolutions = new List<Resolution>();
        resolutions.AddRange(new Resolution[]
        {
            new Resolution {width = 640, height = 360, refreshRate = currentRefreshRate},
            new Resolution {width = 1280, height = 720, refreshRate = currentRefreshRate},
            new Resolution {width = 1920, height = 1080, refreshRate = currentRefreshRate},
            new Resolution {width = 2560, height = 1440, refreshRate = currentRefreshRate},
            new Resolution {width = 3840, height = 2160, refreshRate = currentRefreshRate},
            currentResolution
        });
        resolutions = resolutions.Distinct().ToList();
        resolutions = resolutions.OrderBy(res => res.width).ToList();
        // Add all resolutions to the dropdown
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions.Select(res => res.ToString()).ToList());
        resolutionDropdown.value = resolutions.IndexOf(currentResolution);
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
    }

    private void SetDisplay(int displayIndex)
    {
        // PlayerPrefs.SetInt("UnitySelectMonitor", displayIndex);
    }

    // Set volume
    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("mainVolume", volume);
    }

}
