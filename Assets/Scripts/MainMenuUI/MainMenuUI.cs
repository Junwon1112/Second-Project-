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

    SideMenuUI[] sideMenuUIs;

    private void Awake()
    {
        inputActions = new PlayerInput();
        canvasGroup = GetComponent<CanvasGroup>();
        sideMenuUIs = FindObjectsOfType<SideMenuUI>();
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
        if(!IsChildMenuOpen())
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

    private bool IsChildMenuOpen()
    {
        bool isChildOpen = false;

        for(int i = 0; i < sideMenuUIs.Length; i++)
        {
            if (!sideMenuUIs[i].IsSideUIChangeComplete) //하위 UI를 추가할 때마다 추가
            {
                isChildOpen = true;
                return isChildOpen;
            }
        }
        return isChildOpen;
    }
}
