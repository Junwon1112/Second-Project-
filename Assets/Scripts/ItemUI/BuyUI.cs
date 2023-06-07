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
    /// ItemSlotUI���� �޾ƿ�
    /// </summary>
    public ItemData itemData;

    /// <summary>
    /// ���ø� �ϱ� ������ �����۽���UI���� �����Ͱ��� �״�� �Ҵ��� ��
    /// </summary>
    public uint buyPossibleCount = 1;

    /// <summary>
    /// checkRightcount���� ���������� �Ҵ�����
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

        RectTransform.SetAsLastSibling();

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
        bool isSuccessBuy;
        isSuccessBuy = Inventory.TakeItem(ItemData, (uint)buyCount);
        InventoryUI.SetAllSlotWithData();

        if(!isSuccessBuy)   //�Ҵ翡 �����ߴٸ�
        {
            warningUI.UIOnOffSetting();
            warningUI.SetTextWarningInfo(WarningTextName.WarningText_BuyError);
            //���â
        }
        else    //�Ҵ翡 �����ߴٸ�
        {
            Inventory.Money -= (uint)ItemData.itemValue * (uint)buyCount;
            InventoryUI.SetAllSlotWithData();

            storeUI.SetItemDatas();
            storeUI.SetStoreSlotUIs();
        }
        NumUIClose();
    }

    /// <summary>
    /// �ؽ�Ʈ�� ���� ���� �Է½� �´� ���ڸ� �����ϴ��� Ȯ���ϴ� �Լ� 
    /// </summary>
    /// <param name="inputText"></param>
    protected override void CheckRightCount(string inputText) //�ؽ�Ʈ�� ���� ���� �Է� �� ����
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
