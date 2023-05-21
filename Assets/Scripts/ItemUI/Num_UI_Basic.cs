using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Num_UI_Basic : MonoBehaviour
{
    protected abstract Button OkButton { get; set; }
    protected abstract Button CancelButton { get; set; }
    protected abstract TMP_InputField InputField { get; set; }
    protected abstract CanvasGroup NumUI_CanvasGroup { get; set; }

    /// <summary>
    /// ItemSlotUI에서 받아옴
    /// </summary>
    public abstract ItemData ItemData { get; set; }

    /// <summary>
    /// 아이템 슬롯과 UI의 ID를 받아올 값
    /// </summary>
    public abstract int TakeID { get; set; }

    protected abstract Inventory Inventory { get; set; }
    protected abstract InventoryUI InventoryUI { get; set; }

    public abstract RectTransform RectTransform { get; set; }


    /// <summary>
    /// UI를 열었을 때 보이게 만드는 메서드
    /// </summary>
    public abstract void NumUIOpen();


    /// <summary>
    /// UI를 열었을 때 보이게 만드는 메서드
    /// </summary>
    public abstract void NumUIClose();

    /// <summary>
    /// 텍스트에 나눌 갯수 입력 시 실행,입력된 숫자가 슬롯의 itemCount보다 크면 itemCount를, 작으면 0을 리턴
    /// </summary>
    /// <param name="inputText"></param>
    protected abstract void CheckRightCount(string inputText);


    /// <summary>
    /// ok버튼 누를 시 사용될 메서드, 아이템을 나누는 작업 실행
    /// </summary>
    protected abstract void ClickOKButton();

    public abstract void ClickCancelButton();

}
