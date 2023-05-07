using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// 스킬 슬롯 UI에 관한 클래스, 주로 스킬 데이터의 슬롯 간 이동에 관해 다룸
/// </summary>
public class SkillSlotUI : SkillSlotUI_Basic, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    //스킬 슬롯 UI에서 구현해야 할것 
    //1. 드래그해서 퀵슬롯으로 옮길 수 있어야함, 스킬 사용 요구레벨보다 레벨이 높고 할당된 스킬 포인트가 있어야 드래그 가능하게 만들고 싶음
    //2. 우클릭이나 더블 클릭해서 스킬 애니메이션 발동?

    /// <summary>
    /// 스킬 슬롯 별 id, skillUI 클래스에서 할당할 예정, 혹시 할당 받지 못하면 -1값
    /// </summary>
    int skillSlotUIid = -1;

    /// <summary>
    /// SkillUI에 리스트나 배열로 스킬 스크립터블 오브젝트 받고 여기(skillslotUI)에 할당
    /// </summary>
    SkillData skillData;       
    Image skillImage;
    TextMeshProUGUI skillInfo;
    TextMeshProUGUI skillName;
    TempSlotSkillUI tempSlotSkillUI;

    uint currentSkillLevel;

    public UpDownButton upDownButton;

    public RectTransform rectTransform;

    SkillToQuickSlotUI skillToQuickSlotUI;
    AllQuickSlotUI allQuickSlotUI;


    public override Image SkillImage { get => skillImage; set => skillImage = value; }
    public override SkillData SkillData { get => skillData; set => skillData = value; }

    private void Awake()
    {
        SkillImage = GetComponent<Image>();
        skillInfo = transform.parent.Find("Info_Text").GetComponent<TextMeshProUGUI>();
        skillName = transform.parent.Find("SkillName_Text").GetComponent<TextMeshProUGUI>();
        tempSlotSkillUI = GameObject.FindObjectOfType<TempSlotSkillUI>();
        upDownButton = transform.parent.GetComponentInChildren<UpDownButton>();
        skillToQuickSlotUI = FindObjectOfType<SkillToQuickSlotUI>();
        allQuickSlotUI = FindObjectOfType<AllQuickSlotUI>();
    }


    /// <summary>
    /// 스킬 아이콘 위에 일정시간 올려둘시 Info가 나오는 메서드
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
        if (SkillData.skillLevel > 0)
        {
            skillToQuickSlotUI.SkillData = skillData;
            skillToQuickSlotUI.RectTransform_UI.position = eventData.position;

            if(skillToQuickSlotUI.IsUIOnOff)   //다른 스킬 슬롯을 누르면 꺼지는게 아니라 위치만 바뀌어야 하므로 이미 켜져있을땐 실행시키지 않도록 한다.
            {
                skillToQuickSlotUI.UIOnOffSetting();
            }
        }
    }


    /// <summary>
    /// 드래그 시작시 실행될 메서드, 임시 슬롯 생성
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
    /// 드래그 완료시 실행될 메서드, 퀵슬롯위에 두면 해당 퀵슬롯에 스킬 등록
    /// </summary>
    /// <param name="eventData"></param>
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        QuickSlotUI quickSlotUI = obj.GetComponent<QuickSlotUI>();

        if(quickSlotUI != null)     //퀵슬롯 안찍었으면 QuickSlotUI컴포넌트가 어차피 없을꺼니까 퀵슬롯을 찍었다면 이라는 뜻
        {
            for(int i = 0; i < allQuickSlotUI.quickSlotUIs.Length; i++)
            {
                if (allQuickSlotUI.quickSlotUIs[i].quickSlotSkillData == tempSlotSkillUI.SkillData)     //다른 슬롯에 있는 같은 스킬은 지운다
                {
                    allQuickSlotUI.quickSlotUIs[i].QuickSlotSetData();
                }
            }
            quickSlotUI.QuickSlotSetData(tempSlotSkillUI.SkillData);   
        }


        tempSlotSkillUI.SetTempSkillSlotUIData();
        tempSlotSkillUI.transform.gameObject.SetActive(false);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        
    }

}
