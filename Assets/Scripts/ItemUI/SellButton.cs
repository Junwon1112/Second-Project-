using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour
{
    //1. 이 슬롯의 아이템 가격 체크
    //2. 플레이어의 돈 체크
    //3. 플레이어 인벤토리 내 빈자리 체크
    //4. 모두 체크 후 플레이어 인벤에서 돈을 감소시키고, 아이템을 추가
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
