using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

/// <summary>
/// 6개의 퀵슬롯을 관리하는 함수
/// </summary>
public class AllQuickSlotUI : MonoBehaviour
{
    PlayerInput input;

    /// <summary>
    /// 각각의 단일 퀵슬롯을 배열에 받음
    /// </summary>
    public QuickSlotUI[] quickSlotUIs;

    //Animator anim;



    private void Awake()
    {
        input = TotalGameManager.Instance.Input;
        quickSlotUIs = GetComponentsInChildren<QuickSlotUI>();
        //anim = FindObjectOfType<Player>().transform.GetComponent<Animator>();
    }

    /// <summary>
    /// InputSystem에 등록한 단축키들에 해당하는 함수 등록
    /// </summary>
    private void OnEnable()
    {
        input.QuickSlotUI.Enable();
        input.QuickSlotUI.QuickSlot1.performed += OnQuickSlot1;
        input.QuickSlotUI.QuickSlot2.performed += OnQuickSlot2;
        input.QuickSlotUI.QuickSlot3.performed += OnQuickSlot3;
        input.QuickSlotUI.QuickSlot4.performed += OnQuickSlot4;
        input.QuickSlotUI.QuickSlot5.performed += OnQuickSlot5;
        input.QuickSlotUI.QuickSlot6.performed += OnQuickSlot6;
    }


    private void OnDisable()
    {
        input.QuickSlotUI.QuickSlot1.performed -= OnQuickSlot1;
        input.QuickSlotUI.QuickSlot2.performed -= OnQuickSlot2;
        input.QuickSlotUI.QuickSlot3.performed -= OnQuickSlot3;
        input.QuickSlotUI.QuickSlot4.performed -= OnQuickSlot4;
        input.QuickSlotUI.QuickSlot5.performed -= OnQuickSlot5;
        input.QuickSlotUI.QuickSlot6.performed -= OnQuickSlot6;
        input.QuickSlotUI.Disable();
    }

    private void Start()
    {
        for(int i =0; i < quickSlotUIs.Length; i++)
        {
            quickSlotUIs[i].quickSlotID = 2000 + i;
            //quickSlotUIs[i].SkillUseInitiate();
        }
        
        
    }

    private void OnQuickSlot1(InputAction.CallbackContext obj)
    {
        quickSlotUIs[0].skillUse.UsingSkill(quickSlotUIs[0].quickSlotSkillData);
    }

    private void OnQuickSlot2(InputAction.CallbackContext obj)
    {
        quickSlotUIs[1].skillUse.UsingSkill(quickSlotUIs[1].quickSlotSkillData);
    }

    private void OnQuickSlot3(InputAction.CallbackContext obj)
    {
        quickSlotUIs[2].skillUse.UsingSkill(quickSlotUIs[2].quickSlotSkillData);
    }

    private void OnQuickSlot4(InputAction.CallbackContext obj)
    {
        quickSlotUIs[3].skillUse.UsingSkill(quickSlotUIs[3].quickSlotSkillData);
    }

    private void OnQuickSlot5(InputAction.CallbackContext obj)
    {
        quickSlotUIs[4].skillUse.UsingSkill(quickSlotUIs[4].quickSlotSkillData);
    }

    private void OnQuickSlot6(InputAction.CallbackContext obj)
    {
        quickSlotUIs[5].skillUse.UsingSkill(quickSlotUIs[5].quickSlotSkillData);
    }





}
