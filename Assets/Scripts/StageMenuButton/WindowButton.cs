using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowButton : MonoBehaviour
{
    Button windowButton;
    MainMenuUI mainMenuUI;

    WindowMenuUI windowMenuUI;

    private void Awake()
    {
        windowButton = GetComponent<Button>();
        mainMenuUI = FindObjectOfType<MainMenuUI>();
        windowMenuUI = FindObjectOfType<WindowMenuUI>();
    }

    private void Start()
    {
        windowButton.onClick.AddListener(StartWindowUI);
    }

    private void StartWindowUI()
    {
        mainMenuUI.CloseMainMenu();

        windowMenuUI.SetWindow();
    }
}
