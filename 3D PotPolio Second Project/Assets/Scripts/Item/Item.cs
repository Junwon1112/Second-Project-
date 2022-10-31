using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Item 하나 생성 (개체 하나)
public class Item : MonoBehaviour
{
    ItemData data;  //이 아이템이 가질 아이템 데이터

    private void Start()
    {
        //프리팹 생성. Awake일 때는 data가 없어서 Start에서 실행
        Instantiate(data.itemPrefab, transform);
    }
}
