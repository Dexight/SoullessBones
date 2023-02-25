using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{

    public GameObject SettingsPanel;
    public Button CloseButton;
    public TMP_Dropdown ResolutionDropdown;
    public Resolution[] Resolutions;
    void Start()
    {
        ResolutionDropdown.ClearOptions();
        List<string> Options = new List<string>();
        Resolutions = Screen.resolutions;
        int CurrentResolutionIndex = 0;
        for(int i = 0; i <Resolutions.Length; ++i)
        {
            var option  = Resolutions[i].width + "x" + Resolutions[i].height + " " + Resolutions[i].refreshRate + "Hz";
            Options.Add(option);
            if (Resolutions[i].width == Screen.width && Resolutions[i].height== Screen.height)
                CurrentResolutionIndex = i;
        }
        ResolutionDropdown.AddOptions(Options);
        ResolutionDropdown.RefreshShownValue();
        LoadSettings(CurrentResolutionIndex);
    }

    public void SetWindowMode(bool isFullScreen)=>Screen.fullScreen = isFullScreen;


    public void SaveAndCloseSettings()
    {
        PlayerPrefs.SetInt("ResolutionPreference",
                   ResolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference",
                   System.Convert.ToInt32(Screen.fullScreen));
        
        SettingsPanel.SetActive(false);
    }

    public void SetResolution(int ResolutionIndex) 
    {
        Resolution res = Resolutions[ResolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void LoadSettings(int CurrentResIndex)
    {
        if (PlayerPrefs.HasKey("ResolutionPreference"))
            ResolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        else
            ResolutionDropdown.value = CurrentResIndex;
        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;
    }
}
