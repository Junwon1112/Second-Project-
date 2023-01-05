using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSlotUI : MonoBehaviour
{
    //��ų ���� UI���� �����ؾ� �Ұ� 
    //1. �巡���ؼ� ���������� �ű� �� �־����, ��ų ��� �䱸�������� ������ ���� �Ҵ�� ��ų ����Ʈ�� �־�� �巡�� �����ϰ� ����� ����
    //2. ��Ŭ���̳� ���� Ŭ���ؼ� ��ų �ִϸ��̼� �ߵ�?

    int skillSlotUIid = -1;     //��ų ���� �� id, skillUI Ŭ�������� �Ҵ��� ����, Ȥ�� �Ҵ� ���� ���ϸ� -1��
    public SkillData skillData;        //SkillUI�� ����Ʈ�� �迭�� ��ų ��ũ���ͺ� ������Ʈ �ް� ����(skillslotUI)�� �Ҵ�
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
