using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// 인벤토리 UI를 상속받은 장비 클래스, 완성 후 UI클래스를 만들어서 상속을 할 걸..이라는 걸 느꼈다.
/// </summary>
public class EquipmentUI : BasicUIForm_Parent
{
    /// <summary>
    /// i키로 껐다키기위한 인풋시스템용 변수
    /// </summary>
    PlayerInput input_Control;
    /// <summary>
    /// 껐다 키는걸 canvasGroup을 이용한 변수
    /// </summary>
    CanvasGroup canvasGroupOnOff;
    /// <summary>
    /// 인벤토리가 꺼져있는지 켜져있는지 확인하기 위한 변수
    /// </summary>
    bool isUIOnOff;

    RectTransform rectTransform_UI;

    Player player;
    UI_Player_MoveOnOff ui_OnOff;

    public EquipSlotUI[] equipSlotUIs;
    Inventory playerInven;
    InventoryUI playerInvenUI;

    GraphicRaycaster uiGraphicRaycaster;
    PointerEventData uiPointerEventData;

    public override PlayerInput Input_Control { get => input_Control; set => input_Control = value; }
    public override CanvasGroup CanvasGroupOnOff { get => canvasGroupOnOff; set => canvasGroupOnOff = value; }
    public override bool IsUIOnOff { get => isUIOnOff; set => isUIOnOff = value; }
    public override RectTransform RectTransform_UI { get => rectTransform_UI; set => rectTransform_UI = value; }


    public override Player Player { get => player; set => player = value; }
    public override UI_Player_MoveOnOff UI_OnOff { get => ui_OnOff; set => ui_OnOff = value; }

    public Inventory PlayerInven { get => playerInven; set => playerInven = value; }
    public InventoryUI PlayerInvenUI { get => playerInvenUI; set => playerInvenUI = value; }


    public GraphicRaycaster UIGraphicRaycaster { get => uiGraphicRaycaster; set => uiGraphicRaycaster = value; }
    public PointerEventData UIPointerEventData { get => uiPointerEventData; set => uiPointerEventData = value; }

    protected void Awake()
    {
        Input_Control = new PlayerInput();
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        RectTransform_UI = GetComponent<RectTransform>();

        UIGraphicRaycaster = GameObject.Find("Canvas_Main").gameObject.GetComponent<GraphicRaycaster>();
        
        UI_OnOff = GetComponentInParent<UI_Player_MoveOnOff>();

        equipSlotUIs = GetComponentsInChildren<EquipSlotUI>();

        PlayerInvenUI = FindObjectOfType<InventoryUI>();
    }

    private void Start()
    {
        Player = GameManager.Instance.MainPlayer;
        PlayerInven = GameManager.Instance.MainPlayer.transform.GetComponentInChildren<Inventory>();

        IsUIOnOff = true;
        for(int i = 0; i < equipSlotUIs.Length; i++)
        {
            equipSlotUIs[i].equipSlotID = 1001 + i; //1000번대 슬롯은 장비슬롯임을 구분하기 위해 추가
        }
    }

    private void OnEnable()
    {
        Input_Control.EquipmentUI.Enable();
        Input_Control.EquipmentUI.EquipmentOnOff.performed += OnEquipmentOnOff;
    }

    private void OnDisable()
    {
        Input_Control.EquipmentUI.EquipmentOnOff.performed -= OnEquipmentOnOff;
        Input_Control.EquipmentUI.Disable();
    }

    /// <summary>
    /// U키를 눌러 장비창 onoff를 실행
    /// </summary>
    /// <param name="obj"></param>
    private void OnEquipmentOnOff(InputAction.CallbackContext obj)
    {
        UIOnOffSetting();
        RectTransform_UI.SetAsLastSibling();
    }

    /// <summary>
    /// 장비창 UI를 키거나 껐을 때 실행해야 될 메서드
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
            IsUIOnOff = true;

            CanvasGroupOnOff.alpha = 0;
            CanvasGroupOnOff.interactable = false;
            CanvasGroupOnOff.blocksRaycasts = false;

            UI_OnOff.IsUIOnOff();
        }
        
    }
    public void OnEquipmentItemUse(InputAction.CallbackContext obj)
    {
        RectTransform_UI.SetAsLastSibling();

        List<RaycastResult> slotItemCheck = new List<RaycastResult>();  //UI인식을 위해서는 GraphicRaycast가 필요하고 이걸 사용 후 리턴할 때 (RaycastResult)를 받는 리스트에 저장함
        UIPointerEventData = new PointerEventData(null);                  //GraphicRaycast에서 마우스 위치를 PointerEventData에서 받으므로 정의 해줌

        UIPointerEventData.position = Mouse.current.position.ReadValue();
        UIGraphicRaycaster.Raycast(uiPointerEventData, slotItemCheck);

        GameObject returnObject = slotItemCheck[0].gameObject;

        Debug.Log($"{returnObject.name}");

        EquipSlotUI tempEquipSlotUI = new();

        bool isFindEquipSlot;

        isFindEquipSlot = returnObject.TryGetComponent<EquipSlotUI>(out tempEquipSlotUI);

        if (isFindEquipSlot)    //장비슬롯에서 클릭을 했다면
        {
            ItemSlot tempItemSlot = new();
            tempItemSlot = playerInven.FindSameItemSlotForAddItem(tempEquipSlotUI.ItemData);    //빈 슬롯 찾고
            tempItemSlot.AssignSlotItem(tempEquipSlotUI.ItemData);                              //슬롯에 넣어준다.
            PlayerInvenUI.slotUIs[tempItemSlot.slotID].SetSlotWithData(tempEquipSlotUI.ItemData, 1);          //슬롯UI도 마찬가지

            Player.UnEquipWeaponAbility();     //무기데미지를 빼고 플레이어에 있는 myWeapon변수를 null로 만듬
            Player.isFindWeapon = false;
            tempEquipSlotUI.ClearTempSlot();    //장비슬롯은 비우고
            Destroy(FindObjectOfType<PlayerWeapon>().gameObject);   //무기를 찾아 지운다.
        }
    }
}

