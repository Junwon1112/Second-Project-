using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    Button volumeButton;
    MenuUI_Basic menuUI;

    VolumeMenuUI volumeMenuUI;

    private void Awake()
    {
        volumeButton = GetComponent<Button>();
        menuUI = FindObjectOfType<MenuUI_Basic>();
        volumeMenuUI = FindObjectOfType<VolumeMenuUI>();
    }

    private void Start()
    {
        volumeButton.onClick.AddListener(StartVolumeUI);
    }

    private void StartVolumeUI()
    {
        menuUI.CloseMainMenu();

        volumeMenuUI.SetWindow();
    }
}
