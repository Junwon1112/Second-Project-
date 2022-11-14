using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TempSlotInfoUI : MonoBehaviour
{
    //�⺻������ Infoâ ������ �̹����� �̿��ϸ�
    //����� ���� ���ο� Ŭ������ ����� Splitâ�� �̿��� ����

    public Image itemImage;                //Image�� ������Ƽ�� ��������Ʈ�� �����Ѵ�. 

    // ������ ������ �� ���
    public ItemData tempSlotItemData;   //tempSlot�� �߻���Ų������ �޾ƿ´�.
    public uint tempSlotItemCount;      //tempSlot�� �߻���Ų������ �޾ƿ´�.

    

    private void Awake()
    {
        itemImage = GetComponentInChildren<Image>();
    }



    public void SetTempSlotWithData(ItemData itemData, uint count)
    {
        itemImage.sprite = itemData.itemIcon;   //���⼭ �ι��� ���ø��Ҷ� ������(�Ƹ� ��ӹ޾Ƽ� split�ʿ��� ok������ �������°� ����)
        tempSlotItemData = itemData;
        tempSlotItemCount = count;
    }

}
