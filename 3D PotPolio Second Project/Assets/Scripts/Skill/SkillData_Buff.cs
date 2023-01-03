using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("New Skill Data"), menuName = ("Scriptable Object_Skill Data/Skill Data_Buff"), order = 3)]
public class SkillData_Buff : SkillData //버프 스킬
{
    public float buffDuration;
}
