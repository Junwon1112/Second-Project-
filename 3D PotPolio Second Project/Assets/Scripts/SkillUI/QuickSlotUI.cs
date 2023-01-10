using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotUI : MonoBehaviour
{
    public int quickSlotID = -1;    //퀵슬롯 아이디는 2000번 부터 시작
    public SkillData quickSlotSkillData;
    Image skillImage;



    private void Awake()
    {
        skillImage = GetComponent<Image>();
    }

    private void Start()
    {
        skillImage.color = Color.clear;
    }



    public void QuickSlotSetData(SkillData skillData)
    {
        quickSlotSkillData = skillData;
        skillImage.sprite = skillData.skillIcon;
    }
}
