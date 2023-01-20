using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SkillUse : MonoBehaviour
{
    public bool isSkillUsed = false;    //��Ÿ�� üũ�� bool�Լ�
    public float timer = 0.0f;
    Animator anim;
    Player player;
    PlayerWeapon weapon;


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
            weapon.SkillDamage = skillData.SetSkillDamage(player.AttackDamage);

            anim.SetBool("IsSkillUse", true);
            if(skillData.skillType == SkillTypeCode.Skill_Duration)
            {
                SkillData_Duration tempSkill_Duration = GameManager.Instance.SkillDataManager.FindSkill_Duration(skillData.skillId);
                StartCoroutine(SkillDurationTime(tempSkill_Duration.skillDuration));
            }
        }
    }

    IEnumerator SkillDurationTime(float skillDuration) //��ų ���ӽð�
    {
        yield return new WaitForSeconds(skillDuration);
        anim.SetBool("IsSkillUse", false);
    }

    public void TakeWeapon()    //�÷��̾�� ���� ������ �� ������
    {
        weapon = FindObjectOfType<PlayerWeapon>();
    }
}
