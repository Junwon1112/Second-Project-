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

    private void Awake()
    {
        inputActions = new PlayerInput();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        inputActions.MainMenu.Enable();
        inputActions.MainMenu.StartMainMenu.performed += OnMainMenu;
    }

    private void OnDisable()
    {
        inputActions.MainMenu.StartMainMenu.performed -= OnMainMenu;
        inputActions.MainMenu.Disable();
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
        Time.timeScale = 0;

        isOpen = true;

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void CloseMainMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        isOpen = false;

        Time.timeScale = 1;
    }


}
