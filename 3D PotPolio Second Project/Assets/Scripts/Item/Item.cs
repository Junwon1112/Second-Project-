using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Item �ϳ� ���� (��ü �ϳ�)
public class Item : MonoBehaviour
{
    ItemData data;  //�� �������� ���� ������ ������

    private void Start()
    {
        //������ ����. Awake�� ���� data�� ��� Start���� ����
        Instantiate(data.itemPrefab, transform);
    }
}
