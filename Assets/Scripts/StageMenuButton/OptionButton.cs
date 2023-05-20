using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    Button optionButton;
    MainMenuUI mainMenuUI;

    private void Awake()
    {
        optionButton = GetComponent<Button>();
        mainMenuUI = FindObjectOfType<MainMenuUI>();
    }

    private void Start()
    {
        optionButton.onClick.AddListener(mainMenuUI.OnOffMainMenu);
    }

}
