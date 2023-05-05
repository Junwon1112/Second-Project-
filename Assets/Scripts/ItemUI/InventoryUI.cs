using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// �κ��丮 UI�� ��Ÿ�� ������ �� UI�� �۵��� ����
/// </summary>
public class InventoryUI : BasicUIForm_Parent
{
    /// <summary>
    /// iŰ�� ����Ű������ ��ǲ�ý��ۿ� ����
    /// </summary>
    public PlayerInput input_Control;
    /// <summary>
    /// ���� Ű�°� canvasGroup�� �̿��� ����
    /// </summary>
    protected CanvasGroup canvasGroupOnOff;
    /// <summary>
    /// �κ��丮�� �����ִ��� �����ִ��� Ȯ���ϱ� ���� ����
    /// </summary>
    public bool isUIOnOff;

    RectTransform rectTransform_UI;

    protected GraphicRaycaster uiGraphicRaycaster;
    protected PointerEventData uiPointerEventData;

    protected Player player;
    UI_Player_MoveOnOff ui_OnOff;

    public ItemSlotUI[] slotUIs;
    Inventory playerInven;

    private EquipmentUI equipmentUI;

    public override PlayerInput Input_Control { get => input_Control; set => input_Control = value; }
    public override CanvasGroup CanvasGroupOnOff { get => canvasGroupOnOff; set => canvasGroupOnOff = value; }
    public override bool IsUIOnOff { get => isUIOnOff; set => isUIOnOff = value; }
    public override RectTransform RectTransform_UI { get => rectTransform_UI; set => rectTransform_UI = value; }
    public override GraphicRaycaster UIGraphicRaycaster { get => uiGraphicRaycaster; set => uiGraphicRaycaster = value; }
    public override PointerEventData UIPointerEventData { get => uiPointerEventData; set => uiPointerEventData = value; }
    public override Player Player { get => player; set => player = value; }
    public override UI_Player_MoveOnOff UI_OnOff { get => ui_OnOff; set => ui_OnOff = value; }


    public Inventory PlayerInven { get => playerInven; set => playerInven = value; }
    public EquipmentUI EquipmentUI { get => equipmentUI; set => equipmentUI = value; }
    /**
     *@brief
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




    protected void Awake()
    {
        Input_Control = new PlayerInput();
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        slotUIs = GetComponentsInChildren<ItemSlotUI>();
        RectTransform_UI = GetComponent<RectTransform>();
        UIGraphicRaycaster = GameObject.Find("Canvas").gameObject.GetComponent<GraphicRaycaster>();
        
        equipmentUI = FindObjectOfType<EquipmentUI>();
        UI_OnOff = GetComponentInParent<UI_Player_MoveOnOff>();
    }

    private void Start()
    {
        Player = GameManager.Instance.MainPlayer;
        PlayerInven = GameManager.Instance.MainPlayer.transform.GetComponentInChildren<Inventory>();

        /**
         *@details ���� ������ �� ����UI�� ���� �ʱ�ȭ
        */

        SetAllSlotWithData();   

        IsUIOnOff = true;

    }

    private void OnEnable()
    {
        Input_Control.InventoryUI.Enable();
        Input_Control.InventoryUI.InventoryOnOff.performed += OnInventoryOnOff;
    }

    

    private void OnDisable()
    {
        Input_Control.InventoryUI.InventoryOnOff.performed -= OnInventoryOnOff;
        Input_Control.InventoryUI.Disable();
    }

    /// <summary>
    /// iŰ�� ������ �� �κ��丮 onoff
    /// </summary>
    /// <param name="obj"></param>
    private void OnInventoryOnOff(InputAction.CallbackContext obj)
    {
        UIOnOffSetting();
        RectTransform_UI.SetAsLastSibling();
    }

    /// <summary>
    /// �κ��丮 onoff�� ������ �޼���
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
            isUIOnOff = true;

            CanvasGroupOnOff.alpha = 0;
            CanvasGroupOnOff.interactable = false;
            CanvasGroupOnOff.blocksRaycasts = false;

            UI_OnOff.IsUIOnOff();
        }
    }


    /**
    *@brief
    *���â�� ����� �ű⿡ ���Կ� ����, ���� �κ��丮 ���Կ����� �����
    *���â���� ��Ŭ���ϸ� �ٽ� �κ��丮�� �̵��ϸ� ���� ����
    *�ɸ��� ����ġ�� ����, ���� �̹� ������ ���Ⱑ �ִٸ� �ش� ���Կ��� ���� ��ȯ
    *weapon�� equip���� ���� ����
    *���â ������ ��
    *1.������ ����ó�� ��� �����͸� ���� ������
    *2.��Ŭ���ϸ� ���� ����
    */
    /// <summary>
    /// ��Ŭ���� �������� ����ϰ� �ϴ� �޼���, ��ǲ�׼����� ���������Ƿ� �����ϱ� ���Ϸ��� �κ��丮���� ����(onEnable���� �ѹ��� ȣ�� �Ϸ���)
    /// </summary>
    /// <param name="obj"></param>
    public void OnInventoryItemUse(InputAction.CallbackContext obj)
    {
        RectTransform_UI.SetAsLastSibling();

        List<RaycastResult> slotItemCheck = new List<RaycastResult>();  //UI�ν��� ���ؼ��� GraphicRaycast�� �ʿ��ϰ� �̰� ��� �� ������ �� (RaycastResult)�� �޴� ����Ʈ�� ������
        UIPointerEventData = new PointerEventData(null);                  //GraphicRaycast���� ���콺 ��ġ�� PointerEventData���� �����Ƿ� ���� ����

        UIPointerEventData.position = Mouse.current.position.ReadValue();
        UIGraphicRaycaster.Raycast(uiPointerEventData, slotItemCheck);

        GameObject returnObject = slotItemCheck[0].gameObject;

        Debug.Log($"{returnObject.name}");
        
        ItemSlotUI tempSlotUI;

        bool isFindItemSlot = false;

        isFindItemSlot = returnObject.TryGetComponent<ItemSlotUI>(out tempSlotUI);

        if(isFindItemSlot)
        {

            if (tempSlotUI.ItemData.itemType == ItemType.ComsumableItem)   //data�� ����� �������̶��
            {
                ItemData_Potion tempPotion = new ItemData_Potion();
                tempPotion.Use(player);
                if (tempSlotUI.SlotUICount <= 1)
                {
                    tempSlotUI.SetSlotWithData(tempSlotUI.ItemData, 0);
                    PlayerInven.itemSlots[tempSlotUI.slotUIID].ClearSlotItem();
                }
                else
                {
                    tempSlotUI.SetSlotWithData(tempSlotUI.ItemData, tempSlotUI.SlotUICount - 1);
                    PlayerInven.itemSlots[tempSlotUI.slotUIID].DecreaseSlotItem(1);
                }
            }
            else if(tempSlotUI.ItemData.itemType == ItemType.Weapon && tempSlotUI.ItemData.job == player.Job)  //data�� ����� �÷��̾�� ������ ���ٸ�
            {
                for (int i = 0; i < EquipmentUI.equipSlotUIs.Length; i++)    //���� ������ ã�ƶ�
                {
                    if(EquipmentUI.equipSlotUIs[i].equipSlotID == 1001)     //���� ���� ID�� 1001�̴�.
                    {
                        if (EquipmentUI.equipSlotUIs[i].ItemData == null)   //���� ������ ���Ⱑ ���� ��
                        {
                            EquipmentUI.equipSlotUIs[i].SetTempSlotWithData(tempSlotUI.ItemData, 1);  //��񽽷� ����
                            GameObject tempWeaponObject;    //������ �������� ������ġ�� ����� �� �۵��ǵ��� player���� TakeWeapon�� ���� ������Ʈ�� �����´�.
                            tempWeaponObject = ItemFactory.MakeItem(tempSlotUI.ItemData.ID, Vector3.zero, Quaternion.identity); // player.weaponHandTransform.rotation
                            tempWeaponObject.layer = 10;    //9(Item)���̾�� 10(EquipItem)���� ���� -> �������� �ֿ� �� layer�� �Ǵ��ϴµ� ������ ���Ⱑ �ֿ����°��� ��������  
                            tempWeaponObject.transform.SetParent(player.weaponHandTransform, false);
                            Player.TakeWeapon();
                            Player.myWeapon = (ItemData_Weapon)tempSlotUI.ItemData;   //���⿡ �������� �߰��ϱ� ���� �÷��̾�� ������ ���ⵥ���� ����
                            Player.EquipWeaponAbility();     //�÷��̾�� �ִ� ���� �������� �ڱ� ���ݷ� ��ġ�� �Լ�

                            tempSlotUI.SetSlotWithData(tempSlotUI.ItemData, 0);
                            PlayerInven.itemSlots[tempSlotUI.slotUIID].ClearSlotItem();
                        }
                        else    //���� ������ ���Ⱑ ���� ��
                        {
                            ItemSlot tempItemSlot = new();
                            tempItemSlot.AssignSlotItem(equipmentUI.equipSlotUIs[i].ItemData);  //�ӽý��Կ� ���� ����â�� �ִ� �����͸� ���

                            Destroy(FindObjectOfType<PlayerWeapon>().gameObject);   //���� ���� �������� ã�� �����.
                            Player.UnEquipWeaponAbility();       //���ⵥ������ ���� �÷��̾ �ִ� myWeapon������ null�� ����
                            EquipmentUI.equipSlotUIs[i].SetTempSlotWithData(tempSlotUI.ItemData, 1);    //��񽽷Կ� �κ������͸� �Ҵ��ϰ�

                            //������������ �Ҵ��ϴ� �Ϸ��� ������ �����Ѵ�.
                            GameObject tempWeaponObject;    //������ �������� ������ġ�� ����� �� �۵��ǵ��� player���� TakeWeapon�� ���� ������Ʈ�� �����´�.
                            tempWeaponObject = ItemFactory.MakeItem(tempSlotUI.ItemData.ID, Vector3.zero, Quaternion.identity); // player.weaponHandTransform.rotation
                            tempWeaponObject.transform.SetParent(player.weaponHandTransform, false);
                            Player.TakeWeapon();
                            Player.myWeapon = (ItemData_Weapon)tempSlotUI.ItemData;   //���⿡ �������� �߰��ϱ� ���� �÷��̾�� ������ ���ⵥ���� ����
                            Player.EquipWeaponAbility();     //�÷��̾�� �ִ� ���� �������� �ڱ� ���ݷ� ��ġ�� �Լ�

                            //���� �κ����� �ٲ� �����ڸ��� �ӽý��Կ� ����� �����͸� ����
                            PlayerInven.itemSlots[tempSlotUI.slotUIID].AssignSlotItem(tempItemSlot.SlotItemData);
                            slotUIs[tempSlotUI.slotUIID].SetSlotWithData(tempItemSlot.SlotItemData, 1);

                        }
                        
                    }   
                }

            }
        }
    }

    public void SetAllSlotWithData()    
    {
        for (int i = 0; i < slotUIs.Length; i++) 
        {  
            slotUIs[i].SetSlotWithData(PlayerInven.itemSlots[i].SlotItemData, PlayerInven.itemSlots[i].ItemCount);
            slotUIs[i].slotUIID = i;
        }
    }

}
