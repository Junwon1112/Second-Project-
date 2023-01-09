using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EquipmentUI : InventoryUI      //앞으로 부모보다 기능이 적은 자식은 억지로 만들지 말자는 것을 느꼈다. 아님 거꾸로 리팩토링을 해보자.. 
{
    public PlayerInput equipmentControl;   //u키로 껐다키기위한 인풋시스템용 변수

    protected CanvasGroup canvasGroupOnOff;   //껐다 키는걸 canvasGroup을 이용한 변수

    protected Button equipCloseButton;

    public bool isEquipCanvasGroupOff = true;   //인벤토리가 꺼져있는지 켜져있는지 확인하기 위한 변수
    //new public ItemSlotUI[] slotUIs;

    InventoryUI inventoryUI;

    public EquipSlotUI[] equipSlotUIs;

    UI_Player_MoveOnOff ui_OnOff_E;

    protected override void Awake()
    {
        inventoryControl = new PlayerInput();   //아이템 우클릭은 inven에서 구현해서 필요할 때 가져오기 위해서
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
            equipSlotUIs[i].equipSlotID = 1001 + i; //1000번대 슬롯은 장비슬롯임을 구분하기 위해 추가
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

            ui_OnOff_E.IsUIOnOff2();  //전체 UI ONOFF상태를 확인하고 InputSystem을 Enable과 Disable해주는 함수

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

