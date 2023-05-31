using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// 스킬창 UI에 대한 클래스
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
        Input_Control = TotalGameManager.Instance.Input;
        skillSlotUIs = GetComponentsInChildren<SkillSlotUI>();
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        UI_OnOff = GetComponentInParent<UI_Player_MoveOnOff>();
        skillPoint_Num = transform.Find("SkillPointUI").GetChild(1).GetComponent<TextMeshProUGUI>();
        RectTransform_UI = GetComponent<RectTransform>();

        for(int i = 0; i < skillDatas.Count; i++)
        {
            if(skillDatas[i] != null)
            skillDatas[i].SkillLevel = 0;
        }

    }

    private void OnEnable()
    {
        GetKey();
    }


    private void OnDisable()
    {
        Input_Control.SkillUI.SkillWindowOnOff.performed -= OnSkillWindowOnOff;
        Input_Control.Disable();
    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    GetKey();
    //}

    private void Start()
    {
        int slotIndex = 0;

        for(int i = 0; i < skillDatas.Count; i++)
        {
            if(InGameManager.Instance.MainPlayer.Job == skillDatas[i].job)
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

        InGameManager.Instance.MainPlayer.newDel_LevelUp += SynchronizeSkillPoint;
        
    }

    private void GetKey()
    {
        Input_Control.Enable();
        Input_Control.SkillUI.SkillWindowOnOff.performed += OnSkillWindowOnOff;
    }

    private void OnSkillWindowOnOff(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        UIOnOffSetting();
        rectTransform_UI.SetAsLastSibling();
    }

    /// <summary>
    /// 인벤토리 onoff시 실행할 메서드
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
    /// 플레이어 스킬포인트와 동기화
    /// </summary>
    public void SynchronizeSkillPoint()
    {
        skillPoint_Num.text = InGameManager.Instance.MainPlayer.SkillPoint.ToString();
    }
}
