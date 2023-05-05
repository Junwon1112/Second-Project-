using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ��ųâ UI�� ���� Ŭ����
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

    RectTransform skillRectTransform;

    TextMeshProUGUI skillPoint_Num;

    private void Awake()
    {
        skillSlotUIs = GetComponentsInChildren<SkillSlotUI>();
        skillWindowControl = new PlayerInput();
        skillCanvasGroup = GetComponent<CanvasGroup>();
        ui_OnOff = GetComponentInParent<UI_Player_MoveOnOff>();
        skillCloseButton = transform.Find("CloseButton").GetComponent<Button>();
        skillPoint_Num = transform.Find("SkillPointUI").GetChild(1).GetComponent<TextMeshProUGUI>();
        skillRectTransform = GetComponent<RectTransform>();

        for(int i = 0; i < skillDatas.Count; i++)
        {
            if(skillDatas[i] != null)
            skillDatas[i].SkillLevel = 0;
        }

    }

    private void OnEnable()
    {
        skillWindowControl.Enable();
        skillWindowControl.SkillUI.SkillWindowOnOff.performed += OnSkillWindowOnOff;
    }


    private void OnDisable()
    {
        skillWindowControl.SkillUI.SkillWindowOnOff.performed -= OnSkillWindowOnOff;
        skillWindowControl.Disable();
    }

    private void Start()
    {
        skillCloseButton.onClick.AddListener(OnSkillOnOffSetting);
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
        OnSkillOnOffSetting();
        skillRectTransform.SetAsLastSibling();
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
    /// �÷��̾� ��ų����Ʈ�� ����ȭ
    /// </summary>
    public void SynchronizeSkillPoint()
    {
        skillPoint_Num.text = GameManager.Instance.MainPlayer.SkillPoint.ToString();
    }
}
