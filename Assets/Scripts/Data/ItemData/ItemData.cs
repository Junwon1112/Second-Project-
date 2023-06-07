using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

    /// <summary>
    /// ���� �����ϴ� static�Լ�, �ν��Ͻ����� ȣ������ �ʰ� Ŭ�����̸����� ȣ���ϴ� �� �����ϸ� ����� ū ������ ���ص� �� ���ϴ�.
    /// </summary>
    /// <typeparam name="ItemData"></typeparam>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public static ItemData DeepCopy(ItemData itemData)
    {
        if(itemData != null)
        {
            if(itemData.ID < 10)
            {
                ItemData_Potion itemData_new = new ItemData_Potion();
                itemData_new.itemName = itemData.itemName;
                itemData_new.ID = itemData.ID;
                itemData_new.itemIDCode = itemData.itemIDCode;
                itemData_new.itemPrefab = itemData.itemPrefab;
                itemData_new.itemIcon = itemData.itemIcon;
                itemData_new.itemValue = itemData.itemValue;
                itemData_new.itemMaxCount = itemData.itemMaxCount;
                itemData_new.itemType = itemData.itemType;
                itemData_new.job = itemData.job;
                itemData_new.ItemInfo = itemData.ItemInfo;

                return itemData_new;
            }
            else if(itemData.ID < 100)
            {
                ItemData_Weapon itemData_new = new ItemData_Weapon();
                itemData_new.itemName = itemData.itemName;
                itemData_new.ID = itemData.ID;
                itemData_new.itemIDCode = itemData.itemIDCode;
                itemData_new.itemPrefab = itemData.itemPrefab;
                itemData_new.itemIcon = itemData.itemIcon;
                itemData_new.itemValue = itemData.itemValue;
                itemData_new.itemMaxCount = itemData.itemMaxCount;
                itemData_new.itemType = itemData.itemType;
                itemData_new.job = itemData.job;
                itemData_new.ItemInfo = itemData.ItemInfo;

                return itemData_new;
            }
            else if(itemData.ID == 100)
            {
                ItemData_SpecialPotion_Scale itemData_new = new ItemData_SpecialPotion_Scale();
                itemData_new.itemName = itemData.itemName;
                itemData_new.ID = itemData.ID;
                itemData_new.itemIDCode = itemData.itemIDCode;
                itemData_new.itemPrefab = itemData.itemPrefab;
                itemData_new.itemIcon = itemData.itemIcon;
                itemData_new.itemValue = itemData.itemValue;
                itemData_new.itemMaxCount = itemData.itemMaxCount;
                itemData_new.itemType = itemData.itemType;
                itemData_new.job = itemData.job;
                itemData_new.ItemInfo = itemData.ItemInfo;

                return itemData_new;
            }
        }
        return null;
    }
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

    PurpleMushroom = 100,
}

public enum ItemType
{
    ComsumableItem = 0,
    Weapon
}
