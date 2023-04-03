using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    private static TMPro.TMP_Dropdown resolutionDropdown;
    private static TMPro.TMP_Dropdown displayDropdown;
    private Slider volumeSlider;
    private Button resetNameButton;
    private Button backButton;
    private static List<Resolution> resolutions;
    private static List<Display> displays;
    public static AudioMixer mainMixer;
    private static InputField uname;
    private void Awake()
    {
        resolutionDropdown = transform.Find("Resolution/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        displayDropdown = transform.Find("Display/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        mainMixer = Resources.Load<AudioMixer>("MainMixer");
        volumeSlider = transform.Find("Sound/Slider").GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(volumeSlider.value); });
        resetNameButton = transform.Find("Reset Name").GetComponent<Button>();
        uname = transform.parent.Find("MainMenu/uname").GetComponent<InputField>();
        resetNameButton.onClick.AddListener(delegate { resetName(); });
        backButton = transform.Find("Back Button").GetComponent<Button>();
        backButton.onClick.AddListener(delegate { Back(); });
    }
    void Start()
    {
        OnEnable();
    }
    private void OnEnable()
    {
        setupResolutions();
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            displayDropdown.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            setupDisplays();
        }
    }

    private void setupDisplays()
    {
        displays = new List<Display>();
        displays.AddRange(Display.displays);
        displayDropdown.ClearOptions();
        for (int i = 0; i < displays.Count; i++)
        {
            displayDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData("Display " + (i + 1).ToString()));
        }
        displayDropdown.onValueChanged.AddListener(delegate { SetDisplay(displayDropdown.value); });
        displayDropdown.RefreshShownValue();
    }

    public static void setupResolutions()
    {
        int currentRefreshRate = Screen.currentResolution.refreshRate;
        Resolution currentResolution = new()
        {
            width = Screen.width,
            height = Screen.height,
            refreshRate = currentRefreshRate
        };
        Resolution maxResolution = new()
        {
            width = Display.main.systemWidth,
            height = Display.main.systemHeight,
            refreshRate = currentRefreshRate
        };
        resolutions = new List<Resolution>();
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            resolutions.AddRange(new Resolution[]
            {
            new Resolution {width = 640, height = 360, refreshRate = currentRefreshRate},
            new Resolution {width = 1280, height = 720, refreshRate = currentRefreshRate},
            new Resolution {width = 1920, height = 1080, refreshRate = currentRefreshRate},
            new Resolution {width = 2560, height = 1440, refreshRate = currentRefreshRate},
            new Resolution {width = 3840, height = 2160, refreshRate = currentRefreshRate},
            currentResolution,
            maxResolution
            });
        }
        else
        {
            resolutions.Add(currentResolution);
        }
        resolutions = resolutions.Distinct().ToList();
        resolutions = resolutions.OrderBy(res => res.width).ToList();
        resolutions = resolutions.GetRange(0, resolutions.IndexOf(maxResolution) + 1);

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions.Select(res => res.ToString()).ToList());
        resolutionDropdown.value = resolutions.IndexOf(currentResolution);
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
    }

    private static void SetDisplay(int displayIndex)
    {
        // PlayerPrefs.SetInt("UnitySelectMonitor", displayIndex);
    }

    // Set volume
    private static void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("mainVolume", volume);
    }

    private void Back()
    {
        gameObject.SetActive(false);
        gameObject.transform.parent.GetChild(0).gameObject.SetActive(true);
    }


    private void resetName()
    {


        PlayerPrefs.DeleteKey("username");
        uname.gameObject.SetActive(true);
    }

}
