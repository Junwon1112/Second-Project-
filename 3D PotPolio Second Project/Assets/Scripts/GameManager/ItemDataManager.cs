using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    public ItemData[] itemDatas;
    public ItemData this[int i] //�ε���
    {
        get
        {
            return itemDatas[i];
        }
    }
    //�迭ó�� ���� ������Ƽ

    public ItemData this[ItemIDCode ID ]  //�ε���
    {
        get
        {
            return itemDatas[(int)ID];
        }
    }
    //�迭ó�� ���� ������Ƽ

    
}
