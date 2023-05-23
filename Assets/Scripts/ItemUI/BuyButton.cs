using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    //1. �� ������ ������ ���� üũ
    //2. �÷��̾��� �� üũ
    //3. �÷��̾� �κ��丮 �� ���ڸ� üũ
    //4. ��� üũ �� �÷��̾� �κ����� ���� ���ҽ�Ű��, �������� �߰�
    StoreSlotUI_Buy parentSlot;

    Button button_Buy;
    BuyUI buyUI;

    private void Awake()
    {
        parentSlot = transform.parent.GetChild(1).GetComponent<StoreSlotUI_Buy>();
        button_Buy = GetComponent<Button>();
        buyUI = FindObjectOfType<BuyUI>();
    }

    private void Start()
    {
        button_Buy.onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {
        buyUI.ItemData = parentSlot.ItemData;
        buyUI.NumUIOpen();
    }
}
