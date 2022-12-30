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
    public ItemData takeSlotItemData;   //tempSlot�� �߻���Ų������ �޾ƿ´�.
    public uint takeSlotItemCount;      //tempSlot�� �߻���Ų������ �޾ƿ´�.

    

    void Awake()
    {
        itemImage = GetComponentInChildren<Image>();
    }

}
