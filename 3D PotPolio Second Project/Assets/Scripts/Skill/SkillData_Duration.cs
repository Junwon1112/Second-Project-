using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("New Skill Data"), menuName = ("Scriptable Object_Skill Data/Skill Data_Duration"), order = 2)]
public class SkillData_Duration : SkillData     //지속시간이 있는 공격 스킬
{
    float skillDuration;
}
