using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour
{
    //1. �� ������ ������ ���� üũ
    //2. �÷��̾��� �� üũ
    //3. �÷��̾� �κ��丮 �� ���ڸ� üũ
    //4. ��� üũ �� �÷��̾� �κ����� ���� ���ҽ�Ű��, �������� �߰�
    StoreSlotUI_Sell parentSlot;

    Button button_Sell;
    SellUI sellUI;

    private void Awake()
    {
        parentSlot = transform.parent.GetChild(1).GetComponent<StoreSlotUI_Sell>();
        button_Sell = GetComponent<Button>();
        sellUI = FindObjectOfType<SellUI>();
    }

    private void Start()
    {
        button_Sell.onClick.AddListener(SellItem);
    }

    private void SellItem()
    {
        sellUI.ItemData = parentSlot.ItemData;
        sellUI.ItemCount = parentSlot.Count;
        sellUI.NumUIOpen();
    }
}
