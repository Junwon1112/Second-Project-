using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// � UI�� 1�� �̻� ������ �÷��̾� �������� ���߰� �ϱ� ���� ��ü UI�� �����ϵ��� ���� Ŭ����
/// </summary>
public class UI_Player_MoveOnOff : MonoBehaviour
{
    public static UI_Player_MoveOnOff instance;

    /// <summary>
    /// playerInputSystem�� �ִ� Inventroy�� UI���� ��Ŭ�� �Է��� �־ ��ü UI�� ����Ϸ��� ����;� �ߴ�. 
    /// �������� ��ü UI�� �����ϴ� InputSystem�׸��� ����� �ű⼭ ��Ŭ�� �Է��� �����ؾ� �� (Ȯ�强�� ���� ��� �ʿ�)
    /// </summary>
    InventoryUI inventoryUI;
    EquipmentUI equipmentUI;

    bool isOnInventoryItemUseConnect = false;

    public CanvasGroup[] canvasGroups; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

    }

    private void Start()
    {
        inventoryUI = GetComponentInChildren<InventoryUI>();
        equipmentUI = GetComponentInChildren<EquipmentUI>();
        
    }


    /// <summary>
    /// UI�� �����ų� ������ �÷��̾ �������� Ȯ��
    /// </summary>
    public void IsUIOnOff()    
    {
        uint count = 0;     //UI�� ���� ����
        for (int i = 0; i < canvasGroups.Length; i++)
        {
            
            if (canvasGroups[i].interactable)
            {
                TotalGameManager.Instance.Input.Player.Disable();
                TotalGameManager.Instance.Input.QuickSlotUI.Disable();
                if (!isOnInventoryItemUseConnect)
                {
                    isOnInventoryItemUseConnect = true;
                    inventoryUI.Input_Control.InventoryUI.InventoryItemUse.performed += inventoryUI.OnInventoryItemUse;
                    equipmentUI.Input_Control.EquipmentUI.EquipmentItemUse.performed += equipmentUI.OnEquipmentItemUse;
                    Debug.Log("OnInventoryItemUseConnect");
                }
                break;
            }
            else
            {
                count++;
                if(count >= canvasGroups.Length)    //��� UI�� �������� 
                {
                    TotalGameManager.Instance.Input.Player.Enable();
                    TotalGameManager.Instance.Input.QuickSlotUI.Enable();
                    if (isOnInventoryItemUseConnect)
                    {
                        isOnInventoryItemUseConnect = false;
                        inventoryUI.Input_Control.InventoryUI.InventoryItemUse.performed -= inventoryUI.OnInventoryItemUse;
                        equipmentUI.Input_Control.EquipmentUI.EquipmentItemUse.performed -= equipmentUI.OnEquipmentItemUse;
                        Debug.Log("NO OnInventoryItemUseConnect");
                    }
                    
                    
                } 
            }
        }
    }

}
