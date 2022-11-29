using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SplitUI : MonoBehaviour
{
    protected Button okButton;
    protected Button cancelButton;
    protected TMP_InputField inputField;
    protected CanvasGroup splitUICanvasGroup;

    public ItemData splitItemData;      //ItemSlotUI���� �޾ƿ�
    public uint splitPossibleCount = 1; //���ø� �ϱ� ������ �����۽���UI���� �����Ͱ��� �״�� �Ҵ��� ��
    protected int splitCount = 0; //checkRightcount���� ���������� �Ҵ�����

    public TempSlotSplitUI splitTempSlotSplitUI;
    public int takeID = -1; //������ ���԰� UI�� ID�� �޾ƿ� ��

    public bool isSplitting = false;

    protected Inventory inventory;
    protected InventoryUI inventoryUI;


    protected virtual void Awake()
    {
        okButton = transform.Find("OKButton").GetComponent<Button>();
        cancelButton = transform.Find("CancelButton").GetComponent<Button>();
        inputField = GetComponentInChildren<TMP_InputField>();
        splitUICanvasGroup = GetComponent<CanvasGroup>();
        inventory = FindObjectOfType<Inventory>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        splitTempSlotSplitUI = GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).GetComponent<TempSlotSplitUI>();   //Ȱ��ȭ�� ������Ʈ ã���� ������ �����ϰ�
    }

    protected virtual void Start()
    {
        //inputField.
        inputField.onEndEdit.AddListener(CheckRightCount); //��Ʈ��Ÿ�� ���Ϲ޴� �Լ� ����  => �Էµ� ���ڰ� ������ itemCount���� ũ�� itemCount��, ������ 0�� ����
        
        okButton.onClick.AddListener(ClickOKButton);
        cancelButton.onClick.AddListener(ClickCancelButton);

    }

    public void SplitUIOpen()
    {
        splitUICanvasGroup.alpha = 1.0f;
        splitUICanvasGroup.interactable = true;
        splitUICanvasGroup.blocksRaycasts = true;

        //�����ϸ� ������ �ʱⰪ�� ����� �������ִ� ���� 
        CheckRightCount(inputField.text);
    }

    public void SplitUIClose()
    {
        splitUICanvasGroup.alpha = 0.0f;
        splitUICanvasGroup.interactable = false;
        splitUICanvasGroup.blocksRaycasts = false;
    }

    protected virtual void CheckRightCount(string inputText) //�ؽ�Ʈ�� ���� ���� �Է� �� ����
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

        inputField.text = splitCount.ToString();
        //inputText = splitCount.ToString();
        //textCount = splitCount.ToString();
        //return textCount;
    }

    protected virtual void ClickOKButton()
    {
        GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).gameObject.SetActive(true);  //tempSlot�� ��Ȱ��ȭ ���״� �θ������Ʈ�� ���� ã�Ƽ� Ȱ��ȭ ��ų���̴�.
        
        //���� �������µ� ������ Ȱ��ȭ�� ���״µ�, awake�� ����Ǳ� ���� �Ʒ��Լ��� ���� ����Ǵµ� �ش� �Լ����� Awake���� getcomponent�ؾ��ϴ� ������ �����;ߵż� �������� ������ ����
        splitTempSlotSplitUI.SetTempSlotWithData(splitItemData, (uint)splitCount);       //���� ������ tempslot�� �����ϰ�


        isSplitting = true;

        inventory.itemSlots[takeID].DecreaseSlotItem((uint)splitCount);             //UI�� ���� �����Ϳ����� ��
        inventoryUI.slotUIs[takeID].slotUICount -= (uint)splitCount; ;

        inventoryUI.SetAllSlotWithData();

        SplitUIClose();
    }

    public virtual void ClickCancelButton()
    {
        SplitUIClose();
    }
}
