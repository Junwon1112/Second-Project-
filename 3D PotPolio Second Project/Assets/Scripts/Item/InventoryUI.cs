using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public PlayerInput inventoryControl;   //iŰ�� ����Ű������ ��ǲ�ý��ۿ� ����
    protected CanvasGroup invenCanvasGroupOnOff;   //���� Ű�°� canvasGroup�� �̿��� ����
    public bool isInvenCanvasGroupOff;   //�κ��丮�� �����ִ��� �����ִ��� Ȯ���ϱ� ���� ����

    

    private Button invenCloseButton;

    public ItemSlotUI[] slotUIs;
    Inventory playerInven;

    protected GraphicRaycaster graphicRaycaster;

    protected PointerEventData pointerEventData;

    protected Player player;

    private EquipmentUI equipmentUI;

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


    protected virtual void Awake()
    {
        inventoryControl = new PlayerInput();
        invenCanvasGroupOnOff = GetComponent<CanvasGroup>();
        invenCloseButton = GetComponentInChildren<Button>();
        slotUIs = GetComponentsInChildren<ItemSlotUI>();

        playerInven = FindObjectOfType<Inventory>();
        graphicRaycaster = GameObject.Find("Canvas").gameObject.GetComponent<GraphicRaycaster>();
        player = FindObjectOfType<Player>();
        equipmentUI = FindObjectOfType<EquipmentUI>();
    }

    private void Start()
    {
        invenCloseButton.onClick.AddListener(InventoryOnOffSetting);

        SetAllSlotWithData();   //���� ������ �� ����UI�� ���� �ʱ�ȭ

        isInvenCanvasGroupOff = true;
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

    

    protected void OnInventoryItemUse(InputAction.CallbackContext obj)    //��Ŭ������ ������ ��� �� ������ ���� �Լ�, ��ǲ�׼����� ���������Ƿ� �����ϱ� ���Ϸ��� �κ��丮���� ����(onEnable���� �ѹ��� ȣ�� �Ϸ���)
    {
        List<RaycastResult> slotItemCheck = new List<RaycastResult>();  //UI�ν��� ���ؼ��� GraphicRaycast�� �ʿ��ϰ� �̰� ��� �� ������ �� (RaycastResult)�� �޴� ����Ʈ�� ������
        pointerEventData = new PointerEventData(null);                  //GraphicRaycast���� ���콺 ��ġ�� PointerEventData���� �����Ƿ� ���� ����

        pointerEventData.position = Mouse.current.position.ReadValue();
        graphicRaycaster.Raycast(pointerEventData, slotItemCheck);

        GameObject returnObject = slotItemCheck[0].gameObject;

        Debug.Log($"{returnObject.name}");
        
        ItemSlotUI tempSlotUI;
        tempSlotUI = returnObject.GetComponent<ItemSlotUI>();

        if(tempSlotUI != null)
        {
            if (tempSlotUI.slotUIData.ID == 0)   //data�� �����̶�� (����id = 0)
            {
                ItemData_Potion tempPotion = new ItemData_Potion();
                tempPotion.Use(player);
                if (tempSlotUI.slotUICount <= 1)
                {
                    tempSlotUI.SetSlotWithData(tempSlotUI.slotUIData, 0);
                    playerInven.itemSlots[tempSlotUI.slotUIID].ClearSlotItem();
                }
                else
                {
                    tempSlotUI.SetSlotWithData(tempSlotUI.slotUIData, tempSlotUI.slotUICount - 1);
                    playerInven.itemSlots[tempSlotUI.slotUIID].DecreaseSlotItem(1);
                }
            }
            else if(tempSlotUI.slotUIData.ID == 1)  //data�� ������
            {
                //���â�� ����� �ű⿡ ���Կ� ����, ���� �κ��丮 ���Կ����� �����
                //���â���� ��Ŭ���ϸ� �ٽ� �κ��丮�� �̵��ϸ� ���� ����
                //�ɸ��� ����ġ�� ����, ���� �̹� ������ ���Ⱑ �ִٸ� �ش� ���Կ��� ���� ��ȯ
                //weapon�� equip���� ���� ����
                //���â ������ ��
                //1.������ ����ó�� ��� �����͸� ���� ������
                //2.��Ŭ���ϸ� ���� ����
            }
        }
        
    }

    private void InventoryOnOffSetting()
    {
        if (isInvenCanvasGroupOff)
        {
            isInvenCanvasGroupOff = false;
            if(equipmentUI.isEquipCanvasGroupOff)   //�κ��״µ� ���â�� ���������� �κ��� �÷��̾� ��Ȱ��ȭ
            {
                GameManager.Instance.MainPlayer.input.Disable();
                inventoryControl.Inventory.InventoryItemUse.performed += OnInventoryItemUse;
            }


            invenCanvasGroupOnOff.alpha = 1;
            invenCanvasGroupOnOff.interactable = true;
            invenCanvasGroupOnOff.blocksRaycasts = true;
        }
        else
        {
            isInvenCanvasGroupOff = true;
            if (equipmentUI.isEquipCanvasGroupOff)  //�κ����µ� ���â�� ���������� �κ��� �÷��̾� ��Ȱ��ȭ
            {
                GameManager.Instance.MainPlayer.input.Enable();
                inventoryControl.Inventory.InventoryItemUse.performed -= OnInventoryItemUse;
            }
                

            invenCanvasGroupOnOff.alpha = 0;
            invenCanvasGroupOnOff.interactable = false;
            invenCanvasGroupOnOff.blocksRaycasts = false;
        }
    }

    

    public void SetAllSlotWithData()    //UI�� �κ��丮 �����͸� �־��ִ� �Լ�
    {
        for (int i = 0; i < slotUIs.Length; i++) 
        {  
            slotUIs[i].SetSlotWithData(playerInven.itemSlots[i].SlotItemData, playerInven.itemSlots[i].ItemCount);
            slotUIs[i].slotUIID = i;
        }
    }

}
