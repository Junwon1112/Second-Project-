using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 메인메뉴 씬으로 이동시키는 클래스
/// </summary>
public class ResumeButton : MonoBehaviour
{
    Button resumeButton;
    MenuUI_Basic mainMenuUI;

    private void Awake()
    {
        resumeButton = GetComponent<Button>();
        mainMenuUI = FindObjectOfType<MenuUI_Basic>();
    }

    private void Start()
    {
        resumeButton.onClick.AddListener(mainMenuUI.CloseMainMenu);
    }
}
