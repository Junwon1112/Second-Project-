using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ������ ������ ���� �� ����ϴ� UI�� ���� Ŭ����
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
    /// ItemSlotUI���� �޾ƿ�
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
    /// ���ø� �ϱ� ������ �����۽���UI���� �����Ͱ��� �״�� �Ҵ��� ��
    /// </summary>
    public uint splitPossibleCount = 1;

    /// <summary>
    /// checkRightcount���� ���������� �Ҵ�����
    /// </summary>
    protected int splitCount = 0; 

    public TempSlotSplitUI splitTempSlotSplitUI;

    /// <summary>
    /// ������ ���԰� UI�� ID�� �޾ƿ� ��
    /// </summary>

    public bool isSplitting = false;



    protected virtual void Awake()
    {
        OkButton = transform.Find("OKButton").GetComponent<Button>();
        CancelButton = transform.Find("CancelButton").GetComponent<Button>();
        InputField = GetComponentInChildren<TMP_InputField>();
        NumUI_CanvasGroup = GetComponent<CanvasGroup>();
        splitTempSlotSplitUI = GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).GetComponent<TempSlotSplitUI>();   //Ȱ��ȭ�� ������Ʈ ã���� ������ �����ϰ�
        RectTransform = GetComponent<RectTransform>();
    }

    protected virtual void Start()
    {
        Inventory = GameManager.Instance.MainPlayer.transform.GetComponentInChildren<Inventory>();
        InventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();

        //��Ʈ��Ÿ�� ���Ϲ޴� �Լ� ����  => �Էµ� ���ڰ� ������ itemCount���� ũ�� itemCount��, ������ 0�� ����
        InputField.onEndEdit.AddListener(CheckRightCount); 
        
        OkButton.onClick.AddListener(ClickOKButton);
        CancelButton.onClick.AddListener(ClickCancelButton);

    }

    /// <summary>
    /// UI�� ������ �� ���̰� ����� �޼���
    /// </summary>
    public override void NumUIOpen()
    {
        NumUI_CanvasGroup.alpha = 1.0f;
        NumUI_CanvasGroup.interactable = true;
        NumUI_CanvasGroup.blocksRaycasts = true;

        //�����ϸ� ������ �ʱⰪ�� ����� �������ִ� ���� 
        CheckRightCount(inputField.text);
    }

    /// <summary>
    /// UI�� ������ �� ���̰� ����� �޼���
    /// </summary>
    public override void NumUIClose()
    {
        NumUI_CanvasGroup.alpha = 0.0f;
        NumUI_CanvasGroup.interactable = false;
        NumUI_CanvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// �ؽ�Ʈ�� ���� ���� �Է� �� ����,�Էµ� ���ڰ� ������ itemCount���� ũ�� itemCount��, ������ 0�� ����
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
    /// ok��ư ���� �� ���� �޼���, �������� ������ �۾� ����
    /// </summary>
    protected override void ClickOKButton()
    {
        GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).gameObject.SetActive(true);  //tempSlot�� ��Ȱ��ȭ ���״� �θ������Ʈ�� ���� ã�Ƽ� Ȱ��ȭ ��ų���̴�.
        
        splitTempSlotSplitUI.SetTempSlotWithData(itemData, (uint)splitCount);       //���� ������ tempslot�� �����ϰ�
        splitTempSlotSplitUI.rectTransform_TempSlotSplit.SetAsLastSibling();

        isSplitting = true;

        Inventory.itemSlots[takeID].DecreaseSlotItem((uint)splitCount);             //UI�� ���� �����Ϳ����� ��
        InventoryUI.slotUIs[takeID].SlotUICount -= (uint)splitCount; ;

        InventoryUI.SetAllSlotWithData();

        NumUIClose();
    }

    public override void ClickCancelButton()
    {
        NumUIClose();
    }
}
