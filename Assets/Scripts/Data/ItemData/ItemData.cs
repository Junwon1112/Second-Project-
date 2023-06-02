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
[System.Serializable]
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
    /// ���� �����ϴ� static�Լ�, ��ü�� ���̳ʸ�ȭ ��Ű�� �ٽ� ��ü�� �����鼭(����ȭ �� �ٽ� ������ȭ) ���簡 �Ǵ� �� ������ ��Ȯ�� ������ �𸣰���
    /// </summary>
    /// <typeparam name="ItemData"></typeparam>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public ItemData DeepCopy<ItemData>(ItemData itemData)
    {
        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, itemData);
            ms.Position = 0;

            return (ItemData)formatter.Deserialize(ms);
        }
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
