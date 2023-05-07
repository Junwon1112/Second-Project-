using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton_UI : MonoBehaviour
{
    private Button closeButton;
    BasicUIForm_Parent parentUI;
    UI_Player_MoveOnOff ui_OnOff;

    public Button CloseButton { get; set; }
    public BasicUIForm_Parent ParentUI { get; set; }

    private void Awake()
    {
        CloseButton = GetComponent<Button>();
        ParentUI = transform.parent.GetComponent<BasicUIForm_Parent>();
        ui_OnOff = FindObjectOfType<UI_Player_MoveOnOff>();
    }

    private void Start()
    {
        CloseButton.onClick.AddListener(ParentUI.UIOnOffSetting);
    }

}
