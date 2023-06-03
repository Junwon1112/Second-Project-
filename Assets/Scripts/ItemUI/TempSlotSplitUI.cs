using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// ������ ���Կ��� �������� ���� �� �ӽ÷� ������ ����
/// </summary>
public class TempSlotSplitUI : ItemSlotUI_Basic
{
    //public bool isSpliting = false;     //SplitUI���� OK��ư ������ true�� �ٲ���
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
    /// ���� ���̶�� update���� ������ ���콺 ��ġ�� ����ٴ�
    /// </summary>
    private void Update()
    {
        //���� ���̶�� �����ϱ�
        transform.position = (Vector3)Mouse.current.position.ReadValue();
    }

    /// <summary>
    /// �ӽý��� ���� �޼���
    /// </summary>
    public void ClearTempSlot()
    {
        ItemImage.sprite = null;
        //isSpliting = false;   //splitUI���� ó��
        ItemData = null;
        SlotUICount = 0;
    }

    /// <summary>
    /// �ӽ� ���� �����͸� �����ϴ� �޼���
    /// </summary>
    /// <param name="itemData"></param>
    /// <param name="count"></param>
    public void SetTempSlotWithData(ItemData itemData, uint count)
    {
        ItemData = itemData;
        //ItemData = ItemData.DeepCopy(itemData);
        ItemImage.sprite = ItemData.itemIcon;   //���⼭ �ι��� ���ø��Ҷ� ������(�Ƹ� ��ӹ޾Ƽ� split�ʿ��� ok������ �������°� ����)
        SlotUICount = count;
        tempSlotItemCountText.text = SlotUICount.ToString();
    }
}
