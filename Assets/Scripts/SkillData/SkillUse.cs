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
    
    Player player;
    PlayerWeapon weapon;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
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
            if(skillData.skillId < 10 && skillData.skillId > -1)
            {
                Skill_Implement.Instance.PlaySkill_SwordMan(skillData.skillId, skillData);
            }
            else if(skillData.skillId < 20)
            {
                Skill_Implement.Instance.PlaySkill_Witch(skillData.skillId, skillData);
            }
            
        }
    }



    /// <summary>
    /// �÷��̾ ���⸦ ������ �ش� ���⸦ �ڵ����� ã���� �ϴ� �޼���
    /// </summary>
    public void TakeWeapon()    //�÷��̾�� ���� ������ �� ������
    {
        weapon = FindObjectOfType<PlayerWeapon>();
    }

}
