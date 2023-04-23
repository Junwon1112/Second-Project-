using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

/// <summary>
/// 스킬 데이터를 가지는 스크립터블 오브젝트, 3종류의 스킬데이터의 부모 클래스
/// </summary>
[CreateAssetMenu (fileName = ("New Skill Data"), menuName = ("Scriptable Object_Skill Data/Skill Data"), order = 1 )]
public class SkillData : ScriptableObject
{
    public string skillName;
    
    public int skillId;
    public SkillIDCode skillIDCode;
    public JobType job;

    public Sprite skillIcon;

    public int requireLevel;
    public float skillCooltime;
    public float skillDamage;
    public uint skillLevel = 0;

    public SkillTypeCode skillType;

    public string skillStateName;
    public string skillInformation;

    public uint SkillLevel
    {
        get { return skillLevel; }
        set 
        { 
            if( value >=0)
            {
                skillLevel = value;
            }
        }
    }


}

public enum SkillIDCode
{
    Skill_Wheelwind = 0, //전사 스킬
    Skill_AirSlash = 1,
    Skill_DashAttack = 2,

    Skill_Meteo = 10  //마법사 스킬
}

public enum SkillTypeCode
{
    Skill_Buff = 0,
    Skill_Duration,
    Skill_Normal,
    Skill_Shooting
}
