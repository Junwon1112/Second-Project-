using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeOKButton : MonoBehaviour
{
    Button volumeOKButton;

    VolumeMenuUI volumeMenuUI;

    MainMenuUI mainMenuUI;

    private void Awake()
    {
        volumeOKButton = GetComponent<Button>();
        volumeMenuUI = transform.parent.GetComponent<VolumeMenuUI>();
        mainMenuUI = FindObjectOfType<MainMenuUI>();
    }

    private void Start()
    {
        volumeOKButton.onClick.AddListener(ClickOKButton);
    }

    private void ClickOKButton()
    {
        volumeMenuUI.CloseVolumeMenu();

        mainMenuUI.OpenMainMenu();

    }



}
