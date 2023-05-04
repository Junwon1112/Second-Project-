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

    GraphicRaycaster uiGraphicRaycaster;
    PointerEventData uiPointerEventData;
    Player player;
    UI_Player_MoveOnOff ui_OnOff;

    public EquipSlotUI[] equipSlotUIs;
    Inventory playerInven;

    public override PlayerInput Input_Control { get => input_Control; set => input_Control = value; }
    public override CanvasGroup CanvasGroupOnOff { get => canvasGroupOnOff; set => canvasGroupOnOff = value; }
    public override bool IsUIOnOff { get => isUIOnOff; set => isUIOnOff = value; }
    public override RectTransform RectTransform_UI { get => rectTransform_UI; set => rectTransform_UI = value; }
    public override GraphicRaycaster UIGraphicRaycaster { get => uiGraphicRaycaster; set => uiGraphicRaycaster = value; }
    public override PointerEventData UIPointerEventData { get => uiPointerEventData; set => uiPointerEventData = value; }


    public override Player Player { get => player; set => player = value; }
    public override UI_Player_MoveOnOff UI_OnOff { get => ui_OnOff; set => ui_OnOff = value; }

    public Inventory PlayerInven { get => playerInven; set => playerInven = value; }

    protected void Awake()
    {
        Input_Control = new PlayerInput();
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        RectTransform_UI = GetComponent<RectTransform>();

        UIGraphicRaycaster = GameObject.Find("Canvas").gameObject.GetComponent<GraphicRaycaster>();
        
        UI_OnOff = GetComponentInParent<UI_Player_MoveOnOff>();

        equipSlotUIs = GetComponentsInChildren<EquipSlotUI>();
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
                isUIOnOff = true;

                CanvasGroupOnOff.alpha = 0;
                CanvasGroupOnOff.interactable = false;
                CanvasGroupOnOff.blocksRaycasts = false;

                UI_OnOff.IsUIOnOff();
            }
        }
    }
    internal void OnInventoryItemUse(InputAction.CallbackContext obj)
    {
        
    }


}

