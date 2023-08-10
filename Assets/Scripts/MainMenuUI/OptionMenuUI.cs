using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 첫번째 화면에만 존재하는 메뉴, 인게임내의 같은 역할은 형제인 MainMenuUI가 수행한다.
/// </summary>
public class OptionMenuUI : MenuUI_Basic
{
    CanvasGroup canvasGroup;
    bool isOpen;

    SideMenuUI[] sideMenuUIs;

    protected override CanvasGroup CanvasGroup { get; set; }
    protected override bool IsOpen { get; set; }
    protected override SideMenuUI[] SideMenuUIs { get; set; }

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        SideMenuUIs = FindObjectsOfType<SideMenuUI>();
        IsOpen = false;
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
        if (!IsChildMenuOpen())
        {
            Time.timeScale = 0;

            IsOpen = true;

            CanvasGroup.alpha = 1;
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.interactable = true;

            SoundPlayer.Instance.PlaySound(SoundType_Effect.Sound_UI_Open);
        }
    }

    public override void CloseMainMenu()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;

        IsOpen = false;

        SoundPlayer.Instance.PlaySound(SoundType_Effect.Sound_UI_Close);

        Time.timeScale = 1;
    }

    protected override bool IsChildMenuOpen()
    {
        bool isChildOpen = false;

        for (int i = 0; i < SideMenuUIs.Length; i++)
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
