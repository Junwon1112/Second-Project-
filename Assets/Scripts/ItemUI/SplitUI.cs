using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 아이템 갯수를 나눌 때 사용하는 UI에 들어가는 클래스
/// </summary>
public class SplitUI : Num_UI_Basic
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
    /// ItemSlotUI에서 받아옴
    /// </summary>
    public ItemData itemData;

    protected override Button OkButton { get; set; }
    protected override Button CancelButton { get; set; }
    protected override TMP_InputField InputField { get; set; }
    protected override CanvasGroup NumUI_CanvasGroup { get; set; }
    public override ItemData ItemData { get; set; }
    public override int TakeID { get; set; }
    protected override Inventory Inventory { get; set; }
    protected override InventoryUI InventoryUI { get; set; }
    public override RectTransform RectTransform { get; set; }

    /// <summary>
    /// 스플릿 하기 직전에 아이템슬롯UI에서 데이터값을 그대로 할당해 줌
    /// </summary>
    public uint splitPossibleCount = 1;

    /// <summary>
    /// checkRightcount에서 최종적으로 할당해줌
    /// </summary>
    protected int splitCount = 0; 

    public TempSlotSplitUI splitTempSlotSplitUI;

    /// <summary>
    /// 아이템 슬롯과 UI의 ID를 받아올 값
    /// </summary>

    public bool isSplitting = false;



    protected virtual void Awake()
    {
        OkButton = transform.Find("OKButton").GetComponent<Button>();
        CancelButton = transform.Find("CancelButton").GetComponent<Button>();
        InputField = GetComponentInChildren<TMP_InputField>();
        NumUI_CanvasGroup = GetComponent<CanvasGroup>();
        splitTempSlotSplitUI = GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).GetComponent<TempSlotSplitUI>();   //활성화후 컴포넌트 찾은거 변수에 저장하고
        RectTransform = GetComponent<RectTransform>();
    }

    protected virtual void Start()
    {
        Inventory = GameManager.Instance.MainPlayer.transform.GetComponentInChildren<Inventory>();
        InventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();

        //스트링타입 리턴받는 함수 실행  => 입력된 숫자가 슬롯의 itemCount보다 크면 itemCount를, 작으면 0을 리턴
        InputField.onEndEdit.AddListener(CheckRightCount); 
        
        OkButton.onClick.AddListener(ClickOKButton);
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

        //시작하면 나오는 초기값을 제대로 설정해주는 과정 
        CheckRightCount(inputField.text);
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

    /// <summary>
    /// 텍스트에 나눌 갯수 입력 시 실행,입력된 숫자가 슬롯의 itemCount보다 크면 itemCount를, 작으면 0을 리턴
    /// </summary>
    /// <param name="inputText"></param>
    protected override void CheckRightCount(string inputText) 
    {
        
        //uint tempNum;
        //bool isParsing = uint.TryParse(splitUI.inputCount.text, out tempNum);
        bool isParsing = int.TryParse(inputText, out splitCount);
        if (splitCount > (int)splitPossibleCount-1)
        {
            splitCount = (int)splitPossibleCount-1;
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

    /// <summary>
    /// ok버튼 누를 시 사용될 메서드, 아이템을 나누는 작업 실행
    /// </summary>
    protected override void ClickOKButton()
    {
        GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).gameObject.SetActive(true);  //tempSlot을 비활성화 시켰다 부모오브젝트를 통해 찾아서 활성화 시킬것이다.
        
        splitTempSlotSplitUI.SetTempSlotWithData(itemData, (uint)splitCount);       //나눌 데이터 tempslot에 전달하고
        splitTempSlotSplitUI.rectTransform_TempSlotSplit.SetAsLastSibling();

        isSplitting = true;

        Inventory.itemSlots[takeID].DecreaseSlotItem((uint)splitCount);             //UI와 슬롯 데이터에서는 뺌
        InventoryUI.slotUIs[takeID].SlotUICount -= (uint)splitCount; ;

        InventoryUI.SetAllSlotWithData();

        NumUIClose();
    }

    public override void ClickCancelButton()
    {
        NumUIClose();
    }
}
