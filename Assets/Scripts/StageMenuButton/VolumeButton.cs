using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    Button volumeButton;
    MainMenuUI mainMenuUI;

    VolumeMenuUI volumeMenuUI;

    private void Awake()
    {
        volumeButton = GetComponent<Button>();
        mainMenuUI = FindObjectOfType<MainMenuUI>();
        volumeMenuUI = FindObjectOfType<VolumeMenuUI>();
    }

    private void Start()
    {
        volumeButton.onClick.AddListener(StartVolumeUI);
    }

    private void StartVolumeUI()
    {
        mainMenuUI.CloseMainMenu();

        volumeMenuUI.SetWindow();
    }
}
