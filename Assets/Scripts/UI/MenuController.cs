using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null; 

    [Header("Graphic Settings")]
    [SerializeField] private Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;

    [Header("Resolution Dropdown")]
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [Header("Menu UI Settings")]
    [SerializeField] public GameObject MainMenu;
    [SerializeField] public GameObject TutorialMenu;
    [SerializeField] public GameObject OptionsMenu;
    [SerializeField] public GameObject ControllsMenu;

    public void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
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

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void TutorialOn()
    {
        TutorialMenu.SetActive(true);
    }

    public void TutorialOff()
    {
        TutorialMenu.SetActive(false);
    }

    public void OptionsOn()
    {
        OptionsMenu.SetActive(true);
    }

    public void OptionsOff()
    {
        OptionsMenu.SetActive(false);
    }

    public void ControllsOn()
    {
        ControllsMenu.SetActive(true);
    }

    public void ControllsOff()
    {
        ControllsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
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