using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// �κ��丮 UI�� ��ӹ��� ��� Ŭ����, �ϼ� �� UIŬ������ ���� ����� �� ��..�̶�� �� ������.
/// </summary>
public class EquipmentUI : BasicUIForm_Parent
{
    /// <summary>
    /// iŰ�� ����Ű������ ��ǲ�ý��ۿ� ����
    /// </summary>
    PlayerInput input_Control;
    /// <summary>
    /// ���� Ű�°� canvasGroup�� �̿��� ����
    /// </summary>
    CanvasGroup canvasGroupOnOff;
    /// <summary>
    /// �κ��丮�� �����ִ��� �����ִ��� Ȯ���ϱ� ���� ����
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
            equipSlotUIs[i].equipSlotID = 1001 + i; //1000���� ������ ��񽽷����� �����ϱ� ���� �߰�
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
    /// UŰ�� ���� ���â onoff�� ����
    /// </summary>
    /// <param name="obj"></param>
    private void OnEquipmentOnOff(InputAction.CallbackContext obj)
    {
        UIOnOffSetting();
        RectTransform_UI.SetAsLastSibling();
    }

    /// <summary>
    /// ���â UI�� Ű�ų� ���� �� �����ؾ� �� �޼���
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

        List<RaycastResult> slotItemCheck = new List<RaycastResult>();  //UI�ν��� ���ؼ��� GraphicRaycast�� �ʿ��ϰ� �̰� ��� �� ������ �� (RaycastResult)�� �޴� ����Ʈ�� ������
        UIPointerEventData = new PointerEventData(null);                  //GraphicRaycast���� ���콺 ��ġ�� PointerEventData���� �����Ƿ� ���� ����

        UIPointerEventData.position = Mouse.current.position.ReadValue();
        UIGraphicRaycaster.Raycast(uiPointerEventData, slotItemCheck);

        GameObject returnObject = slotItemCheck[0].gameObject;

        Debug.Log($"{returnObject.name}");

        EquipSlotUI tempEquipSlotUI = new();

        bool isFindEquipSlot;

        isFindEquipSlot = returnObject.TryGetComponent<EquipSlotUI>(out tempEquipSlotUI);

        if (isFindEquipSlot)    //��񽽷Կ��� Ŭ���� �ߴٸ�
        {
            ItemSlot tempItemSlot = new();
            tempItemSlot = playerInven.FindSameItemSlotForAddItem(tempEquipSlotUI.ItemData);    //�� ���� ã��
            tempItemSlot.AssignSlotItem(tempEquipSlotUI.ItemData);                              //���Կ� �־��ش�.
            PlayerInvenUI.slotUIs[tempItemSlot.slotID].SetSlotWithData(tempEquipSlotUI.ItemData, 1);          //����UI�� ��������

            Player.UnEquipWeaponAbility();     //���ⵥ������ ���� �÷��̾ �ִ� myWeapon������ null�� ����
            Player.isFindWeapon = false;
            tempEquipSlotUI.ClearTempSlot();    //��񽽷��� ����
            Destroy(FindObjectOfType<PlayerWeapon>().gameObject);   //���⸦ ã�� �����.
        }
    }
}

