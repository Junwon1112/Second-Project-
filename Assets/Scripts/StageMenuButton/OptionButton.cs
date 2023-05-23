using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    Button optionButton;
    MenuUI_Basic mainMenuUI;

    private void Awake()
    {
        optionButton = GetComponent<Button>();
        mainMenuUI = FindObjectOfType<MenuUI_Basic>();
    }

    private void Start()
    {
        optionButton.onClick.AddListener(mainMenuUI.OnOffMainMenu);
    }

}
