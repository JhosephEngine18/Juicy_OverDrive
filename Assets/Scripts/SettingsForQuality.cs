using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsForQuality : MonoBehaviour
{
    public Toggle fullscreen, vsync;
    public TMP_Dropdown Resolutions;
    void Start()
    {
        fullscreen.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsync.isOn = false;
        }
        else
        {
            vsync.isOn = true;
        }
    }


    public void OnValueChanged(int sel)
    {
        switch (sel)
        {
            case 0:
                Screen.SetResolution(1920, 1080, fullscreen);
                print("1920x1080");
                break;

                case 1:
                Screen.SetResolution(1280,720,fullscreen);
                print("1280x720");
                break;
        }
    }

}
