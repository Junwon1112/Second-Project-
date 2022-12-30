using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DropUI : SplitUI
{
    private Transform playerTransform;

    protected override void Awake()
    {
        okButton = transform.Find("OKButton").GetComponent<Button>();
        cancelButton = transform.Find("CancelButton").GetComponent<Button>();
        inputField = GetComponentInChildren<TMP_InputField>();
        splitUICanvasGroup = GetComponent<CanvasGroup>();
        inventory = FindObjectOfType<Inventory>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        splitTempSlotSplitUI = GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).GetComponent<TempSlotSplitUI>();   //Ȱ��ȭ�� ������Ʈ ã���� ������ �����ϰ�
        playerTransform = FindObjectOfType<Player>().transform;
    }

    protected override void Start()
    {
        //inputField.
        inputField.onEndEdit.AddListener(this.CheckRightCount); //��Ʈ��Ÿ�� ���Ϲ޴� �Լ� ����  => �Էµ� ���ڰ� ������ itemCount���� ũ�� itemCount��, ������ 0�� ����

        okButton.onClick.AddListener(this.ClickOKButton);
        cancelButton.onClick.AddListener(ClickCancelButton);

    }

    protected override void ClickOKButton()
    {
        splitPossibleCount -= (uint)splitCount;

        for(int i = 0; i < splitCount; i++)
        {
            ItemFactory.MakeItem(splitItemData.ID, playerTransform.position, playerTransform.rotation);
        }

        if(splitPossibleCount > 0)  //���� ������ ���� �� ������ 1�� �̻��̸� ���� ���Կ� �������� �ٽ� ����� �ش�.
        {
            inventory.itemSlots[takeID].AssignSlotItem(splitItemData, splitPossibleCount);             //UI�� ���� �����Ϳ����� ��
            inventoryUI.slotUIs[takeID].SetSlotWithData(splitItemData, splitPossibleCount);
        }

        SplitUIClose();
    }

    protected override void CheckRightCount(string inputText) //�ؽ�Ʈ�� ���� ���� �Է� �� ����
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

        inputField.text = splitCount.ToString();
        //inputText = splitCount.ToString();
        //textCount = splitCount.ToString();
        //return textCount;
    }

    public override void ClickCancelButton()
    {
        inventory.itemSlots[takeID].AssignSlotItem(splitItemData, splitPossibleCount);
        inventoryUI.slotUIs[takeID].SetSlotWithData(splitItemData, splitPossibleCount);
        SplitUIClose();
    }
}
//{
//    private Button okButton;
//    private Button cancelButton;
//    private TMP_InputField inputField;
//    private CanvasGroup splitUICanvasGroup;

//    public ItemData splitItemData;      //ItemSlotUI���� �޾ƿ�
//    public uint splitPossibleCount = 1; //���ø� �ϱ� ������ �����۽���UI���� �����Ͱ��� �״�� �Ҵ��� ��
//    private int splitCount = 0; //checkRightcount���� ���������� �Ҵ�����

//    public TempSlotSplitUI splitTempSlotSplitUI;
//    public int takeID = -1; //������ ���԰� UI�� ID�� �޾ƿ� ��

//    public bool isSplitting = false;

//    Inventory inventory;
//    InventoryUI inventoryUI;


//    private void Awake()
//    {
//        okButton = transform.Find("OKButton").GetComponent<Button>();
//        cancelButton = transform.Find("CancelButton").GetComponent<Button>();
//        inputField = FindObjectOfType<TMP_InputField>();
//        splitUICanvasGroup = GetComponent<CanvasGroup>();
//        inventory = FindObjectOfType<Inventory>();
//        inventoryUI = FindObjectOfType<InventoryUI>();
//        splitTempSlotSplitUI = GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).GetComponent<TempSlotSplitUI>();   //Ȱ��ȭ�� ������Ʈ ã���� ������ �����ϰ�
//    }

//    private void Start()
//    {
//        //inputField.
//        inputField.onEndEdit.AddListener(CheckRightCount); //��Ʈ��Ÿ�� ���Ϲ޴� �Լ� ����  => �Էµ� ���ڰ� ������ itemCount���� ũ�� itemCount��, ������ 0�� ����

//        okButton.onClick.AddListener(ClickOKButton);
//        //cancelButton.onClick.AddListener();

//    }

//    public void SplitUIOpen()
//    {
//        splitUICanvasGroup.alpha = 1.0f;
//        splitUICanvasGroup.interactable = true;
//        splitUICanvasGroup.blocksRaycasts = true;

//        //�����ϸ� ������ �ʱⰪ�� ����� �������ִ� ���� 
//        CheckRightCount(inputField.text);
//    }

//    public void SplitUIClose()
//    {
//        splitUICanvasGroup.alpha = 0.0f;
//        splitUICanvasGroup.interactable = false;
//        splitUICanvasGroup.blocksRaycasts = false;
//    }

//    public void CheckRightCount(string inputText) //�ش� ������ ������Ƽ�� �����ϸ� �Ǵ� ��������..
//    {

//        //uint tempNum;
//        //bool isParsing = uint.TryParse(splitUI.inputCount.text, out tempNum);
//        bool isParsing = int.TryParse(inputText, out splitCount);
//        if (splitCount > (int)splitPossibleCount-1)
//        {
//            splitCount = (int)splitPossibleCount-1;
//        }
//        else if (splitCount < 1)
//        {
//            splitCount = 1;
//        }

//        inputField.text = splitCount.ToString();
//        //inputText = splitCount.ToString();
//        //textCount = splitCount.ToString();
//        //return textCount;
//    }

//    public void ClickOKButton()
//    {
//        GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).gameObject.SetActive(true);  //tempSlot�� ��Ȱ��ȭ ���״� �θ������Ʈ�� ���� ã�Ƽ� Ȱ��ȭ ��ų���̴�.

//        //���� �������µ� ������ Ȱ��ȭ�� ���״µ�, awake�� ����Ǳ� ���� �Ʒ��Լ��� ���� ����Ǵµ� �ش� �Լ����� Awake���� getcomponent�ؾ��ϴ� ������ �����;ߵż� �������� ������ ����
//        splitTempSlotSplitUI.SetTempSlotWithData(splitItemData, (uint)splitCount);       //���� ������ tempslot�� �����ϰ�


//        isSplitting = true;

//        inventory.itemSlots[takeID].DecreaseSlotItem((uint)splitCount);             //UI�� ���� �����Ϳ����� ��
//        inventoryUI.slotUIs[takeID].slotUICount -= (uint)splitCount; ;

//        inventoryUI.SetAllSlotWithData();

//        SplitUIClose();
//    }

//    public void ClickCancelButton()
//    {
//        SplitUIClose();
//    }
//}