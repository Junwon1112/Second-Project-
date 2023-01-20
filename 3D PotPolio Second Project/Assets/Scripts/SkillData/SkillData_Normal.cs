using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

[CreateAssetMenu (fileName = ("New Skill Data_Normal"), menuName = ("Scriptable Object_Skill Data/Skill Data"), order = 4 )]
public class SkillData_Normal : SkillData
{
 
    public override float SetSkillDamage(float attackDamage)
    {
        return base.SetSkillDamage(attackDamage);
    }
    


}

