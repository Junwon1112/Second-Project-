using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    //1. �� ������ ������ ���� üũ
    //2. �÷��̾��� �� üũ
    //3. �÷��̾� �κ��丮 �� ���ڸ� üũ
    //4. ��� üũ �� �÷��̾� �κ����� ���� ���ҽ�Ű��, �������� �߰�

    Button button_Buy;

    private void Awake()
    {
        button_Buy = GetComponent<Button>();
    }

    private void Start()
    {
        button_Buy.onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {

    }
}
