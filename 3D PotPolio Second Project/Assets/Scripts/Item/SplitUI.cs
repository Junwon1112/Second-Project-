using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SplitUI : MonoBehaviour
{
    private Button okButton;
    private Button cancelButton;
    private TMP_InputField inputField;
    private CanvasGroup splitUICanvasGroup;

    public ItemData splitItemData;      //ItemSlotUI에서 받아옴
    public uint splitPossibleCount = 0; //스플릿 하기 직전에 아이템슬롯UI에서 데이터값을 그대로 할당해 줌
    private int splitCount = 0; //checkRightcount에서 최종적으로 할당해줌

    public TempSlotSplitUI splitTempSlotSplitUI;
    public int takeID = -1; //아이템 슬롯과 UI의 ID를 받아올 값


    Inventory inventory;
    InventoryUI inventoryUI;


    private void Awake()
    {
        okButton = transform.Find("OKButton").GetComponent<Button>();
        cancelButton = transform.Find("CancelButton").GetComponent<Button>();
        inputField = FindObjectOfType<TMP_InputField>();
        splitUICanvasGroup = GetComponent<CanvasGroup>();
        inventory = FindObjectOfType<Inventory>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        splitTempSlotSplitUI = GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).GetComponent<TempSlotSplitUI>();   //활성화후 컴포넌트 찾은거 변쉐 저장하고
    }

    private void Start()
    {
        inputField.onEndEdit.AddListener(CheckRightCount); //스트링타입 리턴받는 함수 실행  => 입력된 숫자가 슬롯의 itemCount보다 크면 itemCount를, 작으면 0을 리턴
        
        okButton.onClick.AddListener(ClickOKButton);
        //cancelButton.onClick.AddListener();

    }

    public void SplitUIOpen()
    {
        splitUICanvasGroup.alpha = 1.0f;
        splitUICanvasGroup.interactable = true;
        splitUICanvasGroup.blocksRaycasts = true;

        
    }

    public void SplitUIClose()
    {
        splitUICanvasGroup.alpha = 0.0f;
        splitUICanvasGroup.interactable = false;
        splitUICanvasGroup.blocksRaycasts = false;
    }

    public void CheckRightCount(string inputText) //해당 내용을 프로퍼티로 설정하면 되는 문제였다..
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

    public void ClickOKButton()
    {
        GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).gameObject.SetActive(true);  //tempSlot을 비활성화 시켰다 부모오브젝트를 통해 찾아서 활성화 시킬것이다.
        
        splitTempSlotSplitUI.SetTempSlotWithData(splitItemData, (uint)splitCount);       //나눌 데이터 tempslot에 전달하고
        splitTempSlotSplitUI.isSpliting = true;

        inventory.itemSlots[takeID].DecreaseSlotItem((uint)splitCount);             //UI와 슬롯 데이터에서는 뺌
        inventoryUI.slotUIs[takeID].slotUICount -= (uint)splitCount; ;

        inventoryUI.SetAllSlotWithData();

        SplitUIClose();
    }

    public void ClickCancelButton()
    {
        SplitUIClose();
    }
}
