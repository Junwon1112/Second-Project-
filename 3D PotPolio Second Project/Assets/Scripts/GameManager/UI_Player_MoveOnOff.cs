using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Player_MoveOnOff : MonoBehaviour
{
    InventoryUI inventoryUI;    //playerInputSystem�� �ִ� Inventroy�� UI���� ��Ŭ�� �Է��� �־ ��ü UI�� ����Ϸ��� ����;� �ߴ�. 
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

    //public void IsUIOnOff() //������ UI�鿡�� �ߵ��� ����
    //{
    //    //�ϳ��� UI�� ������ �ʾ��� �� � UI�� ������ �÷��̾� ������ Disable
    //    if(inventoryUI.isInvenCanvasGroupOff && equipmentUI.isEquipCanvasGroupOff && skillUI.isSkillWindowOff)
    //    {
    //        GameManager.Instance.MainPlayer.input.Disable();
    //    }
    //    // UI���� �߰�����
    //}

    public void IsUIOnOff2()    //UI�� �����ų� ������ �÷��̾ �������� Ȯ��
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
