using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// ��ų ���� ���õ� Ŭ����, �����Կ��� ȣ��
/// </summary>
public class SkillUse : MonoBehaviour
{
    public bool isSkillUsed = false;    //��Ÿ�� üũ�� bool�Լ�
    public float timer = 0.0f;
    Animator anim;
    Player player;
    PlayerWeapon weapon;
    Skill_Implement skill_Implement;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        
        anim = player.transform.GetComponent<Animator>();
        skill_Implement = FindObjectOfType<Skill_Implement>();
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

    /// <summary>
    /// ��ų �����Ϳ� ���� �ٸ������� ��ų�� �����
    /// </summary>
    /// <param name="skillData"></param>
    public void UsingSkill(SkillData skillData)
    {
        if(!isSkillUsed)
        {
            timer = skillData.skillCooltime;
            
            if(skillData.skillType == SkillTypeCode.Skill_Normal )
            {
                anim.SetTrigger($"IsSkillUse_{skillData.skillName}");
                skill_Implement.PlaySkill(skillData.skillId);
            }

            else if(skillData.skillType == SkillTypeCode.Skill_Duration)
            {
                anim.SetBool($"IsSkillUse_{skillData.skillName}", true);
                skill_Implement.PlaySkill(skillData.skillId);

                SkillData_Duration tempSkill_Duration = GameManager.Instance.SkillDataManager.FindSkill_Duration(skillData.skillId);
                float skillUsingTime = tempSkill_Duration.skillDuration;    //��ų���ӽð�

                StartCoroutine(SkillDurationTime(skillData, skillUsingTime));
            }

            else if (skillData.skillType == SkillTypeCode.Skill_Shooting)
            {
                anim.SetTrigger($"IsSkillUse_{skillData.skillName}");
                skill_Implement.PlaySkill(skillData.skillId);
            }

            else if(skillData.skillType == SkillTypeCode.Skill_Buff)
            {
                anim.SetTrigger($"IsSkillUse_{skillData.skillName}");
                skill_Implement.PlaySkill(skillData.skillId);
            }
        }
    }

    /// <summary>
    /// ���������� ��ų ���ݽ� �ִϸ��̼� ���ӽð��� ����
    /// </summary>
    /// <param name="skillDuration"></param>
    /// <returns></returns>
    IEnumerator SkillDurationTime(SkillData skillData ,float skillDuration) //��ų ���ӽð�
    {
        yield return new WaitForSeconds(skillDuration);
        anim.SetBool($"IsSkillUse_{skillData.skillName}", false);
    }

    /// <summary>
    /// �÷��̾ ���⸦ ������ �ش� ���⸦ �ڵ����� ã���� �ϴ� �޼���
    /// </summary>
    public void TakeWeapon()    //�÷��̾�� ���� ������ �� ������
    {
        weapon = FindObjectOfType<PlayerWeapon>();
    }

}
