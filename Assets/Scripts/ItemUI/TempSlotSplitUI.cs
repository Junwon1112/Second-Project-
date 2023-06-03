using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// 아이템 슬롯에서 아이템을 나눌 시 임시로 보여질 슬롯
/// </summary>
public class TempSlotSplitUI : ItemSlotUI_Basic
{
    //public bool isSpliting = false;     //SplitUI에서 OK버튼 누르면 true로 바꿔줌
    private TextMeshProUGUI tempSlotItemCountText;
    int takeID = -1;
    public RectTransform rectTransform_TempSlotSplit;

    Image itemImage;
    ItemData itemData;
    uint slotUICount;

    public override Image ItemImage { get => itemImage; set => itemImage = value; }
    public override ItemData ItemData { get => itemData; set => itemData = value; }
    public override uint SlotUICount { get => slotUICount; set => slotUICount = value; }

    void Awake()
    {
        ItemImage = GetComponentInChildren<Image>();
        tempSlotItemCountText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        rectTransform_TempSlotSplit = transform.parent.GetComponent<RectTransform>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 분할 중이라면 update에서 슬롯이 마우스 위치르 따라다님
    /// </summary>
    private void Update()
    {
        //분할 중이라면 실행하기
        transform.position = (Vector3)Mouse.current.position.ReadValue();
    }

    /// <summary>
    /// 임시슬롯 비우는 메서드
    /// </summary>
    public void ClearTempSlot()
    {
        ItemImage.sprite = null;
        //isSpliting = false;   //splitUI에서 처리
        ItemData = null;
        SlotUICount = 0;
    }

    /// <summary>
    /// 임시 슬롯 데이터를 세팅하는 메서드
    /// </summary>
    /// <param name="itemData"></param>
    /// <param name="count"></param>
    public void SetTempSlotWithData(ItemData itemData, uint count)
    {
        ItemData = itemData;
        //ItemData = ItemData.DeepCopy(itemData);
        ItemImage.sprite = ItemData.itemIcon;   //여기서 두번쨰 스플릿할때 에러남(아마 상속받아서 split쪽에서 ok누른뒤 에러나는거 같음)
        SlotUICount = count;
        tempSlotItemCountText.text = SlotUICount.ToString();
    }
}
