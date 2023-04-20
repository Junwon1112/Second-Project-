using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 스킬창 UI에 대한 클래스
/// </summary>
public class SkillUI : MonoBehaviour
{
    PlayerInput skillWindowControl;

    public List<SkillData> skillDatas;
    SkillSlotUI[] skillSlotUIs;
    CanvasGroup skillCanvasGroup;
    public bool isSkillWindowOff = true;
    UI_Player_MoveOnOff ui_OnOff;

    Button skillCloseButton;

    
    TextMeshProUGUI skillPoint_Num;

    private void Awake()
    {
        skillSlotUIs = GetComponentsInChildren<SkillSlotUI>();
        skillWindowControl = new PlayerInput();
        skillCanvasGroup = GetComponent<CanvasGroup>();
        ui_OnOff = GetComponentInParent<UI_Player_MoveOnOff>();
        skillCloseButton = transform.Find("CloseButton").GetComponent<Button>();
        skillPoint_Num = transform.Find("SkillPointUI").GetChild(1).GetComponent<TextMeshProUGUI>();

        for(int i = 0; i < skillDatas.Count; i++)
        {
            if(skillDatas[i] != null)
            skillDatas[i].SkillLevel = 0;
        }

    }

    private void OnEnable()
    {
        skillWindowControl.Enable();
        skillWindowControl.Skill.SkillWindowOnOff.performed += OnSkillWindowOnOff;
    }


    private void OnDisable()
    {
        skillWindowControl.Skill.SkillWindowOnOff.performed -= OnSkillWindowOnOff;
        skillWindowControl.Disable();
    }

    private void Start()
    {
        skillCloseButton.onClick.AddListener(OnSkillOnOffSetting);
        int slotIndex = 0;

        for(int i = 0; i < skillSlotUIs.Length; i++)
        {
            if (skillDatas[i] == null)
            {
                Destroy(skillSlotUIs[slotIndex].transform.parent.gameObject);
                slotIndex++;
            }
            else if(GameManager.Instance.MainPlayer.Job == skillDatas[i].job)
            {
                skillSlotUIs[slotIndex].skillData = skillDatas[i];
                skillSlotUIs[slotIndex].upDownButton.SkillLevelToText();
                skillSlotUIs[slotIndex].SetSkillUIInfo();
                slotIndex++;
            }
           
        }

        SynchronizeSkillPoint();

        GameManager.Instance.MainPlayer.newDel_LevelUp += SynchronizeSkillPoint;
        
    }

    private void OnSkillWindowOnOff(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSkillOnOffSetting();
    }

    private void OnSkillOnOffSetting()
    {
        if (isSkillWindowOff)
        {
            isSkillWindowOff = false;

            skillCanvasGroup.alpha = 1;
            skillCanvasGroup.interactable = true;
            skillCanvasGroup.blocksRaycasts = true;

            ui_OnOff.IsUIOnOff();
        }
        else
        {
            isSkillWindowOff = true;

            skillCanvasGroup.alpha = 0;
            skillCanvasGroup.interactable = false;
            skillCanvasGroup.blocksRaycasts = false;

            ui_OnOff.IsUIOnOff();
        }
    }

    /// <summary>
    /// 플레이어 스킬포인트와 동기화
    /// </summary>
    public void SynchronizeSkillPoint()
    {
        skillPoint_Num.text = GameManager.Instance.MainPlayer.SkillPoint.ToString();
    }
}
