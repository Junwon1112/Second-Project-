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

    

    void Awake()
    {
        itemImage = GetComponentInChildren<Image>();
    }

}
