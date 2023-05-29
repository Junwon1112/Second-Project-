using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 어떤 UI라도 1개 이상 켜지면 플레이어 움직임을 멈추게 하기 위해 전체 UI를 관리하도록 만든 클래스
/// </summary>
public class UI_Player_MoveOnOff : MonoBehaviour
{
    public static UI_Player_MoveOnOff instance;

    /// <summary>
    /// playerInputSystem에 있는 Inventroy에 UI전용 우클릭 입력이 있어서 전체 UI에 사용하려면 끌어와야 했다. 
    /// 다음에는 전체 UI를 관리하는 InputSystem항목을 만들고 거기서 우클릭 입력을 구현해야 함 (확장성에 대한 고려 필요)
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
    /// UI가 켜지거나 꺼지면 플레이어가 움직일지 확인
    /// </summary>
    public void IsUIOnOff()    
    {
        uint count = 0;     //UI가 꺼진 갯수
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
                if(count >= canvasGroups.Length)    //모든 UI가 꺼졌으면 
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
