using System.Collections;
using System.Collections.Generic;
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
}

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
