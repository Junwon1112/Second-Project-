using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// SplitUI를 상속받은 DropUI, 아이템을 밖에 버릴때 사용되는 클래스
/// </summary>
public class DropUI : Num_UI_Basic
{
    protected Button okButton;
    protected Button cancelButton;
    protected TMP_InputField inputField;
    protected CanvasGroup numUI_CanvasGroup;
    protected Inventory inventory;
    protected InventoryUI inventoryUI;
    public RectTransform rectTransform;
    public int takeID;

    /// <summary>
    // ItemSlotUI에서 받아옴
    /// </summary>
    public ItemData itemData;

    /// <summary>
    /// 스플릿 하기 직전에 아이템슬롯UI에서 데이터값을 그대로 할당해 줌
    /// </summary>
    public uint splitPossibleCount = 1;

    /// <summary>
    /// checkRightcount에서 최종적으로 할당해줌
    /// </summary>
    protected int splitCount = 0;

    public bool isSplitting = false;

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
    }

    protected void Start()
    {
        PlayerTransform = InGameManager.Instance.MainPlayer.transform;
        Inventory = InGameManager.Instance.MainPlayer.transform.GetComponentInChildren<Inventory>();
        InventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();


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

        SoundPlayer.Instance.PlaySound(SoundType_Effect.Sound_UI_Open);

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

        SoundPlayer.Instance.PlaySound(SoundType_Effect.Sound_UI_Close);
    }

    protected override void ClickOKButton()
    {
        splitPossibleCount -= (uint)splitCount;

        for(int i = 0; i < splitCount; i++)
        {
            ItemFactory.MakeItem(ItemData.ID, PlayerTransform.position, PlayerTransform.rotation);
        }

        if(splitPossibleCount > 0)  //현재 버리고 남은 총 갯수가 1개 이상이면 원래 슬롯에 아이템을 다시 만들어 준다.
        {
            Inventory.itemSlots[TakeID].AssignSlotItem(ItemData, splitPossibleCount);             //UI와 슬롯 데이터에서는 뺌
            InventoryUI.slotUIs[TakeID].SetSlotWithData(ItemData, splitPossibleCount);
        }

        NumUIClose();
    }

    /// <summary>
    /// 텍스트에 버릴 갯수 입력시 맞는 숫자를 버리는지 확인하는 함수 
    /// </summary>
    /// <param name="inputText"></param>
    protected override void CheckRightCount(string inputText) //텍스트에 버릴 갯수 입력 시 실행
    {

        //uint tempNum;
        //bool isParsing = uint.TryParse(splitUI.inputCount.text, out tempNum);
        bool isParsing = int.TryParse(inputText, out splitCount);
        if (splitCount > (int)splitPossibleCount)
        {
            splitCount = (int)splitPossibleCount;
        }
        else if (splitCount < 1)
        {
            splitCount = 1;
        }

        InputField.text = splitCount.ToString();
        //inputText = splitCount.ToString();
        //textCount = splitCount.ToString();
        //return textCount;
    }

    public override void ClickCancelButton()
    {
        Inventory.itemSlots[TakeID].AssignSlotItem(ItemData, splitPossibleCount);
        InventoryUI.slotUIs[TakeID].SetSlotWithData(ItemData, splitPossibleCount);
        NumUIClose();
    }
}
