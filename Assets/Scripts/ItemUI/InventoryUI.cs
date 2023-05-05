using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 인벤토리 UI에 나타낼 데이터 및 UI의 작동을 관리
/// </summary>
public class InventoryUI : BasicUIForm_Parent
{
    /// <summary>
    /// i키로 껐다키기위한 인풋시스템용 변수
    /// </summary>
    public PlayerInput input_Control;
    /// <summary>
    /// 껐다 키는걸 canvasGroup을 이용한 변수
    /// </summary>
    protected CanvasGroup canvasGroupOnOff;
    /// <summary>
    /// 인벤토리가 꺼져있는지 켜져있는지 확인하기 위한 변수
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
     * 인벤 관련 구현
     * 1. 창 위쪽 드래그하면 인벤토리 창 마우스 위치로 이동
     * 
     * 슬롯 관련 구현
     * 
     * 1. 우클릭 또는 더블클릭을 통한 아이템 사용 또는 장착  => 슬롯에서 구현
     * 2. 드래그를 통한 아이템 이동    => 슬롯에서 구현
     *      -마우스를 누르고 있는 상태에서 아이템 아이콘이 흐릿하게 보임
     *      -만약 외부로 이동시키면 아이템 외부에 몇 개 드롭 할건지 물어보기
     *      -이동한 자리에 다른 아이템이 있으면 자리를 바꾸고, 같은 아이템이면 몇 개 옮길건지 물어보는 창 생성
     * 3. 아이템 위에 커서를 뒀을때 아이템 Info창 표시   => 슬롯에서 구현
     * 4. 당연히 아이템 아이콘이 슬롯에 들어가도록 구현
     *      -슬롯 자식으로 아이콘(이미지)가 할당되도록 하는 함수 만들기
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
         *@details 게임 시작할 때 슬롯UI들 전부 초기화
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
    /// i키를 눌렀을 때 인벤토리 onoff
    /// </summary>
    /// <param name="obj"></param>
    private void OnInventoryOnOff(InputAction.CallbackContext obj)
    {
        UIOnOffSetting();
        RectTransform_UI.SetAsLastSibling();
    }

    /// <summary>
    /// 인벤토리 onoff시 실행할 메서드
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
    *장비창을 만들고 거기에 슬롯에 장착, 기존 인벤토리 슬롯에서는 사라짐
    *장비창에서 우클릭하면 다시 인벤토리로 이동하며 무기 해제
    *케릭터 손위치에 장착, 만약 이미 장착한 무기가 있다면 해당 슬롯에서 무기 교환
    *weapon에 equip에서 장착 구현
    *장비창 구현할 것
    *1.아이템 슬롯처럼 모든 데이터를 받을 변수들
    *2.우클릭하면 장착 해제
    */
    /// <summary>
    /// 우클릭시 아이템을 사용하게 하는 메서드, 인풋액션으로 구현했으므로 관리하기 편하려고 인벤토리에서 구현(onEnable에서 한번만 호출 하려고)
    /// </summary>
    /// <param name="obj"></param>
    public void OnInventoryItemUse(InputAction.CallbackContext obj)
    {
        RectTransform_UI.SetAsLastSibling();

        List<RaycastResult> slotItemCheck = new List<RaycastResult>();  //UI인식을 위해서는 GraphicRaycast가 필요하고 이걸 사용 후 리턴할 때 (RaycastResult)를 받는 리스트에 저장함
        UIPointerEventData = new PointerEventData(null);                  //GraphicRaycast에서 마우스 위치를 PointerEventData에서 받으므로 정의 해줌

        UIPointerEventData.position = Mouse.current.position.ReadValue();
        UIGraphicRaycaster.Raycast(uiPointerEventData, slotItemCheck);

        GameObject returnObject = slotItemCheck[0].gameObject;

        Debug.Log($"{returnObject.name}");
        
        ItemSlotUI tempSlotUI;

        bool isFindItemSlot = false;

        isFindItemSlot = returnObject.TryGetComponent<ItemSlotUI>(out tempSlotUI);

        if(isFindItemSlot)
        {

            if (tempSlotUI.ItemData.itemType == ItemType.ComsumableItem)   //data가 사용형 아이템이라면
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
            else if(tempSlotUI.ItemData.itemType == ItemType.Weapon && tempSlotUI.ItemData.job == player.Job)  //data가 무기고 플레이어와 직업이 같다면
            {
                for (int i = 0; i < EquipmentUI.equipSlotUIs.Length; i++)    //무기 슬롯을 찾아라
                {
                    if(EquipmentUI.equipSlotUIs[i].equipSlotID == 1001)     //무기 슬롯 ID는 1001이다.
                    {
                        if (EquipmentUI.equipSlotUIs[i].ItemData == null)   //현재 장착한 무기가 없을 떄
                        {
                            EquipmentUI.equipSlotUIs[i].SetTempSlotWithData(tempSlotUI.ItemData, 1);  //장비슬롯 설정
                            GameObject tempWeaponObject;    //장착한 아이템을 무기위치에 만들고 잘 작동되도록 player에서 TakeWeapon을 통해 컴포넌트를 가져온다.
                            tempWeaponObject = ItemFactory.MakeItem(tempSlotUI.ItemData.ID, Vector3.zero, Quaternion.identity); // player.weaponHandTransform.rotation
                            tempWeaponObject.layer = 10;    //9(Item)레이어에서 10(EquipItem)으로 변경 -> 아이템을 주울 때 layer로 판단하는데 장착한 무기가 주워지는것을 막기위해  
                            tempWeaponObject.transform.SetParent(player.weaponHandTransform, false);
                            Player.TakeWeapon();
                            Player.myWeapon = (ItemData_Weapon)tempSlotUI.ItemData;   //무기에 데미지를 추가하기 위해 플레이어에게 변수로 무기데이터 저장
                            Player.EquipWeaponAbility();     //플레이어에게 있는 무기 데미지와 자기 공격력 합치는 함수

                            tempSlotUI.SetSlotWithData(tempSlotUI.ItemData, 0);
                            PlayerInven.itemSlots[tempSlotUI.slotUIID].ClearSlotItem();
                        }
                        else    //현재 장착한 무기가 있을 때
                        {
                            ItemSlot tempItemSlot = new();
                            tempItemSlot.AssignSlotItem(equipmentUI.equipSlotUIs[i].ItemData);  //임시슬롯에 현재 무기창에 있는 데이터를 백업

                            Destroy(FindObjectOfType<PlayerWeapon>().gameObject);   //기존 무기 프리팹을 찾아 지운다.
                            Player.UnEquipWeaponAbility();       //무기데미지를 빼고 플레이어에 있는 myWeapon변수를 null로 만듬
                            EquipmentUI.equipSlotUIs[i].SetTempSlotWithData(tempSlotUI.ItemData, 1);    //장비슬롯에 인벤데이터를 할당하고

                            //무기프리팹을 할당하는 일련의 과정을 실행한다.
                            GameObject tempWeaponObject;    //장착한 아이템을 무기위치에 만들고 잘 작동되도록 player에서 TakeWeapon을 통해 컴포넌트를 가져온다.
                            tempWeaponObject = ItemFactory.MakeItem(tempSlotUI.ItemData.ID, Vector3.zero, Quaternion.identity); // player.weaponHandTransform.rotation
                            tempWeaponObject.transform.SetParent(player.weaponHandTransform, false);
                            Player.TakeWeapon();
                            Player.myWeapon = (ItemData_Weapon)tempSlotUI.ItemData;   //무기에 데미지를 추가하기 위해 플레이어에게 변수로 무기데이터 저장
                            Player.EquipWeaponAbility();     //플레이어에게 있는 무기 데미지와 자기 공격력 합치는 함수

                            //이제 인벤에서 바뀐 무기자리에 임시슬롯에 백업한 데이터를 저장
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
