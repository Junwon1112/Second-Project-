using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

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

    public SkillTypeCode skillType;

    public string skillStateName;
    public string skillInformation;

    public virtual float SetSkillDamage(float attackDamage)
    {
        float finalSkillDamage = 0;

        finalSkillDamage = skillDamage + (attackDamage * 0.7f);


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
