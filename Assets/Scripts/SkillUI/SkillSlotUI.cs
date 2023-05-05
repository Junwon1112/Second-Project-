using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// ��ų ���� UI�� ���� Ŭ����, �ַ� ��ų �������� ���� �� �̵��� ���� �ٷ�
/// </summary>
public class SkillSlotUI : SkillSlotUI_Basic, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    //��ų ���� UI���� �����ؾ� �Ұ� 
    //1. �巡���ؼ� ���������� �ű� �� �־����, ��ų ��� �䱸�������� ������ ���� �Ҵ�� ��ų ����Ʈ�� �־�� �巡�� �����ϰ� ����� ����
    //2. ��Ŭ���̳� ���� Ŭ���ؼ� ��ų �ִϸ��̼� �ߵ�?

    /// <summary>
    /// ��ų ���� �� id, skillUI Ŭ�������� �Ҵ��� ����, Ȥ�� �Ҵ� ���� ���ϸ� -1��
    /// </summary>
    int skillSlotUIid = -1;

    /// <summary>
    /// SkillUI�� ����Ʈ�� �迭�� ��ų ��ũ���ͺ� ������Ʈ �ް� ����(skillslotUI)�� �Ҵ�
    /// </summary>
    SkillData skillData;       
    Image skillImage;
    TextMeshProUGUI skillInfo;
    TextMeshProUGUI skillName;
    TempSlotSkillUI tempSlotSkillUI;

    uint currentSkillLevel;

    public UpDownButton upDownButton;

    public RectTransform rectTransform;

    public override Image SkillImage { get => skillImage; set => skillImage = value; }
    public override SkillData SkillData { get => skillData; set => skillData = value; }

    private void Awake()
    {
        SkillImage = GetComponent<Image>();
        skillInfo = transform.parent.Find("Info_Text").GetComponent<TextMeshProUGUI>();
        skillName = transform.parent.Find("SkillName_Text").GetComponent<TextMeshProUGUI>();
        tempSlotSkillUI = GameObject.FindObjectOfType<TempSlotSkillUI>();
        upDownButton = transform.parent.GetComponentInChildren<UpDownButton>();
    }


    /// <summary>
    /// ��ų ������ ���� �����ð� �÷��ѽ� Info�� ������ �޼���
    /// </summary>
    public void SetSkillUIInfo()    
    {
        if(SkillData != null)
        {
            SkillImage.sprite = SkillData.skillIcon;
            skillInfo.text = SkillData.skillInformation;
            skillName.text = SkillData.skillName;
        }
        else
        {
            SkillImage.color = Color.clear;
            skillInfo.text = "No Assigned Skill";
            skillName.text = "No Assigned Skill";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }


    /// <summary>
    /// �巡�� ���۽� ����� �޼���, �ӽ� ���� ����
    /// </summary>
    /// <param name="eventData"></param>
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if(SkillData.skillLevel > 0)
        {
            GameObject.Find("SkillMoveSlotUI").transform.GetChild(0).gameObject.SetActive(true);
            tempSlotSkillUI.SetTempSkillSlotUIData(SkillData);
            tempSlotSkillUI.rectTransform.SetAsLastSibling();
        }
    }

    /// <summary>
    /// �巡�� �Ϸ�� ����� �޼���, ���������� �θ� �ش� �����Կ� ��ų ���
    /// </summary>
    /// <param name="eventData"></param>
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        QuickSlotUI quickSlotUI = obj.GetComponent<QuickSlotUI>();

        if(quickSlotUI != null)     //������ ��������� QuickSlotUI������Ʈ�� ������ �������ϱ� �������� ����ٸ� �̶�� ��
        {
            quickSlotUI.QuickSlotSetData(tempSlotSkillUI.SkillData);   
        }


        tempSlotSkillUI.SetTempSkillSlotUIData();
        tempSlotSkillUI.transform.gameObject.SetActive(false);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        
    }

}
