using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellUI : Num_UI_Basic
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
    /// ItemSlotUI���� �޾ƿ�
    /// </summary>
    private ItemData itemData;
    private uint itemCount;

    /// <summary>
    /// ���ø� �ϱ� ������ �����۽���UI���� �����Ͱ��� �״�� �Ҵ��� ��
    /// </summary>
    public uint sellPossibleCount = 1;

    /// <summary>
    /// checkRightcount���� ���������� �Ҵ�����
    /// </summary>
    protected int sellCount = 0;

    public bool isSplitting = false;

    StoreUI storeUI;

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

    public uint ItemCount { get; set; }

    protected void Awake()
    {
        OkButton = transform.Find("OKButton").GetComponent<Button>();
        CancelButton = transform.Find("CancelButton").GetComponent<Button>();
        InputField = GetComponentInChildren<TMP_InputField>();
        NumUI_CanvasGroup = GetComponent<CanvasGroup>();
        RectTransform = GetComponent<RectTransform>();

        storeUI = FindObjectOfType<StoreUI>();
    }

    protected void Start()
    {
        PlayerTransform = InGameManager.Instance.MainPlayer.transform;
        Inventory = FindObjectOfType<Inventory>();
        InventoryUI = FindObjectOfType<InventoryUI>();


        //inputField.
        InputField.onEndEdit.AddListener(this.CheckRightCount); //��Ʈ��Ÿ�� ���Ϲ޴� �Լ� ����  => �Էµ� ���ڰ� ������ itemCount���� ũ�� itemCount��, ������ 0�� ����

        OkButton.onClick.AddListener(this.ClickOKButton);
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
        CheckRightCount(InputField.text);
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

    protected override void ClickOKButton()
    {
        Inventory.itemSlots[TakeID].DecreaseSlotItem((uint)sellCount);
        Inventory.Money += (uint)ItemData.itemValue * (uint)sellCount;

        InventoryUI.SetAllSlotWithData();

        storeUI.SetItemDatas();
        storeUI.SetStoreSlotUIs();
        
        NumUIClose();
    }

    /// <summary>
    /// �ؽ�Ʈ�� ���� ���� �Է½� �´� ���ڸ� �����ϴ��� Ȯ���ϴ� �Լ� 
    /// </summary>
    /// <param name="inputText"></param>
    protected override void CheckRightCount(string inputText) //�ؽ�Ʈ�� ���� ���� �Է� �� ����
    {
        sellPossibleCount = ItemCount;

        bool isParsing = int.TryParse(inputText, out sellCount);
        if (sellCount > (int)sellPossibleCount)
        {
            sellCount = (int)sellPossibleCount;
        }
        else if (sellCount < 1)
        {
            sellCount = 1;
        }

        InputField.text = sellCount.ToString();
    }

    public override void ClickCancelButton()
    {
        NumUIClose();
    }
}
