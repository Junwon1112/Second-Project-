using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    PlayerInput skillWindowControl;

    public List<SkillData> skillDatas;
    SkillSlotUI[] skillSlotUIs;
    CanvasGroup skillCanvasGroup;
    bool isSkillWindowOff = true;

    private void Awake()
    {
        skillSlotUIs = GetComponentsInChildren<SkillSlotUI>();
        skillWindowControl = new PlayerInput();
        skillCanvasGroup = GetComponent<CanvasGroup>();
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
        for(int i = 0; i < skillSlotUIs.Length; i++)
        {
            skillSlotUIs[i].skillData = skillDatas[i];
            skillSlotUIs[i].SetSkillUIInfo();
        }
        
    }

    private void OnSkillWindowOnOff(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(isSkillWindowOff)
        {
            isSkillWindowOff = false;

            skillCanvasGroup.alpha = 1;
            skillCanvasGroup.interactable = true;
            skillCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            isSkillWindowOff = true;

            skillCanvasGroup.alpha = 0;
            skillCanvasGroup.interactable = false;
            skillCanvasGroup.blocksRaycasts = false;

        }
        
    }

}
