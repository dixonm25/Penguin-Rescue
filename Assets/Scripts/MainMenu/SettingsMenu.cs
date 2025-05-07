using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SettingsMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;

    public Toggle fullScreenToggle;

    public Toggle vsyncToggle;

    private int screenInt;

    private int vsyncInt;

    Resolution[] resolutions;

    private bool isFullScreen = false;

    private bool vsync = false;

    const string resName = "resolutionOption";

    void Awake()
    {
        screenInt = PlayerPrefs.GetInt("toggleState");

        if(screenInt == 1 )
        {
            isFullScreen = true;
            fullScreenToggle.isOn = true;
        }
        else
        {
            fullScreenToggle.isOn = false;
        }

        vsyncInt = PlayerPrefs.GetInt("vsyncTogState");

        if (vsyncInt == 0)
        {
            vsyncToggle.isOn = false;
        }
        else
        {
            vsync = true;
            vsyncToggle.isOn = true;
        }

            resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
            {
                PlayerPrefs.SetInt(resName, resolutionDropdown.value);
                PlayerPrefs.Save();
            }));
    }

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        if (isFullscreen == false)
        {
            PlayerPrefs.SetInt("toggleState", 0);
        }
        else
        {
            isFullscreen = true;
            PlayerPrefs.SetInt("toggleState", 1);
        }
    }

    public void SetVsync(bool vsync) 
    {
        if (vsync == true)
        {
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("vsyncTogState", 1);
        }
        else
        {
            vsync = false;
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("vsyncTogState", 0);
        }
    }

}
