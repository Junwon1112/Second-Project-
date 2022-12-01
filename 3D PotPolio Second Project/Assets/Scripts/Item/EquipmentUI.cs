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

    public bool isEquipCanvasGroupOff;   //인벤토리가 꺼져있는지 켜져있는지 확인하기 위한 변수
    //new public ItemSlotUI[] slotUIs;

    InventoryUI inventoryUI;

    public EquipSlotUI[] equipSlotUIs;

    protected override void Awake()
    {
        inventoryControl = new PlayerInput();   //아이템 우클릭은 inven에서 구현해서 필요할 때 가져오기 위해서
        equipmentControl = new PlayerInput();
        canvasGroupOnOff = GetComponent<CanvasGroup>();
        equipCloseButton = GetComponentInChildren<Button>();
        equipSlotUIs = GetComponentsInChildren<EquipSlotUI>();

        graphicRaycaster = GameObject.Find("Canvas").gameObject.GetComponent<GraphicRaycaster>();
        player = FindObjectOfType<Player>();
        inventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
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
            if(inventoryUI.isInvenCanvasGroupOff)
            {
                GameManager.Instance.MainPlayer.input.Disable();
                inventoryUI.inventoryControl.Inventory.InventoryItemUse.performed += OnInventoryItemUse;
            }
         

            canvasGroupOnOff.alpha = 1;
            canvasGroupOnOff.interactable = true;
            canvasGroupOnOff.blocksRaycasts = true;
        }
        else
        {
            isEquipCanvasGroupOff = true;
            if (inventoryUI.isInvenCanvasGroupOff)
            {
                GameManager.Instance.MainPlayer.input.Enable();
                inventoryUI.inventoryControl.Inventory.InventoryItemUse.performed -= OnInventoryItemUse;
            }

            canvasGroupOnOff.alpha = 0;
            canvasGroupOnOff.interactable = false;
            canvasGroupOnOff.blocksRaycasts = false;
        }
    }


}

//public class InventoryUI : MonoBehaviour
//{
//    public PlayerInput inventoryControl;   //i키로 껐다키기위한 인풋시스템용 변수
//    protected CanvasGroup canvasGroupOnOff;   //껐다 키는걸 canvasGroup을 이용한 변수
//    protected bool iscanvasGroupOff = true;   //인벤토리가 꺼져있는지 켜져있는지 확인하기 위한 변수

//    protected Button closeButton;

//    public ItemSlotUI[] slotUIs;
//    Inventory playerInven;

//    protected GraphicRaycaster graphicRaycaster;

//    protected PointerEventData pointerEventData;

//    protected Player player;

//    // 인벤토리 클릭관련 구현할 내용
//    /*
//     * 인벤 관련 구현
//     * 1. 창 위쪽 드래그하면 인벤토리 창 마우스 위치로 이동
//     * 
//     * 슬롯 관련 구현
//     * 
//     * 1. 우클릭 또는 더블클릭을 통한 아이템 사용 또는 장착  => 슬롯에서 구현
//     * 2. 드래그를 통한 아이템 이동    => 슬롯에서 구현
//     *      -마우스를 누르고 있는 상태에서 아이템 아이콘이 흐릿하게 보임
//     *      -만약 외부로 이동시키면 아이템 외부에 몇 개 드롭 할건지 물어보기
//     *      -이동한 자리에 다른 아이템이 있으면 자리를 바꾸고, 같은 아이템이면 몇 개 옮길건지 물어보는 창 생성
//     * 3. 아이템 위에 커서를 뒀을때 아이템 Info창 표시   => 슬롯에서 구현
//     * 4. 당연히 아이템 아이콘이 슬롯에 들어가도록 구현
//     *      -슬롯 자식으로 아이콘(이미지)가 할당되도록 하는 함수 만들기
//     *      
//     */


//    protected void Awake()
//    {
//        inventoryControl = new PlayerInput();
//        canvasGroupOnOff = GetComponent<CanvasGroup>();
//        closeButton = GetComponentInChildren<Button>();
//        slotUIs = GetComponentsInChildren<ItemSlotUI>();

//        playerInven = FindObjectOfType<Inventory>();
//        graphicRaycaster = GameObject.Find("Canvas").gameObject.GetComponent<GraphicRaycaster>();
//        player = FindObjectOfType<Player>();
//    }

//    protected void Start()
//    {
//        closeButton.onClick.AddListener(InventoryOnOffSetting);

//        SetAllSlotWithData();   //게임 시작할 때 슬롯UI들 전부 초기화
//    }

//    protected void OnEnable()
//    {
//        inventoryControl.Inventory.Enable();
//        inventoryControl.Inventory.InventoryOnOff.performed += OnInventoryOnOff;

//    }



//    protected void OnDisable()
//    {
//        inventoryControl.Inventory.InventoryOnOff.performed -= OnInventoryOnOff;
//        inventoryControl.Inventory.Disable();
//    }

//    protected void OnInventoryOnOff(InputAction.CallbackContext obj)
//    {
//        InventoryOnOffSetting();
//    }

//    protected void OnInventoryItemUse(InputAction.CallbackContext obj)    //우클릭으로 아이템 사용 및 장착을 위한 함수, 인풋액션으로 구현했으므로 관리하기 편하려고 인벤토리에서 구현(onEnable에서 한번만 호출 하려고)
//    {
//        List<RaycastResult> slotItemCheck = new List<RaycastResult>();  //UI인식을 위해서는 GraphicRaycast가 필요하고 이걸 사용 후 리턴할 때 (RaycastResult)를 받는 리스트에 저장함
//        pointerEventData = new PointerEventData(null);                  //GraphicRaycast에서 마우스 위치를 PointerEventData에서 받으므로 정의 해줌

//        pointerEventData.position = Mouse.current.position.ReadValue();
//        graphicRaycaster.Raycast(pointerEventData, slotItemCheck);

//        GameObject returnObject = slotItemCheck[0].gameObject;

//        Debug.Log($"{returnObject.name}");

//        ItemSlotUI tempSlotUI;
//        tempSlotUI = returnObject.GetComponent<ItemSlotUI>();

//        if (tempSlotUI != null)
//        {
//            if (tempSlotUI.slotUIData.ID == 0)   //data가 포션이라면 (포션id = 0)
//            {
//                ItemData_Potion tempPotion = new ItemData_Potion();
//                tempPotion.Use(player);
//                if (tempSlotUI.slotUICount <= 1)
//                {
//                    tempSlotUI.SetSlotWithData(tempSlotUI.slotUIData, 0);
//                    playerInven.itemSlots[tempSlotUI.slotUIID].ClearSlotItem();
//                }
//                else
//                {
//                    tempSlotUI.SetSlotWithData(tempSlotUI.slotUIData, tempSlotUI.slotUICount - 1);
//                    playerInven.itemSlots[tempSlotUI.slotUIID].DecreaseSlotItem(1);
//                }
//            }
//            else if (tempSlotUI.slotUIData.ID == 1)  //data가 무기라면
//            {
//                //장비창을 만들고 거기에 슬롯에 장착, 기존 인벤토리 슬롯에서는 사라짐
//                //장비창에서 우클릭하면 다시 인벤토리로 이동하며 무기 해제
//                //케릭터 손위치에 장착, 만약 이미 장착한 무기가 있다면 해당 슬롯에서 무기 교환
//                //weapon에 equip에서 장착 구현
//                //장비창 구현할 것
//                //1.아이템 슬롯처럼 모든 데이터를 받을 변수들
//                //2.우클릭하면 장착 해제
//            }
//        }

//    }

//    protected void InventoryOnOffSetting()
//    {
//        if (iscanvasGroupOff)
//        {
//            iscanvasGroupOff = false;
//            GameManager.Instance.MainPlayer.input.Disable();
//            inventoryControl.Inventory.InventoryItemUse.performed += OnInventoryItemUse;

//            canvasGroupOnOff.alpha = 1;
//            canvasGroupOnOff.interactable = true;
//            canvasGroupOnOff.blocksRaycasts = true;
//        }
//        else
//        {
//            iscanvasGroupOff = true;
//            GameManager.Instance.MainPlayer.input.Enable();
//            inventoryControl.Inventory.InventoryItemUse.performed -= OnInventoryItemUse;

//            canvasGroupOnOff.alpha = 0;
//            canvasGroupOnOff.interactable = false;
//            canvasGroupOnOff.blocksRaycasts = false;
//        }
//    }

//    public void SetAllSlotWithData()    //UI에 인벤토리 데이터를 넣어주는 함수
//    {
//        for (int i = 0; i < slotUIs.Length; i++)
//        {
//            slotUIs[i].SetSlotWithData(playerInven.itemSlots[i].SlotItemData, playerInven.itemSlots[i].ItemCount);
//            slotUIs[i].slotUIID = i;
//        }
//    }

//}
