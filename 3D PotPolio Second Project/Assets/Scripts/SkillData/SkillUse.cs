using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SkillUse : MonoBehaviour
{
    public bool isSkillUsed = false;    //쿨타임 체크용 bool함수
    public float timer = 0.0f;
    Animator anim;
    Player player;
    SkillData_Duration skillData_Duration;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        anim = player.transform.GetComponent<Animator>();
        
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
            skillData.SetSkillDamage(player.AttackDamage);
            anim.SetBool("IsSkillUse", true);
            if(skillData.skillType == SkillTypeCode.Skill_Duration)
            {
                
               // StartCoroutine(SkillDurationTime());
            }
        }
    }

    IEnumerator SkillDurationTime(float skillDuration) //스킬 지속시간
    {
        yield return new WaitForSeconds(skillDuration);
        anim.SetBool("IsSkillUse", false);
    }

}
