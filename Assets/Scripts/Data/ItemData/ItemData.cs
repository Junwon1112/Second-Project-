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
[System.Serializable]
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
    /// 깊은 복사하는 static함수, 객체를 바이너리화 시키고 다시 객체로 돌리면서(직렬화 후 다시 역직렬화) 복사가 되는 거 같은데 정확한 원리는 모르겟음
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
