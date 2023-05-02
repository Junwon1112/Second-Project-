using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Infoâ ������ �̹����� �̿�
/// </summary>
public class TempSlotInfoUI : MonoBehaviour
{
    public Image itemImage;                //Image�� ������Ƽ�� ��������Ʈ�� �����Ѵ�. 

    // ������ ������ �� ���
    public ItemData takeSlotItemData;   //tempSlot�� �߻���Ų������ �޾ƿ´�.
    public uint takeSlotItemCount;      //tempSlot�� �߻���Ų������ �޾ƿ´�.

    RectTransform rectTransform_TempSlotInfo;

    void Awake()
    {
        itemImage = GetComponentInChildren<Image>();
        rectTransform_TempSlotInfo = GetComponent<RectTransform>();
    }

}
