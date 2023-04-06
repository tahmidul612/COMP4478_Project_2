// Tahmidul Islam @tahmidul612
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static TMPro.TMP_Dropdown resolutionDropdown;
    private static TMPro.TMP_Dropdown displayDropdown;
    private Button resetNameButton;
    public static List<Resolution> resolutions;
    private static List<Display> displays;
    private InputField uname;
    private void Awake()
    {
        resolutionDropdown = transform.Find("Resolution/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        displayDropdown = transform.Find("Display/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            resetNameButton = transform.Find("Reset Name").GetComponent<Button>();
            uname = transform.parent.Find("MainMenu/uname").GetComponent<InputField>();
            resetNameButton.onClick.AddListener(delegate { ResetName(); });
        }
    }
    void Start()
    {
        OnEnable();
    }

    // Refresh the resolution dropdown when the settings menu is opened
    private void OnEnable()
    {
        SetupResolutions();

        // Don't show the display dropdown if WebGL
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            displayDropdown.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            SetupDisplays();
        }
    }

    // Populate the display selection dropdown with the
    // available display indexes
    private void SetupDisplays()
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

    public static void SetupResolutions()
    {
        // Get the current and the maximum available resolution
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

        // Create a list of resolutions to choose from
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
        // Only use the current resolution if WebGL
        else
        {
            resolutions.Add(currentResolution);
        }
        // Remove duplicates and sort the list
        resolutions = resolutions.Distinct().ToList();
        resolutions = resolutions.OrderBy(res => res.width).ToList();
        resolutions = resolutions.GetRange(0, resolutions.IndexOf(maxResolution) + 1); // Remove resolutions that are higher than the maximum

        // Set the dropdown options
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions.Select(res => res.ToString()).ToList());
        resolutionDropdown.value = resolutions.IndexOf(currentResolution);
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
    }

    private static void SetDisplay(int displayIndex)
    {
        // PlayerPrefs.SetInt("UnitySelectMonitor", displayIndex);
        // TODO: Add display switching
        // Unity documentation is not very clear on how to achieve this
    }

    // Set volume
    private static void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Reset the username
    private void ResetName()
    {
        PlayerPrefs.DeleteKey("username");
        uname.gameObject.SetActive(true);
    }

}
