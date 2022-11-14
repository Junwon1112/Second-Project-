using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    PlayerInput inventoryControl;   //i키로 껐다키기위한 인풋시스템용 변수
    CanvasGroup canvasGroupOnOff;   //껐다 키는걸 canvasGroup을 이용한 변수
    bool iscanvasGroupOff = true;   //인벤토리가 꺼져있는지 켜져있는지 확인하기 위한 변수

    Button closeButton;

    public ItemSlotUI[] slotUIs;
    Inventory playerInven;

    // 인벤토리 클릭관련 구현할 내용
    /*
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


    private void Awake()
    {
        inventoryControl = new PlayerInput();
        canvasGroupOnOff = GetComponent<CanvasGroup>();
        closeButton = GetComponentInChildren<Button>();
        slotUIs = GetComponentsInChildren<ItemSlotUI>();

        playerInven = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        closeButton.onClick.AddListener(InventoryOnOffSetting);

        SetAllSlotWithData();   //게임 시작할 때 슬롯UI들 전부 초기화
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

    private void InventoryOnOffSetting()
    {
        if (iscanvasGroupOff)
        {
            iscanvasGroupOff = false;
            GameManager.Instance.MainPlayer.input.Disable();

            canvasGroupOnOff.alpha = 1;
            canvasGroupOnOff.interactable = true;
            canvasGroupOnOff.blocksRaycasts = true;
        }
        else
        {
            iscanvasGroupOff = true;
            GameManager.Instance.MainPlayer.input.Enable();

            canvasGroupOnOff.alpha = 0;
            canvasGroupOnOff.interactable = false;
            canvasGroupOnOff.blocksRaycasts = false;
        }
    }

    public void SetAllSlotWithData()    //UI에 인벤토리 데이터를 넣어주는 함수
    {
        Debug.Log($"{slotUIs.Length}");
        for (int i = 0; i < slotUIs.Length; i++) 
        {
            
            slotUIs[i].SetSlotWithData(playerInven.itemSlots[i].SlotItemData, playerInven.itemSlots[i].ItemCount);
            slotUIs[i].slotUIID = i;
        }
    }

}
