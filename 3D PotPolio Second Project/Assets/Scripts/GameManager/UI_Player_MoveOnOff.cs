using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Player_MoveOnOff : MonoBehaviour
{
    InventoryUI inventoryUI;    //playerInputSystem에 있는 Inventroy에 UI전용 우클릭 입력이 있어서 전체 UI에 사용하려면 끌어와야 했다. 
    //EquipmentUI equipmentUI;
    //SkillUI skillUI;

    bool isOnInventoryItemUseConnect = false;

    public CanvasGroup[] canvasGroups; 

    private void Awake()
    {
        inventoryUI = GetComponentInChildren<InventoryUI>();
        //equipmentUI = GetComponentInChildren<EquipmentUI>();
        //skillUI = GetComponentInChildren<SkillUI>();
    }


    private void Start()
    {
        
    }

    //public void IsUIOnOff() //각각의 UI들에서 발동할 예정
    //{
    //    //하나의 UI도 켜지지 않았을 때 어떤 UI가 켜지면 플레이어 움직임 Disable
    //    if(inventoryUI.isInvenCanvasGroupOff && equipmentUI.isEquipCanvasGroupOff && skillUI.isSkillWindowOff)
    //    {
    //        GameManager.Instance.MainPlayer.input.Disable();
    //    }
    //    // UI끌때 추가예정
    //}

    public void IsUIOnOff2()    //UI가 켜지거나 꺼지면 플레이어가 움직일지 확인
    {
        uint count = 0;
        for (int i = 0; i < canvasGroups.Length; i++)
        {
            
            if (canvasGroups[i].interactable)
            {
                GameManager.Instance.MainPlayer.input.Disable();
                if(!isOnInventoryItemUseConnect)
                {
                    isOnInventoryItemUseConnect = true;
                    inventoryUI.inventoryControl.Inventory.InventoryItemUse.performed += inventoryUI.OnInventoryItemUse;
                    Debug.Log("OnInventoryItemUseConnect");
                }
                break;
            }
            else
            {
                count++;
                if(count >= canvasGroups.Length)
                {
                    GameManager.Instance.MainPlayer.input.Enable();
                    if(isOnInventoryItemUseConnect)
                    {
                        isOnInventoryItemUseConnect = false;
                        inventoryUI.inventoryControl.Inventory.InventoryItemUse.performed -= inventoryUI.OnInventoryItemUse;
                        Debug.Log("NO OnInventoryItemUseConnect");
                    }
                    
                    
                } 
            }
        }
    }

}
