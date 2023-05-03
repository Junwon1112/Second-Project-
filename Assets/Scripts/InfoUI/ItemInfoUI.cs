using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// ItemInfoâ�� ������ �����ϴ� Ŭ����
/// </summary>
public class ItemInfoUI : InfoUI_Parent
{
    public bool isInfoOpen = false;

    /// <summary>
    /// ������ �̸�
    /// </summary>
    public override TextMeshProUGUI InfoName { get; set; }
    public override TextMeshProUGUI ItemInformation { get; set; }
    public override CanvasGroup InfoCanvasGroup { get; set; }
    public override RectTransform InfoTransform { get; set; }
    public override Image InfoImage { get; set; }

    private void Awake()
    {
        InfoName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        ItemInformation = transform.Find("Information").GetComponent<TextMeshProUGUI>();
        InfoCanvasGroup = GetComponent<CanvasGroup>();
        InfoTransform = GetComponent<RectTransform>();
        InfoImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    /// <summary>
    /// UI�� ������ �� ����� �޼���
    /// </summary>
    public void OpenInfo()
    {
        InfoCanvasGroup.alpha = 1.0f;
        InfoCanvasGroup.blocksRaycasts = true;
        InfoCanvasGroup.interactable = true;
    }

    public void CloseInfo()
    {
        InfoCanvasGroup.alpha = 0.0f;
        InfoCanvasGroup.blocksRaycasts = false;
        InfoCanvasGroup.interactable = false;
    }

    public override void SetInfo(ItemData itemData, Vector3 infoPos)
    {
        InfoImage.sprite = itemData.itemIcon;
        InfoTransform.position = infoPos;
        OpenInfo();
        InfoName.text = itemData.itemName;
        ItemInformation.text = itemData.ItemInfo;
        isInfoOpen = true;
    }
}
