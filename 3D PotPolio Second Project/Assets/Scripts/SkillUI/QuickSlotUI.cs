using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlotUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int quickSlotID = -1;    //������ ���̵�� 2000�� ���� ����
    public SkillData quickSlotSkillData;
    Image skillImage;
    TempSlotSkillUI tempSlotSkillUI;
    public SkillUse skillUse;


    private void Awake()
    {
        skillImage = GetComponent<Image>();
        tempSlotSkillUI = FindObjectOfType<TempSlotSkillUI>();
        skillUse = GetComponentInChildren<SkillUse>();

    }

    private void Start()
    {
        skillImage.color = Color.clear;
        //skillUse = new SkillUse();
    }


    //public void SkillUseInitiate()
    //{
    //    skillUse = new SkillUse();
    //}

    public void QuickSlotSetData(SkillData skillData = null)    //�Ķ���� ���� ���Ծ��ϸ� skillData �Ķ���ʹ� null ���� �ȴ�.
    {
        if(skillData != null)
        {
            quickSlotSkillData = skillData;
            skillImage.sprite = skillData.skillIcon;

            skillImage.color = Color.white;
        }
        else
        {
            quickSlotSkillData = null;
            skillImage.sprite = null;

            skillImage.color = Color.clear;
        }
        
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if(quickSlotSkillData != null)
        {
            GameObject.Find("SkillMoveSlotUI").transform.GetChild(0).gameObject.SetActive(true);
            tempSlotSkillUI.SetTempSkillSlotUIData(quickSlotSkillData);
        } 
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        QuickSlotUI otherQuickSlotUI = obj.GetComponent<QuickSlotUI>();

        if (otherQuickSlotUI != null && otherQuickSlotUI != this)     //������ ��������� QuickSlotUI������Ʈ�� ������ �������ϱ� �������� ����ٸ� �̶�� ��
        {
            if(otherQuickSlotUI.quickSlotSkillData == null)     //�̵��� ������ ����ִٸ�
            {
                otherQuickSlotUI.QuickSlotSetData(tempSlotSkillUI.tempSkillData);   //�̵��� �������� ä���
                QuickSlotSetData();     //���� ������ ����.
            }
            else    //�̵��� ������ �ٸ� ��ų�� �Ǿ��ִٸ�
            {
                SkillData tempSkillData = new SkillData();  //����� �ӽ� ������

                tempSkillData = otherQuickSlotUI.quickSlotSkillData;    //�ӽ� ��ų�����Ϳ� ����� ��ų������ �����ϰ�

                otherQuickSlotUI.QuickSlotSetData(tempSlotSkillUI.tempSkillData);   //�̵��� �������� ���� ���� �����ͷ� �ٲٰ�
                
                QuickSlotSetData(tempSkillData);     //���� ������ �ӽ� �����ͷ� �ٲ۴�.
            }
        }

        tempSlotSkillUI.transform.gameObject.SetActive(false);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
    }





}
