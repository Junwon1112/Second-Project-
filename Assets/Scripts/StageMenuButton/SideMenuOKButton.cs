using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenuOKButton : MonoBehaviour
{
    Button volumeOKButton;

    SideMenuUI sideMenuUI;

    MainMenuUI mainMenuUI;

    private void Awake()
    {
        volumeOKButton = GetComponent<Button>();
        sideMenuUI = transform.parent.GetComponent<SideMenuUI>();
        mainMenuUI = FindObjectOfType<MainMenuUI>();
    }

    private void Start()
    {
        volumeOKButton.onClick.AddListener(ClickOKButton);
    }

    private void ClickOKButton()
    {
        sideMenuUI.SetWindow();

        mainMenuUI.OpenMainMenu();
    }



}
