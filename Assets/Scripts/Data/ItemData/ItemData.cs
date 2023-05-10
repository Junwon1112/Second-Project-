using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �����͸� �����ϴ� ������ ������ ����� �ִ� ��ũ���ͺ� ������Ʈ, ���� �޴��� ������ ������ ���� �߰�
/// </summary>
[CreateAssetMenu(fileName = "New Item data" , menuName = "Scriptable Object_Item Data/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;        //������ �̸�
    public uint ID;                 //������ ID
    public ItemIDCode itemIDCode;              //������ ID�ڵ�
    public GameObject itemPrefab;  //������ ������
    public Sprite itemIcon;        //������ ������
    public int itemValue;          //������ ��ġ
    public int itemMaxCount;       //������ �ִ� ������
    public ItemType itemType;       //������ Ÿ��
    public JobType job;             //������ ��밡�� ����
    public string ItemInfo;         //������ ����
}


/// <summary>
/// 0~ 10�� �Һ���, 10~�� ����
/// </summary>
public enum ItemIDCode
{
    HP_Potion = 0,
    Basic_Weapon_1 = 10,
    Basic_Weapon_2 = 11,
    Basic_Wand1 = 12,
}

public enum ItemType
{
    ComsumableItem = 0,
    Weapon
}
