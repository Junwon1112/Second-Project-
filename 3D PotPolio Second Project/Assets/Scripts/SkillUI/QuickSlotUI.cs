using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlotUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int quickSlotID = -1;    //퀵슬롯 아이디는 2000번 부터 시작
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

    public void QuickSlotSetData(SkillData skillData = null)    //파라미터 따로 기입안하면 skillData 파라미터는 null 값이 된다.
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

        if (otherQuickSlotUI != null && otherQuickSlotUI != this)     //퀵슬롯 안찍었으면 QuickSlotUI컴포넌트가 어차피 없을꺼니까 퀵슬롯을 찍었다면 이라는 뜻
        {
            if(otherQuickSlotUI.quickSlotSkillData == null)     //이동할 슬롯이 비어있다면
            {
                otherQuickSlotUI.QuickSlotSetData(tempSlotSkillUI.tempSkillData);   //이동할 퀵슬롯을 채우고
                QuickSlotSetData();     //현재 슬롯을 비운다.
            }
            else    //이동할 슬롯이 다른 스킬로 되어있다면
            {
                SkillData tempSkillData = new SkillData();  //저장용 임시 데이터

                tempSkillData = otherQuickSlotUI.quickSlotSkillData;    //임시 스킬데이터에 덮어씌울 스킬데이터 저장하고

                otherQuickSlotUI.QuickSlotSetData(tempSlotSkillUI.tempSkillData);   //이동할 퀵슬롯을 현재 슬롯 데이터로 바꾸고
                
                QuickSlotSetData(tempSkillData);     //현재 슬롯을 임시 데이터로 바꾼다.
            }
        }

        tempSlotSkillUI.transform.gameObject.SetActive(false);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
    }





}
