using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// ��� ���Կ� �� Ŭ����, TempSlotInfoUI�� ��ӹ���, �̰͵� SlotŬ������ ���� ����� ����� �߾�� �ߴ�
/// </summary>
public class EquipSlotUI : ItemSlotUI_Basic
{
    private TextMeshProUGUI takeSlotItemCountText;
    /// <summary>
    /// ���â ���� ���� ���̵� = 1001 ��
    /// </summary>
    public int equipSlotID = 1001;

    Image itemImage;
    ItemData itemData;
    uint slotUICount;

    public override Image ItemImage { get => itemImage; set => itemImage = value; }
    public override ItemData ItemData { get => itemData; set => itemData = value; }
    public override uint SlotUICount { get => slotUICount; set => slotUICount = value; }

    void Awake()
    {
        ItemImage = GetComponentInChildren<Image>();
        takeSlotItemCountText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ClearTempSlot();
    }

    /// <summary>
    /// �ش� ������ ������ ���� �Լ�
    /// </summary>
    public void ClearTempSlot()
    {
        ItemImage.color = Color.clear;
        takeSlotItemCountText.alpha = 0;
        ItemData = null;
        SlotUICount = 0;
    }

    /// <summary>
    /// �ش� ������ �Է¹��� ���������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="itemData">� ���������� �����ͷ� ����</param>
    /// <param name="count">��� ������ ����</param>
    public void SetTempSlotWithData(ItemData itemData, uint count)
    {
        ItemImage.sprite = itemData.itemIcon;   
        ItemImage.color = Color.white;

        ItemData = itemData;
        SlotUICount = count;

        takeSlotItemCountText.text = SlotUICount.ToString();
        takeSlotItemCountText.alpha = 1;
    }
}
