using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// ItemInfo창의 동작을 관리하는 클래스
/// </summary>
public class SkillInfoUI : InfoUI_Parent
{
    public bool isInfoOpen = false;

    /// <summary>
    /// 아이템 이름
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
    /// UI가 열렸을 때 실행될 메서드
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

    public override void SetInfo(SkillData skillData, Vector3 infoPos)
    {
        InfoImage.sprite = skillData.skillIcon;
        InfoTransform.position = infoPos;
        OpenInfo();
        InfoName.text = skillData.skillName;
        ItemInformation.text = skillData.skillInformation;
        isInfoOpen = true;
    }
}
