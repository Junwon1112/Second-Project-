using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

/// <summary>
/// 버프타입의 스킬데이터를 가지는 스크립터블 오브젝트, SkillData를 상속받음, 현재 노멀타입의 공격 스킬은 없음
/// </summary>
[CreateAssetMenu (fileName = ("New Skill Data_Normal"), menuName = ("Scriptable Object_Skill Data/Skill Data_Shooting"), order = 4 )]
public class SkillData_Shooting : SkillData
{
    //<부모에 있는 변수들>

    //public string skillName;
    //public uint skillId;
    //public Sprite skillIcon;

    //public int requireLevel;
    //public float skillCooltime;
    //public float skillDamage;

    //public SkillTypeCode skillType;

    //public string skillStateName;
    //public string skillInformation;
    public GameObject projectile_Prefab;



}

