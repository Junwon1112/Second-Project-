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
    public bool isInvenCanvasGroupOff = true;   //�κ��丮�� �����ִ��� �����ִ��� Ȯ���ϱ� ���� ����

    

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


    //���â�� ����� �ű⿡ ���Կ� ����, ���� �κ��丮 ���Կ����� �����
    //���â���� ��Ŭ���ϸ� �ٽ� �κ��丮�� �̵��ϸ� ���� ����
    //�ɸ��� ����ġ�� ����, ���� �̹� ������ ���Ⱑ �ִٸ� �ش� ���Կ��� ���� ��ȯ
    //weapon�� equip���� ���� ����
    //���â ������ ��
    //1.������ ����ó�� ��� �����͸� ���� ������
    //2.��Ŭ���ϸ� ���� ����
    public void OnInventoryItemUse(InputAction.CallbackContext obj)    //��Ŭ������ ������ ��� �� ������ ���� �Լ�, ��ǲ�׼����� ���������Ƿ� �����ϱ� ���Ϸ��� �κ��丮���� ����(onEnable���� �ѹ��� ȣ�� �Ϸ���)
    {
        List<RaycastResult> slotItemCheck = new List<RaycastResult>();  //UI�ν��� ���ؼ��� GraphicRaycast�� �ʿ��ϰ� �̰� ��� �� ������ �� (RaycastResult)�� �޴� ����Ʈ�� ������
        pointerEventData = new PointerEventData(null);                  //GraphicRaycast���� ���콺 ��ġ�� PointerEventData���� �����Ƿ� ���� ����

        pointerEventData.position = Mouse.current.position.ReadValue();
        graphicRaycaster.Raycast(pointerEventData, slotItemCheck);

        GameObject returnObject = slotItemCheck[0].gameObject;

        Debug.Log($"{returnObject.name}");
        
        ItemSlotUI tempSlotUI;
        EquipSlotUI tempEquipSlotUI = new();

        bool isFindEquipSlot = false;
        bool isFindItemSlot = false;

        isFindItemSlot = returnObject.TryGetComponent<ItemSlotUI>(out tempSlotUI);
        if(!isFindItemSlot)
        {
            
            isFindEquipSlot = returnObject.TryGetComponent<EquipSlotUI>(out tempEquipSlotUI);
        }

        if(isFindItemSlot)
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
            else if(tempSlotUI.slotUIData.ID == 1 || tempSlotUI.slotUIData.ID == 2)  //data�� ������
            {
                for (int i = 0; i < equipmentUI.equipSlotUIs.Length; i++)    //���� ������ ã�ƶ�
                {
                    if(equipmentUI.equipSlotUIs[i].equipSlotID == 1001)     //���� ���� ID�� 1001�̴�.
                    {
                        if (equipmentUI.equipSlotUIs[i].takeSlotItemData == null)   //���� ������ ���Ⱑ ���� ��
                        {
                            equipmentUI.equipSlotUIs[i].SetTempSlotWithData(tempSlotUI.slotUIData, 1);  //��񽽷� ����
                            GameObject tempWeaponObject;    //������ �������� ������ġ�� ����� �� �۵��ǵ��� player���� TakeWeapon�� ���� ������Ʈ�� �����´�.
                            tempWeaponObject = ItemFactory.MakeItem(tempSlotUI.slotUIData.ID, Vector3.zero, Quaternion.identity); // player.weaponHandTransform.rotation
                            tempWeaponObject.transform.SetParent(player.weaponHandTransform, false);
                            player.TakeWeapon();
                            player.myWeapon = (ItemData_Weapon)tempSlotUI.slotUIData;   //���⿡ �������� �߰��ϱ� ���� �÷��̾�� ������ ���ⵥ���� ����
                            player.EquipWeaponAbility();     //�÷��̾�� �ִ� ���� �������� �ڱ� ���ݷ� ��ġ�� �Լ�

                            tempSlotUI.SetSlotWithData(tempSlotUI.slotUIData, 0);
                            playerInven.itemSlots[tempSlotUI.slotUIID].ClearSlotItem();
                        }
                        else    //���� ������ ���Ⱑ ���� ��
                        {
                            ItemSlot tempItemSlot = new();
                            tempItemSlot.AssignSlotItem(equipmentUI.equipSlotUIs[i].takeSlotItemData);  //�ӽý��Կ� ���� ����â�� �ִ� �����͸� ���

                            Destroy(FindObjectOfType<PlayerWeapon>().gameObject);   //���� ���� �������� ã�� �����.
                            player.UnEquipWeaponAbility();       //���ⵥ������ ���� �÷��̾ �ִ� myWeapon������ null�� ����
                            equipmentUI.equipSlotUIs[i].SetTempSlotWithData(tempSlotUI.slotUIData, 1);    //��񽽷Կ� �κ������͸� �Ҵ��ϰ�

                            //������������ �Ҵ��ϴ� �Ϸ��� ������ �����Ѵ�.
                            GameObject tempWeaponObject;    //������ �������� ������ġ�� ����� �� �۵��ǵ��� player���� TakeWeapon�� ���� ������Ʈ�� �����´�.
                            tempWeaponObject = ItemFactory.MakeItem(tempSlotUI.slotUIData.ID, Vector3.zero, Quaternion.identity); // player.weaponHandTransform.rotation
                            tempWeaponObject.transform.SetParent(player.weaponHandTransform, false);
                            player.TakeWeapon();
                            player.myWeapon = (ItemData_Weapon)tempSlotUI.slotUIData;   //���⿡ �������� �߰��ϱ� ���� �÷��̾�� ������ ���ⵥ���� ����
                            player.EquipWeaponAbility();     //�÷��̾�� �ִ� ���� �������� �ڱ� ���ݷ� ��ġ�� �Լ�

                            //���� �κ����� �ٲ� �����ڸ��� �ӽý��Կ� ����� �����͸� ����
                            playerInven.itemSlots[tempSlotUI.slotUIID].AssignSlotItem(tempItemSlot.SlotItemData);
                            slotUIs[tempSlotUI.slotUIID].SetSlotWithData(tempItemSlot.SlotItemData, 1);

                        }
                        
                    }   
                }

            }
        }
        else if(isFindEquipSlot)    //��񽽷Կ��� Ŭ���� �ߴٸ�
        {
            ItemSlot tempItemSlot = new();
            tempItemSlot = playerInven.FindSameItemSlotForAddItem(tempEquipSlotUI.takeSlotItemData);    //�� ���� ã��
            tempItemSlot.AssignSlotItem(tempEquipSlotUI.takeSlotItemData);                              //���Կ� �־��ش�.
            slotUIs[tempItemSlot.slotID].SetSlotWithData(tempEquipSlotUI.takeSlotItemData, 1);          //����UI�� ��������
            
            player.UnEquipWeaponAbility();     //���ⵥ������ ���� �÷��̾ �ִ� myWeapon������ null�� ����

            tempEquipSlotUI.ClearTempSlot();    //��񽽷��� ����
            Destroy(FindObjectOfType<PlayerWeapon>().gameObject);   //���⸦ ã�� �����.

            //StartCoroutine();
        }
    }

    //IEnumerator IsFinished()
    //{
    //    yield return new wait
    //}

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