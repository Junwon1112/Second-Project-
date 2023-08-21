using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// 스킬 사용과 관련된 클래스, 퀵슬롯에서 호출
/// </summary>
public class SkillUse : MonoBehaviour
{
    public bool isSkillUsed = false;    //쿨타임 체크용 bool함수
    public float coolTimeRatio;
    public float initCooltime;
    public float timer = 0.0f;
    
    //Player player;
    PlayerWeapon weapon;

    private void Awake()
    {
        //player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        if(timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            coolTimeRatio =  timer / initCooltime;
            isSkillUsed = true;
        }
        else
        {
            isSkillUsed = false;
        }
        
    }

    /// <summary>
    /// 스킬 데이터에 따라 다른형태의 스킬이 실행됨
    /// </summary>
    /// <param name="skillData"></param>
    public void UsingSkill(SkillData skillData)
    {
        if(!isSkillUsed)
        {
            timer = skillData.skillCooltime;
            initCooltime = skillData.skillCooltime;
            if (skillData.skillId < 10 && skillData.skillId > -1)
            {
                Skill_Implement.Instance.PlaySkill_SwordMan(skillData.skillId, skillData);
            }
            else if(skillData.skillId < 20)
            {
                Skill_Implement.Instance.PlaySkill_Witch(skillData.skillId, skillData);
            }
            
        }
    }





}
