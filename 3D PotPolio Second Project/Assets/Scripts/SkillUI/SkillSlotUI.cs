using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillSlotUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //스킬 슬롯 UI에서 구현해야 할것 
    //1. 드래그해서 퀵슬롯으로 옮길 수 있어야함, 스킬 사용 요구레벨보다 레벨이 높고 할당된 스킬 포인트가 있어야 드래그 가능하게 만들고 싶음
    //2. 우클릭이나 더블 클릭해서 스킬 애니메이션 발동?

    int skillSlotUIid = -1;     //스킬 슬롯 별 id, skillUI 클래스에서 할당할 예정, 혹시 할당 받지 못하면 -1값
    public SkillData skillData;        //SkillUI에 리스트나 배열로 스킬 스크립터블 오브젝트 받고 여기(skillslotUI)에 할당
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

        if(quickSlotUI != null)     //퀵슬롯 안찍었으면 QuickSlotUI컴포넌트가 어차피 없을꺼니까 퀵슬롯을 찍었다면 이라는 뜻
        {
            quickSlotUI.QuickSlotSetData(tempSlotSkillUI.tempSkillData);   
        }



        tempSlotSkillUI.transform.gameObject.SetActive(false);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        
    }
}
