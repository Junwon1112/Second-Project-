using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SkillUse : MonoBehaviour
{
    public bool isSkillUsed = false;    //쿨타임 체크용 bool함수
    public float timer = 0.0f;
    Animator anim;
    PlayerWeapon weapon;

    private void Awake()
    {
        anim = FindObjectOfType<Player>().transform.GetComponent<Animator>();
        weapon = FindObjectOfType<PlayerWeapon>();    
    }

    private void FixedUpdate()
    {
        if(timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            isSkillUsed = true;
        }
        else
        {
            isSkillUsed = false;
        }
        
    }


    public void UsingSkill(SkillData skillData)
    {
        if(!isSkillUsed)
        {
            timer = skillData.skillCooltime;
            skillData.SetSkillDamage(weapon.AttackDamage);
            anim.SetBool("IsSkillUse", true);
            
        }
    }
}
