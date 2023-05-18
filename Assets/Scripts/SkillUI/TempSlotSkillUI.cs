using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// 드래그 시 사용될 임시 스킬슬롯
/// </summary>
public class TempSlotSkillUI : SkillSlotUI_Basic
{
    Image skillImage;
    SkillData skillData;
    public RectTransform rectTransform;

    public override Image SkillImage { get => skillImage; set => skillImage = value; }
    public override SkillData SkillData { get => skillData; set => skillData = value; }

    private void Awake()
    {
        SkillImage = GetComponent<Image>();
        rectTransform = transform.parent.GetComponent<RectTransform>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);   //canvasGroup으로 OnOff하면 안보이는 동안 update에서 Image가 마우스를 계속 따라다니므로 활성화를 끈다. 
    }

    private void Update()
    {
        transform.position = (Vector3)Mouse.current.position.ReadValue();
    }

    public void SetTempSkillSlotUIData(SkillData skillData = null)
    {
        if(skillData != null)
        {
            SkillData = skillData;
            SkillImage.sprite = this.skillData.skillIcon;
        }
        else
        {
            SkillData = null;
            SkillImage.sprite = null;
        }
    }
}
