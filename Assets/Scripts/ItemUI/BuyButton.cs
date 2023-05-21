using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    //1. 이 슬롯의 아이템 가격 체크
    //2. 플레이어의 돈 체크
    //3. 플레이어 인벤토리 내 빈자리 체크
    //4. 모두 체크 후 플레이어 인벤에서 돈을 감소시키고, 아이템을 추가

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
