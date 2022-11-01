using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템 데이터를 저장하는 데이터 파일을 만들어 주는 스크립트
[CreateAssetMenu(fileName = "New Item data" , menuName = "Scripatable Object/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    //Scriptable 오브젝트로 만들 것

    //ItemData에 들어갈 내용
    public string itemName;        //아이템 이름
    public uint ID;                 //아이템 ID
    // public int itemType;           //아이템 종류 - 소모품인지 장비아이템인지 => 강사님은 인터페이스로 구현함
    public GameObject itemPrefab;  //아이템 프리팹
    public Sprite itemIcon;        //아이템 아이콘
    public int itemValue;          //아이템 가치
    public int itemMaxCount;       //아이템 최대 누적수
}

public enum ItemIDCode
{
    HP_Potion = 0,
    Weapon
}
