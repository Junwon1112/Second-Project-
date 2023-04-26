using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuUI : MonoBehaviour
{
    PlayerInput inputActions;

    CanvasGroup canvasGroup;
    bool isOpen = false;

    VolumeMenuUI volumeMenuUI;

    private void Awake()
    {
        inputActions = new PlayerInput();
        canvasGroup = GetComponent<CanvasGroup>();
        volumeMenuUI = FindObjectOfType<VolumeMenuUI>();
    }

    private void OnEnable()
    {
        inputActions.MainMenuUI.Enable();
        inputActions.MainMenuUI.StartMainMenu.performed += OnMainMenu;
    }

    private void OnDisable()
    {
        inputActions.MainMenuUI.StartMainMenu.performed -= OnMainMenu;
        inputActions.MainMenuUI.Disable();
    }

    private void OnMainMenu(InputAction.CallbackContext obj)
    {
        if(!isOpen)
        {
            OpenMainMenu();
        }
        else
        {
            CloseMainMenu();
        }
 
    }

    public void OpenMainMenu()
    {
        if(!isChildMenuOpen())
        {
            Time.timeScale = 0;

            isOpen = true;

            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }  
    }

    public void CloseMainMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        isOpen = false;

        Time.timeScale = 1;
    }

    private bool isChildMenuOpen()
    {
        bool isChildOpen = false;
        if(!volumeMenuUI.IsVolumeChangeComplete) //하위 UI를 추가할 때마다 추가
        {
            isChildOpen = true;
        }

        return isChildOpen;
    }
}
