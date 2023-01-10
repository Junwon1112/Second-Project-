using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AllQuickSlotUI : MonoBehaviour
{
    PlayerInput input;
    QuickSlotUI[] quickSlotUIs;


    private void Awake()
    {
        input = new PlayerInput();
        quickSlotUIs = GetComponentsInChildren<QuickSlotUI>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.QuickSlot.QuickSlot1.performed += OnQuickSlot1;
        input.QuickSlot.QuickSlot2.performed += OnQuickSlot2;
        input.QuickSlot.QuickSlot3.performed += OnQuickSlot3;
        input.QuickSlot.QuickSlot4.performed += OnQuickSlot4;
        input.QuickSlot.QuickSlot5.performed += OnQuickSlot5;
        input.QuickSlot.QuickSlot6.performed += OnQuickSlot6;
    }


    private void OnDisable()
    {
        input.QuickSlot.QuickSlot1.performed -= OnQuickSlot1;
        input.QuickSlot.QuickSlot2.performed -= OnQuickSlot2;
        input.QuickSlot.QuickSlot3.performed -= OnQuickSlot3;
        input.QuickSlot.QuickSlot4.performed -= OnQuickSlot4;
        input.QuickSlot.QuickSlot5.performed -= OnQuickSlot5;
        input.QuickSlot.QuickSlot6.performed -= OnQuickSlot6;
        input.Disable();
    }

    private void Start()
    {
        for(int i =0; i < quickSlotUIs.Length; i++)
        {
            quickSlotUIs[i].quickSlotID = 2000 + i;
        }
    }





    private void OnQuickSlot1(InputAction.CallbackContext obj)
    {
        //quickSlotUIs[0].quickSlotSkillData.skillMotion
    }

    private void OnQuickSlot2(InputAction.CallbackContext obj)
    {
        
    }

    private void OnQuickSlot3(InputAction.CallbackContext obj)
    {
        
    }

    private void OnQuickSlot4(InputAction.CallbackContext obj)
    {
        
    }

    private void OnQuickSlot5(InputAction.CallbackContext obj)
    {
        
    }

    private void OnQuickSlot6(InputAction.CallbackContext obj)
    {
        
    }




}
