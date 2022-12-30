using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EquipSlotUI : TempSlotInfoUI
{
    //public bool isSpliting = false;     //SplitUI���� OK��ư ������ true�� �ٲ���
    private TextMeshProUGUI takeSlotItemCountText;
    int takeID = -1;
    public int equipSlotID = 1001;     //���â ���� ���� ���̵� = 1001 ��

    void Awake()
    {
        this.itemImage = GetComponentInChildren<Image>();
        takeSlotItemCountText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ClearTempSlot();
    }

    public void ClearTempSlot()
    {
        itemImage.color = Color.clear;
        takeSlotItemCountText.alpha = 0;
        //isSpliting = false;   //splitUI���� ó��
        takeSlotItemData = null;
        takeSlotItemCount = 0;
    }

    public void SetTempSlotWithData(ItemData itemData, uint count)
    {
        itemImage.sprite = itemData.itemIcon;   //���⼭ �ι��� ���ø��Ҷ� ������(�Ƹ� ��ӹ޾Ƽ� split�ʿ��� ok������ �������°� ����)
        itemImage.color = Color.white;

        takeSlotItemData = itemData;
        takeSlotItemCount = count;

        takeSlotItemCountText.text = takeSlotItemCount.ToString();
        takeSlotItemCountText.alpha = 1;
    }

    //--------��ӹ��� TempSlotInfoUI�� ����--------------------------------

    //==================================================================================================================
    //public Image itemImage;                //Image�� ������Ƽ�� ��������Ʈ�� �����Ѵ�. 

    //// ������ ������ �� ���
    //public ItemData tempSlotItemData;   //tempSlot�� �߻���Ų������ �޾ƿ´�.
    //public uint tempSlotItemCount;      //tempSlot�� �߻���Ų������ �޾ƿ´�.

    //private void Awake()
    //{
    //    itemImage = GetComponentInChildren<Image>();
    //}

    //public void SetTempSlotWithData(ItemData itemData, uint count)
    //{
    //    itemImage.sprite = itemData.itemIcon;
    //    tempSlotItemData = itemData;
    //    tempSlotItemCount = count;
    //}
    //==================================================================================================================
}
