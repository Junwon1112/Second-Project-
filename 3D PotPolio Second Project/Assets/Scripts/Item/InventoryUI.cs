using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    PlayerInput inventoryControl;   //iŰ�� ����Ű������ ��ǲ�ý��ۿ� ����
    CanvasGroup canvasGroupOnOff;   //���� Ű�°� canvasGroup�� �̿��� ����
    bool iscanvasGroupOff = true;   //�κ��丮�� �����ִ��� �����ִ��� Ȯ���ϱ� ���� ����

    Button closeButton;

    public ItemSlotUI[] slotUIs;
    Inventory playerInven;

    // �κ��丮 Ŭ������ ������ ����
    /*
     * �κ� ���� ����
     * 1. â ���� �巡���ϸ� �κ��丮 â ���콺 ��ġ�� �̵�
     * 
     * ���� ���� ����
     * 
     * 1. ��Ŭ�� �Ǵ� ����Ŭ���� ���� ������ ��� �Ǵ� ����  => ���Կ��� ����
     * 2. �巡�׸� ���� ������ �̵�    => ���Կ��� ����
     *      -���콺�� ������ �ִ� ���¿��� ������ �������� �帴�ϰ� ����
     *      -���� �ܺη� �̵���Ű�� ������ �ܺο� �� �� ��� �Ұ��� �����
     *      -�̵��� �ڸ��� �ٸ� �������� ������ �ڸ��� �ٲٰ�, ���� �������̸� �� �� �ű���� ����� â ����
     * 3. ������ ���� Ŀ���� ������ ������ Infoâ ǥ��   => ���Կ��� ����
     * 4. �翬�� ������ �������� ���Կ� ������ ����
     *      -���� �ڽ����� ������(�̹���)�� �Ҵ�ǵ��� �ϴ� �Լ� �����
     *      
     */


    private void Awake()
    {
        inventoryControl = new PlayerInput();
        canvasGroupOnOff = GetComponent<CanvasGroup>();
        closeButton = GetComponentInChildren<Button>();
        slotUIs = GetComponentsInChildren<ItemSlotUI>();

        playerInven = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        closeButton.onClick.AddListener(InventoryOnOffSetting);

        SetAllSlotWithData();   //���� ������ �� ����UI�� ���� �ʱ�ȭ
    }

    private void OnEnable()
    {
        inventoryControl.Inventory.Enable();
        inventoryControl.Inventory.InventoryOnOff.performed += OnInventoryOnOff;
    }

    private void OnDisable()
    {
        inventoryControl.Inventory.InventoryOnOff.performed -= OnInventoryOnOff;
        inventoryControl.Inventory.Disable();
    }

    private void OnInventoryOnOff(InputAction.CallbackContext obj)
    {
        InventoryOnOffSetting();
    }

    private void InventoryOnOffSetting()
    {
        if (iscanvasGroupOff)
        {
            iscanvasGroupOff = false;
            GameManager.Instance.MainPlayer.input.Disable();

            canvasGroupOnOff.alpha = 1;
            canvasGroupOnOff.interactable = true;
            canvasGroupOnOff.blocksRaycasts = true;
        }
        else
        {
            iscanvasGroupOff = true;
            GameManager.Instance.MainPlayer.input.Enable();

            canvasGroupOnOff.alpha = 0;
            canvasGroupOnOff.interactable = false;
            canvasGroupOnOff.blocksRaycasts = false;
        }
    }

    public void SetAllSlotWithData()    //UI�� �κ��丮 �����͸� �־��ִ� �Լ�
    {
        Debug.Log($"{slotUIs.Length}");
        for (int i = 0; i < slotUIs.Length; i++) 
        {
            
            slotUIs[i].SetSlotWithData(playerInven.itemSlots[i].SlotItemData, playerInven.itemSlots[i].ItemCount);
            slotUIs[i].slotUIID = i;
        }
    }

}
