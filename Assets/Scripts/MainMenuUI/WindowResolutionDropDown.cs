using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindowResolutionDropDown : MonoBehaviour
{
    List<Resolution> resolutions = new List<Resolution>();
    TMP_Dropdown windowResolutionDropDown;
    
    FullScreenMode screenMode;

    public bool isFullScreen = true;
    Toggle fullScreenToggle;

    int resol_Width;
    int resol_Height;
    int resol_RefreshRate;

    private void Awake()
    {
        windowResolutionDropDown = GetComponentInChildren<TMP_Dropdown>();
        fullScreenToggle = transform.parent.Find("FullScreen_Toggle").GetComponent<Toggle>();
        InitUI();
    }

    private void Start()
    {
        fullScreenToggle.onValueChanged.AddListener((isFullScreen) => FullScreenButton(isFullScreen));
    }

    private void InitUI()
    {
        resolutions.AddRange(Screen.resolutions);
        windowResolutionDropDown.options.Clear();

        foreach (Resolution resolution in resolutions)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
            optionData.text = resolution.width + "x" + resolution.height + " " + resolution.refreshRate + "hz";
            windowResolutionDropDown.options.Add(optionData);
        }
        windowResolutionDropDown.RefreshShownValue();
        windowResolutionDropDown.value = 21;

        fullScreenToggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false ;
    }


    public void FullScreenButton(bool isFull)
    {
        isFull = fullScreenToggle.isOn;
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(resol_Width, resol_Height, screenMode, resol_RefreshRate);
    }

    public void DropDownOptionChange()
    {
        resol_Width = resolutions[windowResolutionDropDown.value].width;
        resol_Height = resolutions[windowResolutionDropDown.value].height;
        resol_RefreshRate = resolutions[windowResolutionDropDown.value].refreshRate;

        Screen.SetResolution(resol_Width, resol_Height, screenMode, resol_RefreshRate);
    }
}
