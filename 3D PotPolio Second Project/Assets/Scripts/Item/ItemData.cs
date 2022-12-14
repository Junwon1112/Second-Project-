using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �����͸� �����ϴ� ������ ������ ����� �ִ� ��ũ��Ʈ
[CreateAssetMenu(fileName = "New Item data" , menuName = "Scriptable Object/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    //Scriptable ������Ʈ�� ���� ��

    //ItemData�� �� ����
    public string itemName;        //������ �̸�
    public uint ID;                 //������ ID
    // public int itemType;           //������ ���� - �Ҹ�ǰ���� ������������ => ������� �������̽��� ������
    public GameObject itemPrefab;  //������ ������
    public Sprite itemIcon;        //������ ������
    public int itemValue;          //������ ��ġ
    public int itemMaxCount;       //������ �ִ� ������
}

public enum ItemIDCode
{
    HP_Potion = 0,
    Basic_Weapon_1,
    Basic_Weapon_2
}
