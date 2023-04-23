using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실제 스킬 구현부
/// skillId 0~ 9는 swordman, 10~19은 마법사
/// </summary>
public class Skill_Implement : MonoBehaviour
{
    public static Skill_Implement Instance;

    Player player;
    PlayerWeapon weapon;

    Animator anim;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }



        player = FindObjectOfType<Player>();
        anim = player.transform.GetComponent<Animator>();
    }


    public void TakeWeapon()
    {
        weapon = FindObjectOfType<PlayerWeapon>();
    }

    /// <summary>
    /// 스킬 ID를 입력하면 해당 스킬의 구현 함수를 불러옴
    /// </summary>
    /// <param name="skillID"></param>
    public void PlaySkill_SwordMan(int skillID, SkillData skillData)
    {
        switch (skillID)
        {   
            case 0:
                StartCoroutine(SetAnimTime_SkillDuration(skillData, SkillDataManager.Instance.FindSkill_Duration(skillID).skillDuration));
                SwordMan_Skill_Wheelwind(skillID);
                break;
            case 1:
                anim.SetTrigger($"IsSkillUse_{skillData.skillName}");
                SordMan_Skill_AirSlash(skillID);
                break;
            case 2:
                anim.SetTrigger($"IsSkillUse_{skillData.skillName}");
                Debug.Log("애니메이션에서 발동");
                //Skill_DashAttack(skillID);
                break;

            default:
                Debug.Log("Don't Exist SkillID");
                break;
        }
    }

    public void PlaySkill_Witch(int skillID, SkillData skillData)
    {
        switch (skillID)
        {
            case 10:
                anim.SetTrigger($"IsSkillUse_{skillData.skillName}");
                Witch_Skill_Meteo(skillID);
                break;
            case 11:

                break;
            case 12:

                break;

            default:
                Debug.Log("Don't Exist SkillID");
                break;
        }
    }


    private void SwordMan_Skill_Wheelwind(int skillID)
    {
        SkillData_Duration tempSkill_Duration = SkillDataManager.Instance.FindSkill_Duration(skillID);

        float skillUsingTime = tempSkill_Duration.skillDuration;    //스킬지속시간

        //스킬 데미지 설정
        weapon.SkillDamage = tempSkill_Duration.skillDamage * (tempSkill_Duration.skillLevel * 0.5f) + (player.AttackDamage * 0.2f);

        float compensateTime = 0.5f;    //파티클용 프리팹 지속시간 보정용 시간
        Quaternion compensateRotaion = Quaternion.Euler(-90.0f, 0.0f, 0.0f);    //파티클용 프리팹 회전 보정
        Vector3 compensatePosition = new Vector3(0, -1.5f, 0);  //파티클용 프리팹 위치 보정

        ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_WheelWind, player.transform,
            player.transform.position + compensatePosition, player.transform.rotation * compensateRotaion, skillUsingTime + compensateTime);
    }

    /// <summary>
    /// 지속형태의 스킬 공격시 애니메이션 지속시간을 설정
    /// </summary>
    /// <param name="skillDuration"></param>
    /// <returns></returns>
    IEnumerator SetAnimTime_SkillDuration(SkillData skillData, float skillDuration) //스킬 지속시간
    {
        anim.SetBool($"IsSkillUse_{skillData.skillName}", true);
        yield return new WaitForSeconds(skillDuration);
        anim.SetBool($"IsSkillUse_{skillData.skillName}", false);
    }

    private void SordMan_Skill_AirSlash(int skillID)
    {
        SkillData_Shooting tempSkill_Data = SkillDataManager.Instance.FindSkill_Shooting(skillID);

        //스킬데미지 설정
        weapon.SkillDamage = tempSkill_Data.skillDamage * (tempSkill_Data.skillLevel * 1.0f) + (player.AttackDamage * 0.7f);


        Vector3 compensatePosition = new Vector3(1.0f, 0.5f, 0);  //투사체 프리팹 위치 보정

        Instantiate(tempSkill_Data.projectile_Prefab, player.transform.position + compensatePosition, player.transform.rotation);
    }    

    public void SwordMan_Skill_DashAttack(int skillID)   //플레이어 애니메이션에서 직접 참조해야 해서 얘만 public으로 만듬
    {
        SkillData_Normal tempSkill_Data = SkillDataManager.Instance.FindSkill_Normal(skillID);

        //스킬데미지 설정
        weapon.SkillDamage = tempSkill_Data.skillDamage * (tempSkill_Data.skillLevel * 1.0f) + (player.AttackDamage * 0.5f);

        
        float moveDistance = 20.0f;

        Vector3 dir = player.transform.forward;


        Vector3 skillRange = player.transform.position + dir * moveDistance;
        float radiusRange = 1.0f;

        int checkMonsterNum = 0;

        for(int effectInterval = 0; effectInterval < moveDistance; effectInterval++)
        {
            ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_DashAttack, player.transform.position + dir * effectInterval, player.transform.rotation);
        }
        


        Collider[] monsterColliders = new Collider[6];
        Monster[] monsters = new Monster[6];

        checkMonsterNum = Physics.OverlapCapsuleNonAlloc(player.transform.position, skillRange, radiusRange, monsterColliders, 1 << LayerMask.NameToLayer("Monster"));

        if(checkMonsterNum > 0)
        {
            for(int i = 0; i < checkMonsterNum; i++)
            {
                monsters[i] = monsterColliders[i].transform.parent.GetComponentInChildren<Monster>();

                SoundPlayer.Instance?.PlaySound(SoundType.Sound_WindHit);

                weapon.SkillAttack(monsters[i]);

                monsters[i].SetHP();
                if (monsters[i].HP <= 0 && weapon.isCheckExp)
                {
                    weapon.isCheckExp = false;
                    player.Exp += monsters[i].giveExp;
                    player.SetExp();
                    if (player.Exp >= player.MaxExp)
                    {
                        player.newDel_LevelUp();    //레벨업 델리게이트
                    }
                }
            }
        }

        player.transform.position += dir * moveDistance;

    }

    public void Witch_Skill_Meteo(int skillID)   //플레이어 애니메이션에서 직접 참조해야 해서 얘만 public으로 만듬
    {
        SkillData_Shooting tempSkill_Data = SkillDataManager.Instance.FindSkill_Shooting(skillID);

        //스킬데미지 설정
        weapon.SkillDamage = tempSkill_Data.skillDamage * (tempSkill_Data.skillLevel * 0.5f) + (player.AttackDamage * 0.2f);


        Vector3 compensatePosition = player.transform.forward * 6;  //투사체 프리팹 위치 보정

        Instantiate(tempSkill_Data.projectile_Prefab, player.transform.position + compensatePosition, player.transform.rotation);
    }


}
