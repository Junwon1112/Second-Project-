using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowButton : MonoBehaviour
{
    Button windowButton;
    MenuUI_Basic menuUI;

    WindowMenuUI windowMenuUI;

    private void Awake()
    {
        windowButton = GetComponent<Button>();
        menuUI = FindObjectOfType<MenuUI_Basic>();
        windowMenuUI = FindObjectOfType<WindowMenuUI>();
    }

    private void Start()
    {
        windowButton.onClick.AddListener(StartWindowUI);
    }

    private void StartWindowUI()
    {
        menuUI.CloseMainMenu();

        windowMenuUI.SetWindow();
    }
}
