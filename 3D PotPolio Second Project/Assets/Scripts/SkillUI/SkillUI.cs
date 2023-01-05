using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public List<SkillData> skillDatas;
    SkillSlotUI[] skillSlotUIs;

    private void Awake()
    {
        skillSlotUIs = GetComponentsInChildren<SkillSlotUI>();
    }

    private void Start()
    {
        for(int i = 0; i < skillSlotUIs.Length; i++)
        {
            skillSlotUIs[i].skillData = skillDatas[i];
            skillSlotUIs[i].SetSkillUIInfo();
        }
        
    }
}
