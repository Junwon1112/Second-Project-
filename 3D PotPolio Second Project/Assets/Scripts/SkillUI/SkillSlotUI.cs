using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSlotUI : MonoBehaviour
{
    //스킬 슬롯 UI에서 구현해야 할것 
    //1. 드래그해서 퀵슬롯으로 옮길 수 있어야함, 스킬 사용 요구레벨보다 레벨이 높고 할당된 스킬 포인트가 있어야 드래그 가능하게 만들고 싶음
    //2. 우클릭이나 더블 클릭해서 스킬 애니메이션 발동?

    int skillSlotUIid = -1;     //스킬 슬롯 별 id, skillUI 클래스에서 할당할 예정, 혹시 할당 받지 못하면 -1값
    public SkillData skillData;        //SkillUI에 리스트나 배열로 스킬 스크립터블 오브젝트 받고 여기(skillslotUI)에 할당
    Image skillIcon;
    TextMeshProUGUI skillInfo;


    private void Awake()
    {
        skillIcon = GetComponent<Image>();
        skillInfo = transform.parent.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {

    }

    void Update()
    {
        
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
            skillIcon.sprite = null;
            skillInfo.text = "No Assigned Skill";
        }
    }
}
