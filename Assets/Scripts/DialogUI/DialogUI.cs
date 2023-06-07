using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class DialogUI : BasicUIForm_Parent, IPointerClickHandler, IPointerDownHandler
{
    public string[] dialogs_Print;
    public string npcName;
    uint index = 0;
    public NPC npc;
    public NPC_Trigger npc_Trigger;

    public override PlayerInput Input_Control { get; set; }
    public override CanvasGroup CanvasGroupOnOff { get; set; }
    public override bool IsUIOnOff { get; set; }
    public override RectTransform RectTransform_UI { get; set; }
    public override Player Player { get; set; }
    public override UI_Player_MoveOnOff UI_OnOff { get; set; }
    public TextMeshProUGUI TextName { get; set; }
    public TextMeshProUGUI Text_Dialog { get; set; }

    bool isEndTalk;

    private void OnEnable()
    {
        Input_Control.StoreUI_Dialog.Enable();
        Input_Control.StoreUI_Dialog.StoreUI_Dialog_OnOff.performed += OnDialogOnoff;
    }


    private void OnDisable()
    {
        Input_Control.StoreUI_Dialog.StoreUI_Dialog_OnOff.performed -= OnDialogOnoff;
        Input_Control.StoreUI_Dialog.Disable();
    }

    private void Awake()
    {
        TextName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        Text_Dialog = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        Input_Control = TotalGameManager.Instance.Input;
        RectTransform_UI = GetComponent<RectTransform>();
    }

    private void Start()
    {
        IsUIOnOff = true;
        dialogs_Print = new string[10];
    }
    private void OnDialogOnoff(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (npc_Trigger.IsPlayerInTrigger)  //NPC트리거 내부라면
        {
            UIOnOffSetting();
            PrintDialog();
            RectTransform_UI.SetAsLastSibling();
        }
    }

    public override void UIOnOffSetting()
    {
        if (IsUIOnOff)
        {
            IsUIOnOff = false;

            CanvasGroupOnOff.alpha = 1;
            CanvasGroupOnOff.interactable = true;
            CanvasGroupOnOff.blocksRaycasts = true;

            //UI_OnOff.IsUIOnOff();
        }
        else
        {
            IsUIOnOff = true;

            CanvasGroupOnOff.alpha = 0;
            CanvasGroupOnOff.interactable = false;
            CanvasGroupOnOff.blocksRaycasts = false;

            //UI_OnOff.IsUIOnOff();
        }

    }

    public void PrintDialog()
    {
        TextName.text = npcName;
        Text_Dialog.text = dialogs_Print[index];
    }

    public void SetText(string name, string[] dialogs)
    {
        for (int i = 0; i < dialogs_Print.Length; i++)
        {
            dialogs_Print[i] = null;
        }
        
        for (int i = 0; i < dialogs.Length; i++)
        {
            npcName = name;
            dialogs_Print[i] = dialogs[i]; 
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        index++;
        if(npc.basicScript.Length > index)
        {
            PrintDialog();
        }
        else
        {
            index = 0;
            UIOnOffSetting();
        }
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
