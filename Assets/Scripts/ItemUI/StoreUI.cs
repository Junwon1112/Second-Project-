using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : BasicUIForm_Parent
{
    //public static StoreUI instance;
    private Merchant merchant;
    private Merchant_Trigger merchant_Trigger;

    [SerializeField]
    GameObject storeSlotUIObjects_Buy;
    [SerializeField]
    GameObject storeSlotUIObjects_Sell;

    /// <summary>
    /// i키로 껐다키기위한 인풋시스템용 변수
    /// </summary>
    PlayerInput input_Control;
    /// <summary>
    /// 껐다 키는걸 canvasGroup을 이용한 변수
    /// </summary>
    CanvasGroup canvasGroupOnOff;
    /// <summary>
    /// 인벤토리가 꺼져있는지 켜져있는지 확인하기 위한 변수
    /// </summary>
    bool isUIOnOff;

    RectTransform rectTransform_UI;     //StoreUI의 렉트 트랜스폼

    Player player;
    UI_Player_MoveOnOff ui_OnOff;

    private RectTransform storeBuySlotUIs_Rect;    //슬롯 UI들의 부모의 렉트 트랜스폼
    private RectTransform storeSellSlotUIs_Rect;    //슬롯 UI들의 부모의 렉트 트랜스폼

    Inventory inven;

    ItemData[] itemDatas_Buy;
    ItemData[] itemDatas_Sell;
    uint[] itemCounts_Sell = new uint[6];
    int[] targetItemSlotIDs = new int[6];

    Button sellTab;
    Button buyTab;

    RectTransform sellScrollView;
    RectTransform buyScrollView;

    public override PlayerInput Input_Control { get => input_Control; set => input_Control = value; }
    public override CanvasGroup CanvasGroupOnOff { get => canvasGroupOnOff; set => canvasGroupOnOff = value; }
    public override bool IsUIOnOff { get => isUIOnOff; set => isUIOnOff = value; }
    public override RectTransform RectTransform_UI { get => rectTransform_UI; set => rectTransform_UI = value; }
    public override Player Player { get => player; set => player = value; }
    public override UI_Player_MoveOnOff UI_OnOff { get => ui_OnOff; set => ui_OnOff = value; }

    private void OnEnable()
    {
        input_Control.StoreUI.Enable();
        input_Control.StoreUI.StoreUIOnOff.performed += OnStoreUIOnoff;
    }


    private void OnDisable()
    {
        input_Control.StoreUI.StoreUIOnOff.performed -= OnStoreUIOnoff;
        input_Control.StoreUI.Disable();
    }

    private void Awake()
    {
        //Initialize();

        Input_Control = TotalGameManager.Instance.Input;
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        RectTransform_UI = GetComponent<RectTransform>();
        UI_OnOff = GetComponentInParent<UI_Player_MoveOnOff>();

        merchant = FindObjectOfType<Merchant>();
        merchant_Trigger = FindObjectOfType<Merchant_Trigger>();

        

        sellTab = transform.GetChild(0).GetComponent<Button>();
        buyTab = transform.GetChild(1).GetComponent<Button>();

        storeSellSlotUIs_Rect = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        storeBuySlotUIs_Rect = transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<RectTransform>();

        buyScrollView = transform.Find("ScrollView_Buy").GetComponent<RectTransform>();
        sellScrollView = transform.Find("ScrollView_Sell").GetComponent<RectTransform>();
    }


    private void Start()
    {
        inven = FindObjectOfType<Inventory>();
        Player = InGameManager.Instance.MainPlayer;
        sellTab.onClick.AddListener(SetSellScroll);
        buyTab.onClick.AddListener(SetBuyScroll);

        itemDatas_Buy = new ItemData[merchant.sellingItems.Length];
        itemDatas_Sell = new ItemData[6];
    }

    public void SetItemDatas()
    {
        //구매할 수 있는 아이템 목록 세팅
        for(int i = 0; i < merchant.sellingItems.Length; i++)
        {
            itemDatas_Buy[i] = merchant.sellingItems[i];
        }

        //판매할 수 있는 아이템 목록 세팅
        int slotIndex = 0;

        for(int i = 0; i < inven.itemSlots.Length; i++)
        {
            //(할당 전 데이터 내용 초기화)
            itemDatas_Sell[i] = null; 
            itemCounts_Sell[i] = 0;
            targetItemSlotIDs[i] = -1;

            if (inven.itemSlots[i].SlotItemData != null)
            {
                itemDatas_Sell[slotIndex] = inven.itemSlots[i].SlotItemData;
                itemCounts_Sell[slotIndex] = inven.itemSlots[i].ItemCount;
                targetItemSlotIDs[slotIndex] = inven.itemSlots[i].slotID;
                slotIndex++;
            }
            
        }
    }

    /// <summary>
    /// G키를 이용해 스토어 UI를 킴, (상인앞에서만)
    /// </summary>
    /// <param name="obj"></param>
    private void OnStoreUIOnoff(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(merchant_Trigger.IsPlayerInTrigger)  //상인트리거 내부라면
        {
            UIOnOffSetting();
            RectTransform_UI.SetAsLastSibling();
        }
    }

    public void SetStoreSlotUIs()
    {
        ClearSlotUIs();
        SetStoreSlotUIsHeight();

        //구매슬롯 갱신
        for(int i = 0; i < itemDatas_Buy.Length; i++)
        {
            GameObject slotUIObj = Instantiate(storeSlotUIObjects_Buy, storeBuySlotUIs_Rect);
            StoreSlotUI_Buy storeSlotUI_Buy = slotUIObj.GetComponentInChildren<StoreSlotUI_Buy>();
            storeSlotUI_Buy.ItemData = itemDatas_Buy[i];
            storeSlotUI_Buy.SetItem(storeSlotUI_Buy.ItemData.itemIcon, storeSlotUI_Buy.ItemData.itemName, storeSlotUI_Buy.ItemData.itemValue);
        }

        //판매슬롯 갱신
        for (int i = 0; i < itemDatas_Sell.Length; i++)
        {
            if(itemDatas_Sell[i] != null)
            {
                GameObject slotUIObj = Instantiate(storeSlotUIObjects_Sell, storeSellSlotUIs_Rect);
                StoreSlotUI_Sell storeSlotUI_Sell = slotUIObj.GetComponentInChildren<StoreSlotUI_Sell>();
                storeSlotUI_Sell.ItemData = itemDatas_Sell[i];
                storeSlotUI_Sell.Count = itemCounts_Sell[i];
                storeSlotUI_Sell.ItemCount_Text.text = storeSlotUI_Sell.Count.ToString();
                storeSlotUI_Sell.targetItemSlotID = targetItemSlotIDs[i];
                storeSlotUI_Sell.SetItem(storeSlotUI_Sell.ItemData.itemIcon, storeSlotUI_Sell.ItemData.itemName, storeSlotUI_Sell.ItemData.itemValue);
            }
        }
    }

    private void SetStoreSlotUIsHeight()
    {
        float height_Buy = itemDatas_Buy.Length * 200.0f + 150;
        storeBuySlotUIs_Rect.sizeDelta = new Vector2(storeBuySlotUIs_Rect.rect.width, height_Buy);

        float height_Sell = itemDatas_Sell.Length * 200.0f + 150;
        storeBuySlotUIs_Rect.sizeDelta = new Vector2(storeBuySlotUIs_Rect.rect.width, height_Sell);
    }

    public void ClearSlotUIs()
    {
        if (storeBuySlotUIs_Rect.childCount > 0)
        {
            FindBuySlot[] buySlots = storeBuySlotUIs_Rect.GetComponentsInChildren<FindBuySlot>();
            for (int i = 0; i < buySlots.Length; i++)
            {
                Destroy(buySlots[i].gameObject);
            }
        }

        if (storeSellSlotUIs_Rect.childCount >0)
        {
            FindSellSlot[] sellSlots= storeSellSlotUIs_Rect.GetComponentsInChildren<FindSellSlot>();
            for(int i = 0; i < sellSlots.Length; i++)
            {
                Destroy(sellSlots[i].gameObject);
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        merchant = FindObjectOfType<Merchant>();
        //SetStoreSlotUIs();
    }

    public override void UIOnOffSetting()
    {
        if (IsUIOnOff)
        {
            IsUIOnOff = false;

            SetItemDatas();
            SetStoreSlotUIs();

            CanvasGroupOnOff.alpha = 1;
            CanvasGroupOnOff.interactable = true;
            CanvasGroupOnOff.blocksRaycasts = true;

            UI_OnOff.IsUIOnOff();
        }
        else
        {
            IsUIOnOff = true;

            CanvasGroupOnOff.alpha = 0;
            CanvasGroupOnOff.interactable = false;
            CanvasGroupOnOff.blocksRaycasts = false;

            UI_OnOff.IsUIOnOff();
        }
    }

    public void SetSellScroll()
    {
        sellScrollView.SetAsLastSibling();
    }

    public void SetBuyScroll()
    {
        buyScrollView.SetAsLastSibling();
    }
}
