using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EquipmentUI : InventoryUI      //������ �θ𺸴� ����� ���� �ڽ��� ������ ������ ���ڴ� ���� ������. �ƴ� �Ųٷ� �����丵�� �غ���.. 
{
    public PlayerInput equipmentControl;   //uŰ�� ����Ű������ ��ǲ�ý��ۿ� ����

    protected CanvasGroup canvasGroupOnOff;   //���� Ű�°� canvasGroup�� �̿��� ����

    protected Button equipCloseButton;

    public bool isEquipCanvasGroupOff = true;   //�κ��丮�� �����ִ��� �����ִ��� Ȯ���ϱ� ���� ����
    //new public ItemSlotUI[] slotUIs;

    InventoryUI inventoryUI;

    public EquipSlotUI[] equipSlotUIs;

    UI_Player_MoveOnOff ui_OnOff_E;

    protected override void Awake()
    {
        inventoryControl = new PlayerInput();   //������ ��Ŭ���� inven���� �����ؼ� �ʿ��� �� �������� ���ؼ�
        equipmentControl = new PlayerInput();
        canvasGroupOnOff = GetComponent<CanvasGroup>();
        equipCloseButton = transform.Find("CloseButton").GetComponent<Button>();
        equipSlotUIs = GetComponentsInChildren<EquipSlotUI>();

        graphicRaycaster = GameObject.Find("Canvas").gameObject.GetComponent<GraphicRaycaster>();
        player = FindObjectOfType<Player>();
        inventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
        ui_OnOff_E = GetComponentInParent<UI_Player_MoveOnOff>();
    }

    private void Start()
    {
        equipCloseButton.onClick.AddListener(EquipmentOnOffSetting);
        isEquipCanvasGroupOff = true;
        for(int i = 0; i < equipSlotUIs.Length; i++)
        {
            equipSlotUIs[i].equipSlotID = 1001 + i; //1000���� ������ ��񽽷����� �����ϱ� ���� �߰�
        }
    }

    private void OnEnable()
    {
        equipmentControl.Equipment.Enable();
        equipmentControl.Equipment.EquipmentOnOff.performed += OnEquipmentOnOff;
    }

    private void OnDisable()
    {
        equipmentControl.Equipment.EquipmentOnOff.performed -= OnEquipmentOnOff;
        equipmentControl.Equipment.Disable();
    }

    private void OnEquipmentOnOff(InputAction.CallbackContext obj)
    {
        EquipmentOnOffSetting();
    }

    private void EquipmentOnOffSetting()
    {
        if (isEquipCanvasGroupOff)
        {
            isEquipCanvasGroupOff = false;

            canvasGroupOnOff.alpha = 1;
            canvasGroupOnOff.interactable = true;
            canvasGroupOnOff.blocksRaycasts = true;

            ui_OnOff_E.IsUIOnOff2();  //��ü UI ONOFF���¸� Ȯ���ϰ� InputSystem�� Enable�� Disable���ִ� �Լ�

            //if(inventoryUI.isInvenCanvasGroupOff)
            //{
            //    GameManager.Instance.MainPlayer.input.Disable();
            //    inventoryUI.inventoryControl.Inventory.InventoryItemUse.performed += inventoryUI.OnInventoryItemUse;
            //}
        }
        else
        {
            isEquipCanvasGroupOff = true;


            canvasGroupOnOff.alpha = 0;
            canvasGroupOnOff.interactable = false;
            canvasGroupOnOff.blocksRaycasts = false;
            
            ui_OnOff_E.IsUIOnOff2();

            //if (inventoryUI.isInvenCanvasGroupOff)
            //{
            //    GameManager.Instance.MainPlayer.input.Enable();
            //    inventoryUI.inventoryControl.Inventory.InventoryItemUse.performed -= inventoryUI.OnInventoryItemUse;
            //}
        }
    }


}

