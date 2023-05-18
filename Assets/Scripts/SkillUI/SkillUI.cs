using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// ��ųâ UI�� ���� Ŭ����
/// </summary>
public class SkillUI : BasicUIForm_Parent
{
    PlayerInput input_Control;
    CanvasGroup canvasGroupOnOff;
    bool isUIOnOff = true;
    UI_Player_MoveOnOff ui_OnOff;
    RectTransform rectTransform_UI;
    Player player;


    TextMeshProUGUI skillPoint_Num;
    public List<SkillData> skillDatas;
    SkillSlotUI[] skillSlotUIs;

    public override PlayerInput Input_Control { get => input_Control; set => input_Control = value; }
    public override CanvasGroup CanvasGroupOnOff { get => canvasGroupOnOff; set => canvasGroupOnOff = value; }
    public override bool IsUIOnOff { get => isUIOnOff; set => isUIOnOff = value; }
    public override RectTransform RectTransform_UI { get => rectTransform_UI; set => rectTransform_UI = value; }
    public override Player Player { get => player; set => player = value; }
    public override UI_Player_MoveOnOff UI_OnOff { get => ui_OnOff; set => ui_OnOff = value; }


    private void Awake()
    {
        skillSlotUIs = GetComponentsInChildren<SkillSlotUI>();
        input_Control = new PlayerInput();
        canvasGroupOnOff = GetComponent<CanvasGroup>();
        ui_OnOff = GetComponentInParent<UI_Player_MoveOnOff>();
        skillPoint_Num = transform.Find("SkillPointUI").GetChild(1).GetComponent<TextMeshProUGUI>();
        rectTransform_UI = GetComponent<RectTransform>();

        for(int i = 0; i < skillDatas.Count; i++)
        {
            if(skillDatas[i] != null)
            skillDatas[i].SkillLevel = 0;
        }

    }

    private void OnEnable()
    {
        input_Control.Enable();
        input_Control.SkillUI.SkillWindowOnOff.performed += OnSkillWindowOnOff;
    }


    private void OnDisable()
    {
        input_Control.SkillUI.SkillWindowOnOff.performed -= OnSkillWindowOnOff;
        input_Control.Disable();
    }

    private void Start()
    {
        int slotIndex = 0;

        for(int i = 0; i < skillDatas.Count; i++)
        {
            if(GameManager.Instance.MainPlayer.Job == skillDatas[i].job)
            {
                skillSlotUIs[slotIndex].SkillData = skillDatas[i];
                skillSlotUIs[slotIndex].upDownButton.SkillLevelToText();
                skillSlotUIs[slotIndex].SetSkillUIInfo();
                slotIndex++;
            }

            if (i == skillDatas.Count -1)
            {
                int destroyNum = skillSlotUIs.Length - slotIndex;
                for (int j = 0; j < destroyNum; j++)
                {
                    Destroy(skillSlotUIs[skillSlotUIs.Length -1 - j].transform.parent.gameObject);
                }
            }
        }

        SynchronizeSkillPoint();

        GameManager.Instance.MainPlayer.newDel_LevelUp += SynchronizeSkillPoint;
        
    }

    private void OnSkillWindowOnOff(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        UIOnOffSetting();
        rectTransform_UI.SetAsLastSibling();
    }

    /// <summary>
    /// �κ��丮 onoff�� ������ �޼���
    /// </summary>
    public override void UIOnOffSetting()
    {
        if (IsUIOnOff)
        {
            IsUIOnOff = false;

            CanvasGroupOnOff.alpha = 1;
            CanvasGroupOnOff.interactable = true;
            CanvasGroupOnOff.blocksRaycasts = true;

            UI_OnOff.IsUIOnOff();
        }
        else
        {
            isUIOnOff = true;

            CanvasGroupOnOff.alpha = 0;
            CanvasGroupOnOff.interactable = false;
            CanvasGroupOnOff.blocksRaycasts = false;

            UI_OnOff.IsUIOnOff();
        }
    }

    /// <summary>
    /// �÷��̾� ��ų����Ʈ�� ����ȭ
    /// </summary>
    public void SynchronizeSkillPoint()
    {
        skillPoint_Num.text = GameManager.Instance.MainPlayer.SkillPoint.ToString();
    }
}
