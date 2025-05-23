using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Collections.Generic;

public class OptionsController : MonoBehaviour
{
    float Selection = 2;

    [Space(10)]
    [Header("Volume Settings")]
    [SerializeField] private Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Space(10)]
    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;

    [Space(10)]
    [Header("Graphic Settings")]
    [SerializeField] private Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;

    [Space(10)]
    [Header("Resolution Dropdown")]
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [Space(10)]
    [Header("Back Button")]
    [SerializeField] private GameObject BackOff;
    [SerializeField] private GameObject BackOn;

    [Space(10)]
    [Header("Controls Button")]
    [SerializeField] private GameObject ControlsOff;
    [SerializeField] private GameObject ControlsOn;

    [Space(10)]
    [Header("Menu Settings")]
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject ControlMenu;

    public void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (Selection <= 2)
            {
                Selection++;
            }

            if (Selection > 2)
            {
                Selection = 1;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (Selection >= 1)
            {
                Selection--;
            }

            if (Selection < 1)
            {
                Selection = 2;
            }
        }

        if (Selection == 1)
        {
            BackOff.SetActive(true);
            BackOn.SetActive(false);
            ControlsOff.SetActive(false);
            ControlsOn.SetActive(true);

            if (Input.GetKeyUp(KeyCode.Return))
            {
                OptionsMenu.SetActive(false);
                ControlMenu.SetActive(true);
            }
        }

        if (Selection == 2)
        {
            BackOff.SetActive(false);
            BackOn.SetActive(true);
            ControlsOff.SetActive(true);
            ControlsOn.SetActive(false);

            if (Input.GetKeyUp(KeyCode.Return))
            {
                VolumeApply();
                GameplayApply();
                GraphicsApply();
                OptionsMenu.SetActive(false);
                MainMenu.SetActive(true);
            }
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
        volumeSlider.value = volume;
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        Debug.Log("Applied Volume");
    }

    public void GameplayApply()
    {
        if (invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
        }

        Debug.Log("Applied Gamplay");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void GraphicsApply()
    {
        //PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);
        PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;
        Debug.Log("Applied Graphics");
    }
}
