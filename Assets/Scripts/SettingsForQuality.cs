using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsForQuality : MonoBehaviour
{
    public TMP_Dropdown Resolutions;
    bool isFullScreen = true;
    public Toggle ScreenMode;
    [SerializeField] Scene currentScene;

    //Changes the ScreenMode if is FullScreen or not

    private void Awake()
    {
        OnValueChanged(PlayerPrefs.GetInt("Resolutions", 0));
        isFullScreen = PlayerPrefs.GetInt("ScreenMode", 1) != 0;
        OnScreenModeChanged(isFullScreen);
    }
    public void OnScreenModeChanged(bool isFullScreened)
    {

        if (isFullScreened)
        { 
            Screen.fullScreen = true;
            ScreenMode.isOn = true;
        }
        else if (!isFullScreened)
        {
            Screen.fullScreen = false;
            ScreenMode.isOn = false;
        }

        isFullScreen = isFullScreened;
        PlayerPrefs.SetInt("ScreenMode", isFullScreened ? 1 : 0);
        PlayerPrefs.Save();
    }


    public void OnValueChanged(int sel)
    {
        switch (sel)
        {
            case 0:
                Screen.SetResolution(1920, 1080, isFullScreen);
                print("1920x1080");
                Resolutions.value = 0;
                break;

            case 1:
                Screen.SetResolution(1280, 720, isFullScreen);
                print("1280x720");
                Resolutions.value = 1;
                break;

            case 2:
                Screen.SetResolution(640, 360, isFullScreen);
                print("640x360");
                Resolutions.value = 2;
                break;
        }

        PlayerPrefs.SetInt("Resolutions", sel);
        PlayerPrefs.Save();
    }

}
