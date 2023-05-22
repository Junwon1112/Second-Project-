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

    Inventory inven;
    InventoryUI invenUI;

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

        inven = FindObjectOfType<Inventory>();
        invenUI = FindObjectOfType<InventoryUI>();
    }

    protected void Start()
    {
        PlayerTransform = GameManager.Instance.MainPlayer.transform;
        Inventory = GameManager.Instance.MainPlayer.transform.GetComponentInChildren<Inventory>();
        InventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();


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
        bool isSuccessBuy;
        isSuccessBuy = inven.TakeItem(ItemData, (uint)buyCount);
        invenUI.SetAllSlotWithData();

        if(!isSuccessBuy)   //�Ҵ翡 �����ߴٸ�
        {
            //���â
        }
        else    //�Ҵ翡 �����ߴٸ�
        {
            inven.Money -= (uint)ItemData.itemValue * (uint)buyCount;
            invenUI.SetAllSlotWithData();
        }
        NumUIClose();
    }

    /// <summary>
    /// �ؽ�Ʈ�� ���� ���� �Է½� �´� ���ڸ� �����ϴ��� Ȯ���ϴ� �Լ� 
    /// </summary>
    /// <param name="inputText"></param>
    protected override void CheckRightCount(string inputText) //�ؽ�Ʈ�� ���� ���� �Է� �� ����
    {
        buyPossibleCount = inven.Money / (uint)ItemData.itemValue;

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
