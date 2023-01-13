using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillSlotUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //��ų ���� UI���� �����ؾ� �Ұ� 
    //1. �巡���ؼ� ���������� �ű� �� �־����, ��ų ��� �䱸�������� ������ ���� �Ҵ�� ��ų ����Ʈ�� �־�� �巡�� �����ϰ� ����� ����
    //2. ��Ŭ���̳� ���� Ŭ���ؼ� ��ų �ִϸ��̼� �ߵ�?

    int skillSlotUIid = -1;     //��ų ���� �� id, skillUI Ŭ�������� �Ҵ��� ����, Ȥ�� �Ҵ� ���� ���ϸ� -1��
    public SkillData skillData;        //SkillUI�� ����Ʈ�� �迭�� ��ų ��ũ���ͺ� ������Ʈ �ް� ����(skillslotUI)�� �Ҵ�
    Image skillIcon;
    TextMeshProUGUI skillInfo;
    TempSlotSkillUI tempSlotSkillUI;

    private void Awake()
    {
        skillIcon = GetComponent<Image>();
        skillInfo = transform.parent.GetComponentInChildren<TextMeshProUGUI>();
        tempSlotSkillUI = GameObject.FindObjectOfType<TempSlotSkillUI>();
    }

    public void SetSkillUIInfo()    
    {
        if(skillData != null)
        {
            skillIcon.sprite = skillData.skillIcon;
            skillInfo.text = skillData.skillInformation;
        }
        else
        {
            skillIcon.color = Color.clear;
            skillInfo.text = "No Assigned Skill";
        }
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        GameObject.Find("SkillMoveSlotUI").transform.GetChild(0).gameObject.SetActive(true);
        tempSlotSkillUI.SetTempSkillSlotUIData(skillData);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        QuickSlotUI quickSlotUI = obj.GetComponent<QuickSlotUI>();

        if(quickSlotUI != null)     //������ ��������� QuickSlotUI������Ʈ�� ������ �������ϱ� �������� ����ٸ� �̶�� ��
        {
            quickSlotUI.QuickSlotSetData(tempSlotSkillUI.tempSkillData);   
        }



        tempSlotSkillUI.transform.gameObject.SetActive(false);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        
    }
}
