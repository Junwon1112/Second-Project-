using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// �巡�� �� ���� �ӽ� ��ų����
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
        this.gameObject.SetActive(false);   //canvasGroup���� OnOff�ϸ� �Ⱥ��̴� ���� update���� Image�� ���콺�� ��� ����ٴϹǷ� Ȱ��ȭ�� ����. 
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
