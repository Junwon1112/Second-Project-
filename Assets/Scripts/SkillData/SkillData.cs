using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

/// <summary>
/// ��ų �����͸� ������ ��ũ���ͺ� ������Ʈ, 3������ ��ų�������� �θ� Ŭ����
/// </summary>
[CreateAssetMenu (fileName = ("New Skill Data"), menuName = ("Scriptable Object_Skill Data/Skill Data"), order = 1 )]
public class SkillData : ScriptableObject
{
    public string skillName;
    
    public int skillId;
    public SkillIDCode skillIDCode;

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

    public virtual float SetSkillDamage(float attackDamage)
    {
        float finalSkillDamage;

        finalSkillDamage = skillDamage * (skillLevel + 1) + (attackDamage * 0.7f);


        return finalSkillDamage;
    }
    


}

public enum SkillIDCode
{
    Skill_Wheelwind = 0
}

public enum SkillTypeCode
{
    Skill_Buff = 0,
    Skill_Duration,
    Skill_Normal
}