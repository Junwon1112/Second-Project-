using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 첫번째 시작씬에서 같은일은 OptionMenuUI가 수행한다. 
/// </summary>
public class MainMenuUI : MenuUI_Basic
{
    PlayerInput inputActions;

    CanvasGroup canvasGroup;
    bool isOpen = false;

    SideMenuUI[] sideMenuUIs;
    protected override CanvasGroup CanvasGroup { get; set; }
    protected override bool IsOpen { get; set; }
    protected override SideMenuUI[] SideMenuUIs { get; set; }


    private void Awake()
    {
        inputActions = new PlayerInput();

        CanvasGroup = GetComponent<CanvasGroup>();
        SideMenuUIs = FindObjectsOfType<SideMenuUI>();
        IsOpen = false;
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

    public void OnMainMenu(InputAction.CallbackContext obj)
    {
        OnOffMainMenu();
    }

    public override void OnOffMainMenu()
    {
        if (!IsOpen)
        {
            OpenMainMenu();
        }
        else
        {
            CloseMainMenu();
        }
    }

    public override void OpenMainMenu()
    {
        if(!IsChildMenuOpen())
        {
            Time.timeScale = 0;

            IsOpen = true;

            CanvasGroup.alpha = 1;
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.interactable = true;
        }  
    }

    public override void CloseMainMenu()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;

        IsOpen = false;

        Time.timeScale = 1;
    }

    protected override bool IsChildMenuOpen()
    {
        bool isChildOpen = false;

        for(int i = 0; i < SideMenuUIs.Length; i++)
        {
            if (!SideMenuUIs[i].IsSideUIChangeComplete) //하위 UI를 추가할 때마다 추가
            {
                isChildOpen = true;
                return isChildOpen;
            }
        }
        return isChildOpen;
    }
}
