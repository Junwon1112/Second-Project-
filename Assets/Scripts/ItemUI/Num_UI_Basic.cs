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
    /// ItemSlotUI���� �޾ƿ�
    /// </summary>
    public abstract ItemData ItemData { get; set; }

    /// <summary>
    /// ������ ���԰� UI�� ID�� �޾ƿ� ��
    /// </summary>
    public abstract int TakeID { get; set; }

    protected abstract Inventory Inventory { get; set; }
    protected abstract InventoryUI InventoryUI { get; set; }

    public abstract RectTransform RectTransform { get; set; }


    /// <summary>
    /// UI�� ������ �� ���̰� ����� �޼���
    /// </summary>
    public abstract void NumUIOpen();


    /// <summary>
    /// UI�� ������ �� ���̰� ����� �޼���
    /// </summary>
    public abstract void NumUIClose();

    /// <summary>
    /// �ؽ�Ʈ�� ���� ���� �Է� �� ����,�Էµ� ���ڰ� ������ itemCount���� ũ�� itemCount��, ������ 0�� ����
    /// </summary>
    /// <param name="inputText"></param>
    protected abstract void CheckRightCount(string inputText);


    /// <summary>
    /// ok��ư ���� �� ���� �޼���, �������� ������ �۾� ����
    /// </summary>
    protected abstract void ClickOKButton();

    public abstract void ClickCancelButton();

}
