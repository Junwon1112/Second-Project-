using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyUI : Num_UI_Basic
{
    protected Button okButton;
    protected Button cancelButton;
    protected TMP_InputField inputField;
    protected CanvasGroup numUI_CanvasGroup;
    protected Inventory inventory;
    protected InventoryUI inventoryUI;
    public RectTransform rectTransform;
    public int takeID;

    private Transform playerTransform;

    /// <summary>
    /// ItemSlotUI에서 받아옴
    /// </summary>
    public ItemData itemData;

    /// <summary>
    /// 스플릿 하기 직전에 아이템슬롯UI에서 데이터값을 그대로 할당해 줌
    /// </summary>
    public uint buyPossibleCount = 1;

    /// <summary>
    /// checkRightcount에서 최종적으로 할당해줌
    /// </summary>
    protected int buyCount = 0;

    public bool isSplitting = false;

    StoreUI storeUI;

    WarningUI warningUI;

    protected override Button OkButton { get; set; }
    protected override Button CancelButton { get; set; }
    protected override TMP_InputField InputField { get; set; }
    protected override CanvasGroup NumUI_CanvasGroup { get; set; }
    public override ItemData ItemData { get; set; }
    public override int TakeID { get; set; }
    protected override Inventory Inventory { get; set; }
    protected override InventoryUI InventoryUI { get; set; }
    public override RectTransform RectTransform { get; set; }
    public Transform PlayerTransform { get; set; }


    protected void Awake()
    {
        OkButton = transform.Find("OKButton").GetComponent<Button>();
        CancelButton = transform.Find("CancelButton").GetComponent<Button>();
        InputField = GetComponentInChildren<TMP_InputField>();
        NumUI_CanvasGroup = GetComponent<CanvasGroup>();
        RectTransform = GetComponent<RectTransform>();

        storeUI = FindObjectOfType<StoreUI>();
        warningUI = FindObjectOfType<WarningUI>();
    }

    protected void Start()
    {
        PlayerTransform = InGameManager.Instance.MainPlayer.transform;
        Inventory = FindObjectOfType<Inventory>();
        InventoryUI = FindObjectOfType<InventoryUI>();


        //inputField.
        InputField.onEndEdit.AddListener(this.CheckRightCount); //스트링타입 리턴받는 함수 실행  => 입력된 숫자가 슬롯의 itemCount보다 크면 itemCount를, 작으면 0을 리턴

        OkButton.onClick.AddListener(this.ClickOKButton);
        CancelButton.onClick.AddListener(ClickCancelButton);
    }

    /// <summary>
    /// UI를 열었을 때 보이게 만드는 메서드
    /// </summary>
    public override void NumUIOpen()
    {
        NumUI_CanvasGroup.alpha = 1.0f;
        NumUI_CanvasGroup.interactable = true;
        NumUI_CanvasGroup.blocksRaycasts = true;

        RectTransform.SetAsLastSibling();

        //시작하면 나오는 초기값을 제대로 설정해주는 과정 
        CheckRightCount(InputField.text);
    }

    /// <summary>
    /// UI를 열었을 때 보이게 만드는 메서드
    /// </summary>
    public override void NumUIClose()
    {
        NumUI_CanvasGroup.alpha = 0.0f;
        NumUI_CanvasGroup.interactable = false;
        NumUI_CanvasGroup.blocksRaycasts = false;
    }

    protected override void ClickOKButton()
    {
        bool isSuccessBuy;
        isSuccessBuy = Inventory.TakeItem(ItemData, (uint)buyCount);
        InventoryUI.SetAllSlotWithData();

        if(!isSuccessBuy)   //할당에 실패했다면
        {
            warningUI.UIOnOffSetting();
            warningUI.SetTextWarningInfo(WarningTextName.WarningText_BuyError);
            //경고창
        }
        else    //할당에 성공했다면
        {
            Inventory.Money -= (uint)ItemData.itemValue * (uint)buyCount;
            InventoryUI.SetAllSlotWithData();

            storeUI.SetItemDatas();
            storeUI.SetStoreSlotUIs();
        }
        NumUIClose();
    }

    /// <summary>
    /// 텍스트에 구매 갯수 입력시 맞는 숫자를 구매하는지 확인하는 함수 
    /// </summary>
    /// <param name="inputText"></param>
    protected override void CheckRightCount(string inputText) //텍스트에 구매 갯수 입력 시 실행
    {
        buyPossibleCount = Inventory.Money / (uint)ItemData.itemValue;
        bool isParsing = int.TryParse(inputText, out buyCount);
        if (buyCount > (int)buyPossibleCount)
        {
            buyCount = (int)buyPossibleCount;
        }
        else if (buyCount < 1)
        {
            buyCount = 1;
        }

        InputField.text = buyCount.ToString();
    }

    public override void ClickCancelButton()
    {
        NumUIClose();
    }
}
