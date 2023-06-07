using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// 아이템 데이터를 저장하는 데이터 파일을 만들어 주는 스크립터블 오브젝트, 에셋 메뉴에 아이템 데이터 생성 추가
/// </summary>
[CreateAssetMenu(fileName = "New Item data" , menuName = "Scriptable Object_Item Data/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;        //아이템 이름
    public uint ID;                 //아이템 ID
    public ItemIDCode itemIDCode;              //아이템 ID코드
    public GameObject itemPrefab;  //아이템 프리팹
    public Sprite itemIcon;        //아이템 아이콘
    public int itemValue;          //아이템 가치
    public int itemMaxCount;       //아이템 최대 누적수
    public ItemType itemType;       //아이템 타입
    public JobType job;             //아이템 사용가능 직업
    public string ItemInfo;         //아이템 설명

    /// <summary>
    /// 깊은 복사하는 static함수, 인스턴스에서 호출하지 않고 클래스이름으로 호출하는 걸 생각하면 상속은 큰 걱정을 안해도 될 듯하다.
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
/// 0~ 10은 소비형, 10~는 무기
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
