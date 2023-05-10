using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템과 관련된 데이터를 관리하는 클래스
/// </summary>
public class ItemDataManager : MonoBehaviour
{
    public ItemData_Potion[] itemDatas_Potion;
    public ItemData_Weapon[] itemDatas_Weapon;


    //-----------------배열에 등록하고 인덱서로 찾는 방식은 id와 인덱스를 같게 놓아야하고(id가10번이면 10번째까지 배열을 만들어야됨) itemdata자식 형태는 받을수가 없어서 일단 보류----------- 


    ///// <summary>
    ///// 인덱서, 프로퍼티를 배열처럼 사용, 프로퍼티 이름을 this로 해서 클래스이름으로 프로퍼티를 
    ///// 호출 후, 배열을 써서 해당 배열과 같은 인덱스를 쓰는 프로퍼티 값을 리턴해줌 
    ///// </summary>
    ///// <param name="i"></param>
    ///// <returns></returns>
    //public ItemData this[int i]
    //{
    //    get
    //    {
    //        return itemDatas_Potion[i];
    //    }
    //}
    ////배열처럼 쓰는 프로퍼티

    //public ItemData this[ItemIDCode ID]  //인덱서
    //{
    //    get
    //    {
    //        return itemDatas_Potion[(int)ID];
    //    }
    //}
    ////배열처럼 쓰는 프로퍼티

    public ItemData FindItem_ItemData(uint _skillID)
    {
        if(_skillID < 10)
        {
            for (int i = 0; i < itemDatas_Potion.Length; i++)
            {
                if (itemDatas_Potion[i].ID == _skillID)
                {
                    return itemDatas_Potion[i];
                }
            }
        }
        else if(_skillID < 20)
        {
            for (int i = 0; i < itemDatas_Weapon.Length; i++)
            {
                if (itemDatas_Weapon[i].ID == _skillID)
                {
                    return itemDatas_Weapon[i];
                }
            }
        }
        

        return null;
    }


    //----------------------------------------------------------------------
    /// <summary>
    /// ID로 원하는 아이템데이터 찾는 함수, 오버로딩
    /// </summary>
    /// <param name="_skillID"></param>
    /// <returns></returns>
    public ItemData_Potion FindItem_Potion(uint _skillID)
    {
        for (int i = 0; i < itemDatas_Potion.Length; i++)
        {
            if (itemDatas_Potion[i].ID == _skillID)
            {
                return itemDatas_Potion[i];
            }
        }

        return null;
    }

    /// <summary>
    /// itemIDCode(열거형)로 원하는 아이템데이터 찾는 함수, 오버로딩
    /// </summary>
    /// <param name="_itemIDCode"></param>
    /// <returns></returns>
    public ItemData_Potion FindItem_Potion(ItemIDCode _itemIDCode)
    {
        for (int i = 0; i < itemDatas_Potion.Length; i++)
        {
            if (itemDatas_Potion[i].itemIDCode == _itemIDCode)
            {
                return itemDatas_Potion[i];
            }
        }

        return null;
    }

    /// <summary>
    /// 이름으로 원하는 아이템데이터 찾는 함수, 오버로딩
    /// </summary>
    /// <param name="_itemName"></param>
    /// <returns></returns>
    public ItemData_Potion FindItem_Potion(string _itemName)
    {
        for (int i = 0; i < itemDatas_Potion.Length; i++)
        {
            if (itemDatas_Potion[i].itemName == _itemName)
            {
                return itemDatas_Potion[i];
            }
        }

        return null;
    }
    //-------------------------------------------------------무기-------------------------

    /// <summary>
    /// ID로 원하는 아이템데이터 찾는 함수, 오버로딩
    /// </summary>
    /// <param name="_skillID"></param>
    /// <returns></returns>
    public ItemData_Weapon FindItem_Weapon(uint _skillID)
    {
        for (int i = 0; i < itemDatas_Weapon.Length; i++)
        {
            if (itemDatas_Weapon[i].ID == _skillID)
            {
                return itemDatas_Weapon[i];
            }
        }

        return null;
    }

    /// <summary>
    /// itemIDCode(열거형)로 원하는 아이템데이터 찾는 함수, 오버로딩
    /// </summary>
    /// <param name="_itemIDCode"></param>
    /// <returns></returns>
    public ItemData_Weapon FindItem_Weapon(ItemIDCode _itemIDCode)
    {
        for (int i = 0; i < itemDatas_Weapon.Length; i++)
        {
            if (itemDatas_Weapon[i].itemIDCode == _itemIDCode)
            {
                return itemDatas_Weapon[i];
            }
        }

        return null;
    }

    /// <summary>
    /// 이름으로 원하는 아이템데이터 찾는 함수, 오버로딩
    /// </summary>
    /// <param name="_itemName"></param>
    /// <returns></returns>
    public ItemData_Weapon FindItem_Weapon(string _itemName)
    {
        for (int i = 0; i < itemDatas_Weapon.Length; i++)
        {
            if (itemDatas_Weapon[i].itemName == _itemName)
            {
                return itemDatas_Weapon[i];
            }
        }

        return null;
    }
}
