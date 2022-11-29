using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    public ItemData[] itemDatas;
    public ItemData this[int i] //인덱서
    {
        get
        {
            return itemDatas[i];
        }
    }
    //배열처럼 쓰는 프로퍼티

    public ItemData this[ItemIDCode ID ]  //인덱서
    {
        get
        {
            return itemDatas[(int)ID];
        }
    }
    //배열처럼 쓰는 프로퍼티

    
}
