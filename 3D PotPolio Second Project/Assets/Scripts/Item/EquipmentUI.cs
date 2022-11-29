using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EquipmentUI : InventoryUI
{
    public PlayerInput equipmentControl;   //uŰ�� ����Ű������ ��ǲ�ý��ۿ� ����

    protected CanvasGroup canvasGroupOnOff;   //���� Ű�°� canvasGroup�� �̿��� ����

    protected Button equipCloseButton;

    public bool isEquipCanvasGroupOff;   //�κ��丮�� �����ִ��� �����ִ��� Ȯ���ϱ� ���� ����
    //new public ItemSlotUI[] slotUIs;

    InventoryUI inventoryUI;

    protected override void Awake()
    {
        inventoryControl = new PlayerInput();   //������ ��Ŭ���� inven���� �����ؼ� �ʿ��� �� �������� ���ؼ�
        equipmentControl = new PlayerInput();
        canvasGroupOnOff = GetComponent<CanvasGroup>();
        equipCloseButton = GetComponentInChildren<Button>();
        slotUIs = GetComponentsInChildren<ItemSlotUI>();

        graphicRaycaster = GameObject.Find("Canvas").gameObject.GetComponent<GraphicRaycaster>();
        player = FindObjectOfType<Player>();
        inventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
    }

    private void Start()
    {
        equipCloseButton.onClick.AddListener(EquipmentOnOffSetting);
        isEquipCanvasGroupOff = true;
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
                inventoryControl.Inventory.InventoryItemUse.performed += OnInventoryItemUse;
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
                inventoryControl.Inventory.InventoryItemUse.performed -= OnInventoryItemUse;
            }

            canvasGroupOnOff.alpha = 0;
            canvasGroupOnOff.interactable = false;
            canvasGroupOnOff.blocksRaycasts = false;
        }
    }

    
}

//public class InventoryUI : MonoBehaviour
//{
//    public PlayerInput inventoryControl;   //iŰ�� ����Ű������ ��ǲ�ý��ۿ� ����
//    protected CanvasGroup canvasGroupOnOff;   //���� Ű�°� canvasGroup�� �̿��� ����
//    protected bool iscanvasGroupOff = true;   //�κ��丮�� �����ִ��� �����ִ��� Ȯ���ϱ� ���� ����

//    protected Button closeButton;

//    public ItemSlotUI[] slotUIs;
//    Inventory playerInven;

//    protected GraphicRaycaster graphicRaycaster;

//    protected PointerEventData pointerEventData;

//    protected Player player;

//    // �κ��丮 Ŭ������ ������ ����
//    /*
//     * �κ� ���� ����
//     * 1. â ���� �巡���ϸ� �κ��丮 â ���콺 ��ġ�� �̵�
//     * 
//     * ���� ���� ����
//     * 
//     * 1. ��Ŭ�� �Ǵ� ����Ŭ���� ���� ������ ��� �Ǵ� ����  => ���Կ��� ����
//     * 2. �巡�׸� ���� ������ �̵�    => ���Կ��� ����
//     *      -���콺�� ������ �ִ� ���¿��� ������ �������� �帴�ϰ� ����
//     *      -���� �ܺη� �̵���Ű�� ������ �ܺο� �� �� ��� �Ұ��� �����
//     *      -�̵��� �ڸ��� �ٸ� �������� ������ �ڸ��� �ٲٰ�, ���� �������̸� �� �� �ű���� ����� â ����
//     * 3. ������ ���� Ŀ���� ������ ������ Infoâ ǥ��   => ���Կ��� ����
//     * 4. �翬�� ������ �������� ���Կ� ������ ����
//     *      -���� �ڽ����� ������(�̹���)�� �Ҵ�ǵ��� �ϴ� �Լ� �����
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

//        SetAllSlotWithData();   //���� ������ �� ����UI�� ���� �ʱ�ȭ
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

//    protected void OnInventoryItemUse(InputAction.CallbackContext obj)    //��Ŭ������ ������ ��� �� ������ ���� �Լ�, ��ǲ�׼����� ���������Ƿ� �����ϱ� ���Ϸ��� �κ��丮���� ����(onEnable���� �ѹ��� ȣ�� �Ϸ���)
//    {
//        List<RaycastResult> slotItemCheck = new List<RaycastResult>();  //UI�ν��� ���ؼ��� GraphicRaycast�� �ʿ��ϰ� �̰� ��� �� ������ �� (RaycastResult)�� �޴� ����Ʈ�� ������
//        pointerEventData = new PointerEventData(null);                  //GraphicRaycast���� ���콺 ��ġ�� PointerEventData���� �����Ƿ� ���� ����

//        pointerEventData.position = Mouse.current.position.ReadValue();
//        graphicRaycaster.Raycast(pointerEventData, slotItemCheck);

//        GameObject returnObject = slotItemCheck[0].gameObject;

//        Debug.Log($"{returnObject.name}");

//        ItemSlotUI tempSlotUI;
//        tempSlotUI = returnObject.GetComponent<ItemSlotUI>();

//        if (tempSlotUI != null)
//        {
//            if (tempSlotUI.slotUIData.ID == 0)   //data�� �����̶�� (����id = 0)
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
//            else if (tempSlotUI.slotUIData.ID == 1)  //data�� ������
//            {
//                //���â�� ����� �ű⿡ ���Կ� ����, ���� �κ��丮 ���Կ����� �����
//                //���â���� ��Ŭ���ϸ� �ٽ� �κ��丮�� �̵��ϸ� ���� ����
//                //�ɸ��� ����ġ�� ����, ���� �̹� ������ ���Ⱑ �ִٸ� �ش� ���Կ��� ���� ��ȯ
//                //weapon�� equip���� ���� ����
//                //���â ������ ��
//                //1.������ ����ó�� ��� �����͸� ���� ������
//                //2.��Ŭ���ϸ� ���� ����
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

//    public void SetAllSlotWithData()    //UI�� �κ��丮 �����͸� �־��ִ� �Լ�
//    {
//        for (int i = 0; i < slotUIs.Length; i++)
//        {
//            slotUIs[i].SetSlotWithData(playerInven.itemSlots[i].SlotItemData, playerInven.itemSlots[i].ItemCount);
//            slotUIs[i].slotUIID = i;
//        }
//    }

//}
